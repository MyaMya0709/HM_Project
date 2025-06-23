using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    [Header("Desh")]
    public float dashPower = 10f;
    public float dashDuration = 5f;
    public float dashCooldown = 0.05f;

    [Header("Jump")]
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;

    [Header("DoubleTap")]
    float lastTapTime = -1f;               // 첫번째 입력 시간
    float doubleTapThreshold = 0.2f;       // 더블탭으로 인식하는 시간

    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool isDashing;
    public bool isAbleDash = true;
    public bool isJumpDash = false;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.right; // 기본은 오른쪽

    public IWeapon currentWeapon;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        currentWeapon = GetComponentInChildren<IWeapon>();
    }

    private void Update()
    {
        if (IsDead) return;
    }

    private void FixedUpdate()
    {
        if (IsDead) return;

        if (isJumpDash)
            return; // 슈퍼 점프 중에는 다른 물리 계산 안 함

        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashPower;
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput.x * stats.moveSpeed, rb.linearVelocity.y);
        }

        //rb.linearVelocity = new Vector2(moveInput.x * stats.moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.performed || context.canceled)
        {
            Debug.Log("Move");
            float currentTime = Time.time;
            moveInput = context.ReadValue<Vector2>();

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }

            ////더블 탭
            //if (currentTime - lastTapTime < doubleTapThreshold)
            //{
            //    Debug.Log("Double Tap Detected!");
            //    StartCoroutine(SuperJump());
            //    lastTapTime = -1f; // 리셋
            //}

            //// 일반 움직임 로직
            //else
            //{
            //    Debug.Log("Single Tap");
            //    // 2단 점프 직전에 y속도를 0으로 초기화
            //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //    lastTapTime = currentTime;
            //}
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            Debug.Log("Jump");
            float currentTime = Time.time;

            // 점프 횟수 초기화
            if (IsGrounded())
            {
                jumpCount = 0;
            }

            //공중에서 더블 탭
            if (!IsGrounded() && currentTime - lastTapTime < doubleTapThreshold)
            {
                Debug.Log("Double Tap Detected!");
                StartCoroutine(SuperJump());
                jumpCount++;
                lastTapTime = -1f; // 리셋
            }

            // 일반 점프 로직
            else if (jumpCount <= 1)
            {
                Debug.Log("Single Tap");
                // 2단 점프 직전에 y속도를 0으로 초기화
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
                lastTapTime = currentTime;
            }
        }
    }

    public void OnDesh(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && isAbleDash)
        {
            Debug.Log("IDesh");
            // 슈퍼 점프
            if (!IsGrounded() && moveInput.y > 0.5f) // 공중 + 위 방향 입력
            {
                Debug.Log("UpDesh");
                StartCoroutine(SuperJump());
            }
            //빠른 하강
            else if (!IsGrounded() && moveInput.y < -0.5f) //공중 + 아래 방향 입력
            {
                Debug.Log("DownDesh");
                StartCoroutine(FastFall());
            }
            else
            {
                Debug.Log("DefaultDesh");
                Vector2 dashDir;

                // 대쉬 방향 선택
                if (Mathf.Abs(moveInput.x) > 0.01f)
                    dashDir = new Vector2(moveInput.x, 0).normalized;
                else
                    dashDir = lastLookDirection;

                StartCoroutine(StartDash(dashDir));
            }   
        }
    }

    public IEnumerator StartDash(Vector2 direction)
    {
        Debug.Log("DeshCoroutine");
        isDashing = true; // isDashing 동안 사용자의 입력을 받지 않음
        isAbleDash = false;
        dashDirection = direction.normalized;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // 애니메이션 트리거
        //animator?.SetTrigger("Dash");

        //rb.AddForce(dashDirection * dashPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // 대시 후 애니메이션 복구
        //if (IsGrounded())
        //    animator?.Play("Idle");

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        isAbleDash = true;
    }

    public IEnumerator SuperJump()
    {
        Debug.Log("SuperJumpCoroutine");
        isDashing = true;
        isJumpDash = true;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0.1f;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForFixedUpdate(); // Vector2.zero 후 AddForce적용 시 한 프레임 쉬어야 잘됨
        rb.AddForce(Vector2.up * dashPower, ForceMode2D.Impulse);
        //animator.Play("Jump", -1, 0);

        yield return new WaitForSeconds(0.3f); // 제어 시간
        rb.gravityScale = originalGravity;
        isDashing = false;
        isJumpDash = false;
    }

    public IEnumerator FastFall()
    {
        Debug.Log("FastFallCoroutine");
        isDashing = true;
        isJumpDash = true;
        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        rb.AddForce(Vector2.down * dashPower, ForceMode2D.Impulse);
        //animator.Play("Fall", -1, 0);

        yield return new WaitForSeconds(0.3f); // 제어 시간
        rb.gravityScale = originalGravity;
        isDashing = false;
        isJumpDash = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("OnAttack");
        if (context.performed)
        {
            //if (!IsGrounded() && moveInput.y < -0.5f ) // 공중 + 아래 방향키 입력
            //{
            //    StartCoroutine(FastFall());
            //    StartCoroutine(StartDownAttack)();
            //}

            animator?.SetTrigger("Attack");
            // 무기공격 로직 연결 예정
            currentWeapon.Attack();
        }
    }

    //public IEnumerator StartDownAttack()
    //{

    //    currentWeapon.DownAttack();
    //}

    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}

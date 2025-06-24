using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    public Rigidbody2D rb;
    public Vector2 moveInput;

    [Header("Desh")]
    public float dashPower = 10f;
    public float dashDuration = 5f;
    public float dashCooldown = 0.05f;
    public bool isDashing;
    public bool isAbleDash = true;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.right; // 기본은 오른쪽

    [Header("Jump")]
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;
    public bool isJumpDash = false;

    //[Header("DoubleTap")]
    //float lastTapTime = -1f;               // 첫번째 입력 시간
    //float doubleTapThreshold = 0.2f;       // 더블탭으로 인식하는 시간

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
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        //더블탭 입력 확인
        if (context.performed && context.interaction is MultiTapInteraction tap && tap.tapCount == 2)
        {
            Debug.Log("Double Tap Detected");

            //대쉬 가능 여부 확인
            if (!isDashing && isAbleDash)
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

            if (jumpCount <= 0)
            {
                Debug.Log("Single Tap");
                // 2단 점프 직전에 y속도를 0으로 초기화
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
                
            ////공중에서 더블 탭
            //if (!IsGrounded() && currentTime - lastTapTime < doubleTapThreshold)
            //{
            //    Debug.Log("Double Tap Detected!");
            //    StartCoroutine(SuperJump());
            //    jumpCount++;
            //    lastTapTime = -1f; // 리셋
            //}

            //// 일반 점프 로직
            //else if (jumpCount <= 1)
            //{
            //    Debug.Log("Single Tap");
            //    // 2단 점프 직전에 y속도를 0으로 초기화
            //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //    jumpCount++;
            //    lastTapTime = currentTime;
            //}
        }
    }

    public void OnSuperJump(InputAction.CallbackContext context)
    {
        if (context.performed && context.interaction is MultiTapInteraction tap && tap.tapCount == 2)
        {
            Debug.Log("Double Tap Detected");
            if(!IsGrounded() && !isDashing && isAbleDash)
            StartCoroutine(SuperJump());
        }
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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("OnAttack");
        if (context.performed)
        {
            if (!IsGrounded()) // 공중에서 공격시
            {
                // 위쪽 반동 추가
                rb.linearVelocity = Vector2.up * 4f;

                animator?.SetTrigger("Attack");
                // 무기공격 로직 연결 예정
                currentWeapon.Attack();
            }
            else
            {
                animator?.SetTrigger("Attack");
                // 무기공격 로직 연결 예정
                currentWeapon.Attack();
            }
        }
    }

    public void OnDownAttack(InputAction.CallbackContext context)
    {
        //더블탭 입력 확인
        if (context.performed && context.interaction is MultiTapInteraction tap && tap.tapCount == 2)
        {
            Debug.Log("Double Tap Detected");

            //빠른 하강
            if (!IsGrounded() && !isDashing && isAbleDash) //공중 + 대쉬 가능 여부
            {
                Debug.Log("DownDesh");
                StartCoroutine(StartDownAttack());
            }
        }
    }

    public IEnumerator StartDownAttack()
    {
        Debug.Log("DownAttackCoroutine");
        isDashing = true;
        isJumpDash = true;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.down * dashPower; // 등속 운동 시작

        // 땅에 닿을 때까지 등속 운동 유지
        while (!IsGrounded())
        {
            rb.linearVelocity = Vector2.down * dashPower;
            yield return null; // 매 프레임 유지
        }

        currentWeapon.DownAttack();

        // 도착 시 상태 초기화
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        isDashing = false;
        isJumpDash = false;
    }

    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}

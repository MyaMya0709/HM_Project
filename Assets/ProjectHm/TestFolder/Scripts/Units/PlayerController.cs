using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool isMove = false;

    [Header("Desh")]
    public float dashPower = 10f;
    public float dashCooldown = 0.05f;
    public float dashDistance = 20f;
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

    [Header("DoubleTap")]
    float lastTapTime = -1f;               // 첫번째 입력 시간
    float doubleTapThreshold = 0.2f;       // 더블탭으로 인식하는 시간

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

        if (isJumpDash) return; // 슈퍼 점프 중에는 다른 물리 계산 안 함

        if (isDashing) return;

        if (isMove)
        {
            rb.linearVelocity = new Vector2(moveInput.x * stats.moveSpeed, rb.linearVelocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            float currentTime = Time.time;

            //더블 탭 체크
            if (currentTime - lastTapTime < doubleTapThreshold && !isDashing && isAbleDash)
            {
                Debug.Log("DefaultDesh");
                StartCoroutine(StartDash(lastLookDirection));
                lastTapTime = -1f; // 리셋
            }

            // 일반 이동 로직
            else
            {
                Debug.Log("Move");
                isMove = true;
                moveInput = context.ReadValue<Vector2>();

                // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
                if (Mathf.Abs(moveInput.x) > 0.01f)
                {
                    lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
                }

                lastTapTime = currentTime;
            }
        }
        if (context.canceled)
        {
            isMove = false;
        }
    }

    public IEnumerator StartDash(Vector2 direction)
    {
        Debug.Log("DeshCoroutine");
        isDashing = true; // isDashing 동안 사용자의 입력을 받지 않음
        isAbleDash = false;
        dashDirection = direction;
        Vector2 curPos = rb.position;
        Vector2 tarPos = curPos + dashDirection * dashDistance;

        rb.linearVelocity = Vector2.zero;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // 애니메이션 트리거
        //animator?.SetTrigger("Dash");

        // 대쉬 거리까지 등속 운동
        while (Vector2.Distance(rb.position, tarPos) > 0.01f)
        {
            //rb.linearVelocity = dashDirection * dashPower;
            //yield return null; // 매 프레임 유지

            Vector2 next = Vector2.MoveTowards(rb.position, tarPos, dashPower * Time.fixedDeltaTime);
            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

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

        if (context.started)
        {
            Debug.Log("Jump");
            float currentTime = Time.time;

            // 점프 횟수 초기화
            if (IsGrounded())
            {
                jumpCount = 0;
            }

            //공중에서 더블 탭
            if (!IsGrounded() && currentTime - lastTapTime < doubleTapThreshold && jumpCount == 1)
            {
                Debug.Log("Double Tap Detected!");
                if (!isDashing && isAbleDash)
                    StartCoroutine(SuperJump());
                jumpCount++;
                lastTapTime = -1f; // 리셋
            }

            else if (!IsGrounded() && jumpCount == 1)
            {
                lastTapTime = currentTime;
            }

            // 일반 점프 로직
            else if (IsGrounded() && jumpCount == 0)
            {
                Debug.Log("Single Tap");
                // 2단 점프 직전에 y속도를 0으로 초기화
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }

    public IEnumerator SuperJump()
    {
        Debug.Log("SuperJumpCoroutine");
        isDashing = true;
        isJumpDash = true;

        Vector2 curPos = rb.position;
        Vector2 tarPos = curPos + Vector2.up * dashDistance;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        //animator.Play("Jump", -1, 0);

        rb.linearVelocity = Vector2.zero;
        while (Vector2.Distance(rb.position, tarPos) > 0.01f)
        {
            //rb.linearVelocity = dashDirection * dashPower;
            //yield return null; // 매 프레임 유지

            Vector2 next = Vector2.MoveTowards(rb.position, tarPos, dashPower * Time.fixedDeltaTime);
            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

        // 도착 시 상태 초기화
        rb.linearVelocity = Vector2.zero;
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
                rb.linearVelocity = Vector2.up * 3f;

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
        if (context.performed)
        {
            float currentTime = Time.time;

            //더블 탭 체크
            if (currentTime - lastTapTime < doubleTapThreshold && !IsGrounded())
            {
                Debug.Log("Double Tap Detected");
                StartCoroutine(StartDownAttack());
                lastTapTime = -1f; // 리셋
            }
            else
            {
                lastTapTime = currentTime;
            }
        }
    }

    public IEnumerator StartDownAttack()
    {
        Debug.Log("DownAttackCoroutine");

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
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
    }

    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}

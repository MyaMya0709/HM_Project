using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    [Header("Desh")]
    public float dashPower = 10f;
    public float dashDuration = 5f;
    public float dashCooldown = 0.05f;

    [Header("Jump")]
    public float jumpForce = 80f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;

    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool isDashing;
    public bool isAbleDash = true;
    public bool isJumpDash = false;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.right; // 기본은 오른쪽

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Move");
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                jumpCount = 0;
            }
            if (jumpCount <= 1)
            {
                Debug.Log("Jump");
                // 2단 점프 직전에 y속도를 0으로 초기화
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
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

    private void Attack()
    {
        animator?.SetTrigger("Attack");
        // 무기공격 로직 연결 예정
        
    }
}

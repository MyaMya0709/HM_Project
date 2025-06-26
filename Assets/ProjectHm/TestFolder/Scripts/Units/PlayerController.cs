using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public IWeapon currentWeapon;

    [Header("MovementCheck")]
    public bool isMove = false;
    public bool isDashing = false;
    public bool isAbleDash = true;
    public bool isJumpDash = false;
    public bool isAbleAttack = true;
    public bool isCharging = false;

    [Header("Desh")]
    public float dashPower = 10f;
    public float dashCooldown = 0.05f;
    public float dashDistance = 20f;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.right; // 기본은 오른쪽
    public LayerMask obstacle;                        // 장애물 레이어

    [Header("Jump")]
    public float jumpForce = 5;
    public float jumpPower = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpDistance = 20f;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;

    [Header("Attack")]
    public float rebound = 3f;
    public int attackCount = 0;                   // 공중 공격 횟수
    public float attackDelay = 0.5f;              // 공중공격 4회 이후 딜레이
    public float lastAttackTime;                  // 4번째 공중공격 시간
    public float chargingStart;                   // 차징 시작 시간
    public float chargingTime = 2f;               // 차징 시간

    [Header("DoubleTap")]
    public float lastJumpTapTime = -1f;           // 슈퍼 점프 첫번째 입력 시간
    public float lastDashTapTime = -1f;           // 대쉬 첫번째 입력 시간
    public float lastDownTapTime = -1f;           // 내려찍기 첫번째 입력 시간
    public float doubleTapThreshold = 0.2f;       // 더블탭으로 인식하는 시간

    public Vector2 basePos;
    public Vector2 targetPos;

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

        if (isCharging) return;

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
            if (currentTime - lastDashTapTime < doubleTapThreshold && !isDashing && isAbleDash)
            {
                Debug.Log("DefaultDesh");
                StartCoroutine(StartDash(lastLookDirection));
                lastDashTapTime = -1f; // 리셋
            }
            else
            {
                lastDashTapTime = currentTime;
            }

            // 일반 이동 로직
            Debug.Log("Move");
            isMove = true;
            moveInput = context.ReadValue<Vector2>();

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }
        }
        if (context.canceled)
            isMove = false;
    }

    public IEnumerator StartDash(Vector2 direction)
    {
        Debug.Log("DeshCoroutine");
        isDashing = true; // isDashing 동안 사용자의 입력을 받지 않음
        isAbleDash = false;
        dashDirection = direction;
        basePos = rb.position;
        basePos = new Vector2(
            basePos.x,
            Mathf.Clamp(basePos.y, 0, 100)
            );

        Vector2 trsdd = basePos + dashDirection * dashDistance;
        targetPos = new Vector2(trsdd.x, basePos.y);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        Vector2 ddPos = new Vector2(rb.position.x, basePos.y);

        // 애니메이션 트리거
        //animator?.SetTrigger("Dash");

        // 대쉬 거리까지 등속 운동
        while (Vector2.Distance(ddPos, targetPos) > 0.01f)
        {
            Vector2 next = Vector2.MoveTowards(ddPos, targetPos, dashPower * Time.fixedDeltaTime);

            ddPos = new Vector2(rb.position.x, basePos.y);

            RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, dashPower * Time.fixedDeltaTime, obstacle);
            if (hit)
            {
                Debug.Log("Obstacle hit, break");
                break;
            }

            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

        // 대시 후 애니메이션 복구
        //if (IsGrounded())
        //    animator?.Play("Idle");

        if (!IsGrounded())
        {   
            yield return new WaitForSeconds(0.05f);
            Debug.Log("체공");
        }

        Debug.Log("등속운동 중지");

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        isAbleDash = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //공중
            if (!IsGrounded())
            {
                float currentTime = Time.time;

                if (currentTime - lastJumpTapTime < doubleTapThreshold && jumpCount == 2)
                {
                    //더블 탭
                    Debug.Log("Double Tap Detected!");
                    StartCoroutine(SuperJump());
                    lastJumpTapTime = -1f; // 리셋
                }
                else
                {
                    jumpCount++;
                    lastJumpTapTime = currentTime;
                }
            }

            // 일반 점프 로직
            if (IsGrounded())
            {
                Debug.Log("Jump");
                // 점프 횟수 초기화
                jumpCount = 0;
                // 점프 직전에 y속도를 0으로 초기화
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }

    public IEnumerator SuperJump()
    {
        Debug.Log("SuperJumpCoroutine");
        isJumpDash = true;

        Vector2 curPos = rb.position;
        Vector2 tarPos = curPos + Vector2.up * jumpDistance;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        //animator.Play("Jump", -1, 0);

        rb.linearVelocity = Vector2.zero;
        while (Vector2.Distance(rb.position, tarPos) > 0.01f)
        {
            //rb.linearVelocity = dashDirection * dashPower;
            //yield return null; // 매 프레임 유지

            Vector2 next = Vector2.MoveTowards(rb.position, tarPos, jumpPower * Time.fixedDeltaTime);
            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

        if(!IsGrounded())
            yield return new WaitForSeconds(0.05f);

        // 도착 시 상태 초기화
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;

        isJumpDash = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            chargingStart = Time.time;
            isCharging = true;
        }

        else if (context.performed)
        {
            Debug.Log("Performed");
        }

        else if (context.canceled)
        {
            Debug.Log("OnAttack");

            if (chargingStart + chargingTime >= Time.time)
            {
                isCharging = false;
            }

            if (!IsGrounded()) // 공중 체크
            {
                // 딜레이 체크
                if (lastAttackTime + attackDelay >= Time.time)
                    return;

                // 위쪽 반동 추가
                rb.linearVelocity = Vector2.up * rebound;

                // 공격 횟수
                attackCount++;
                Debug.Log($"Attack {attackCount}");

                animator?.SetTrigger("Attack");
                // 무기공격 로직 연결 예정
                currentWeapon.Attack();

                if (attackCount == 4)
                {
                    lastAttackTime = Time.time;
                    attackCount = 0;
                }
            }
            else if (IsGrounded())
            {
                if (chargingStart + chargingTime <= Time.time)
                {
                    Debug.Log($"{Time.time}");
                    Debug.Log("ChargingAttack");
                    //animator?.SetTrigger("ChargingAttack");
                    currentWeapon.ChargingAttack();
                    isCharging = false;
                }
                else
                {
                    // 땅에 닿으면 횟수 초기화
                    attackCount = 0;
                    // 일반 공격
                    animator?.SetTrigger("Attack");
                    currentWeapon.Attack();
                }
            }
        }
    }

    public void OnDownAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float currentTime = Time.time;

            //더블 탭 체크
            if (currentTime - lastDownTapTime < doubleTapThreshold && !IsGrounded())
            {
                Debug.Log("Double Tap Detected");
                StartCoroutine(StartDownAttack());
                lastDownTapTime = -1f; // 리셋
            }
            else
            {
                lastDownTapTime = currentTime;
            }
        }
    }

    public IEnumerator StartDownAttack()
    {
        Debug.Log("DownAttackCoroutine");

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // 땅에 닿을 때까지 등속 운동
        while (!IsGrounded())
        {
            rb.linearVelocity = Vector2.down * dashPower;
            yield return null; // 매 프레임 유지
        }
        rb.linearVelocity = Vector2.zero;
        currentWeapon.DownAttack();

        // 도착 시 상태 초기화
        rb.gravityScale = originalGravity;
    }

    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}

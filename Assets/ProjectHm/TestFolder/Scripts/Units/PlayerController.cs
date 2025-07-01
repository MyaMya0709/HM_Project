using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    public int curGold = 0;
    public float curExp = 0;
    public float curBuff = 0f;

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
    public bool isDashAttack = false;

    [Header("Desh")]
    public float dashPower = 10f;
    public float dashCooldown = 0.05f;
    public float dashDistance = 20f;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.right; // 기본은 오른쪽
    public LayerMask obstacle;                        // 장애물 레이어
    public Vector2 basePos;
    public Vector2 targetPos;

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
    public Vector2 lastkey = Vector2.zero;        // 방향 저장
    public int tapCount = 0;                      // 더블탭 카운트

    [Header("ItemLooting")]
    public Transform lootingArea;                 // 루팅 기준점
    public LayerMask lootingItem;                 // 루팅 가능한 아이템 레이어
    public float lootingRadius = 10f;             // 루팅 가능 거리

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        currentWeapon = GetComponentInChildren<IWeapon>();
    }

    private void Update()
    {
        if (IsDead) return;
        IsLooting();
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
            moveInput = context.ReadValue<Vector2>(); // 입력키 저장
            if (moveInput == null) return;            // 입력키 없을때 되돌아가기

            Vector2 curDir = moveInput;               // 입력 방향 저장
            float currentTime = Time.time;            // 누른 시간 저장

            // 대쉬하는중, 가능여부, 더블 탭, 입력 방향 체크
            if (curDir == lastkey && currentTime - lastDashTapTime < doubleTapThreshold && !isDashing && isAbleDash)
            {
                //조건 달성 두번째 입력시, 탭 카운트 2 달성
                tapCount++;
            }
            else
            {
                // 첫번째 키 입력 시, 탭 카운트 1과 입력키 저장
                // 조건 미달성 두번째 키 입력, 탭 카운트 유지 및 입력키 저장
                tapCount = 1;
                lastkey = curDir;
            }

            lastDashTapTime = currentTime;            // 마지막 키 입력시간 저장

            // 더블 탭 성공 체크
            if (tapCount == 2)
            {
                // 탭 카운트 초기화 및 대쉬 실행
                Debug.Log("DefaultDesh");
                tapCount = 0;
                StartCoroutine(StartDash(lastLookDirection));
            }

            // 대쉬 이후 일반 이동
            Debug.Log("Move");
            isMove = true;

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }
        }
        // 키 입력이 끝날 때, 이동 종료 
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
        targetPos = basePos + dashDirection * dashDistance;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;


        // 애니메이션 트리거
        //animator?.SetTrigger("Dash");

        // 대쉬 거리까지 등속 운동
        while (Vector2.Distance(rb.position, targetPos) > 0.01f)
        {
            targetPos = new Vector2 (targetPos.x, rb.position.y);

            Vector2 next = Vector2.MoveTowards(rb.position, targetPos, dashPower * Time.fixedDeltaTime);

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

    public void IsLooting()
    {
        //아이템 감지
        Collider2D[] items = Physics2D.OverlapCircleAll(lootingArea.position, lootingRadius, lootingItem);

        StartCoroutine(OnLooting(items));
    }

    public IEnumerator OnLooting(Collider2D[] items)
    {
        yield return new WaitForSeconds(1f);

        foreach (Collider2D item in items)
        {
            if (item == null) continue;

            // 감지된 아이템 끌어당기기
            item.transform.position = Vector3.MoveTowards(item.transform.position, transform.position, 15f * Time.deltaTime);

            // 아이템과 플레이어가 가까우면 아이템 습득 및 파괴
            float distance = Vector2.Distance(item.transform.position, transform.position);
            if (distance < 0.3f)
            {
                LootableItem loot = item.GetComponent<LootableItem>();
                if (loot != null)
                {
                    item.GetComponent<LootableItem>().OnLooted(this);
                    Destroy(item.gameObject);
                }
            }
        }
    }
}

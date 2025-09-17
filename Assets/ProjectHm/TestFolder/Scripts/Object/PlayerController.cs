using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public PlayerCondition condition;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public GameObject weaponHolder;
    public IManualWeapon currentWeapon;
    public Animator animator;

    [Header("MovementCheck")]
    public bool isMove = false;
    public bool isDashing = false;
    public bool isAbleDash = true;
    public bool isJumpDash = false;
    public bool isAbleAttack = true;
    public bool isCharging = false;

    [Header("Desh")]
    public float dashPower = 80f;
    public float dashCooldown = 0.15f;
    public float dashDistance = 4f;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.left;  // 기본은 왼쪽
    public LayerMask obstacle;                        // 장애물 레이어
    public Vector2 basePos;
    public Vector2 targetPos;

    [Header("Jump")]
    public float jumpForce = 50f;
    public float superJumpForce = 100f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpDistance = 7f;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;

    [Header("Attack")]
    public float rebound = 4.5f;
    public float attackCooldown;

    public int attackCount = 0;                   // 공중 공격 횟수
    public float attackRest = 0.6f;               // 공중공격 4회 이후 딜레이
    public float lastOnAirTime;                   // 4번째 공중공격 시간

    public float chargingStart;                   // 차징 시작 시간
    public float holdTime;                        // 차징을 하고 있던 시간
    public float chargingTime = 0.3f;             // 차징 시간
    public float lastAttackTime;                  // 마지막 공격 시간

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
    public float lootingSpeed = 30f;              // 루팅 속도

    [Header("Particle")]
    public GameObject moveParticle;
    public GameObject dashParticle;
    public GameObject downParticle;
    public GameObject jumpParticle;

    float dustTimer = 0f;

    public bool facingRight = false;

    private void Awake()
    {
        if (weaponHolder.transform.childCount == 0)
        {
            GameManager.Instance.WeaponInit(weaponHolder);
            currentWeapon = weaponHolder.GetComponentInChildren<IManualWeapon>();
            Debug.Log("시작시 무기 장착");
        }
        else
        {
            foreach (Transform child in weaponHolder.transform) Destroy(child.gameObject);
            GameManager.Instance.WeaponInit(weaponHolder);
            currentWeapon = weaponHolder.GetComponentInChildren<IManualWeapon>();
            Debug.Log("시작시 무기 장착");
        }

        condition = GetComponent<PlayerCondition>();
        animator = GetComponent<Animator>();

        if (condition.characterData == null) animator.runtimeAnimatorController = GameManager.Instance.curCharacterData.animator;
        else animator.runtimeAnimatorController = condition.characterData.animator;
            
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        holdTime = Time.time - chargingStart;
        // 착지 상태 체크해서 애니메이션 전환
        animator.SetBool("IsGrounded", IsGrounded());
        currentWeapon.anim.SetBool("IsGrounded", IsGrounded());

        IsLooting();

        if (lastLookDirection.x < 0 && facingRight)
        {
            Flip();
        }
        else if (lastLookDirection.x > 0 && !facingRight)
        {
            Flip();
        }

        if (IsGrounded() && isMove)
        {
            dustTimer += Time.deltaTime;
            if (dustTimer >= 0.2f)
            {
                // 현재 위치로 이동
                GameObject ps = Instantiate(moveParticle, transform.position, transform.rotation);
                ps.GetComponent<ParticleSystem>().Emit(1); // 파티클 1개 생성
                dustTimer = 0f;
            }
        }
        else
        {
            dustTimer = 0.2f; // 이동이 멈추면 타이머 초기화
        }
    }

    bool fefef;

    private void FixedUpdate()
    {
        if (isJumpDash) return; // 슈퍼 점프 중에는 다른 물리 계산 안 함

        if (isDashing) return;

        if (isCharging) return;

        fefef = Time.time - lastAttackTime >= attackCooldown;

        if (isMove && (fefef || !IsGrounded()))
        {
            rb.linearVelocity = new Vector2(moveInput.x * condition.totalMoveSpeed, rb.linearVelocity.y);
        }
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.ReadValue<Vector2>()}");
        if (context.performed)
        {
            Vector2 eeeee = context.ReadValue<Vector2>(); // 입력키 저장
            if (eeeee == null) return;            // 입력키 없을때 되돌아가기
            if (eeeee.x > 0)
            {
                DoMove(true, eeeee.x);
            }
            else
            {
                DoMove(true, eeeee.x);
            }

        }
        // 키 입력이 끝날 때, 이동 종료 
        if (context.canceled)
        {
            Debug.Log("moveEE");
            
            DoMove(false, 0);
        }
    }

    public void DoMove(bool onMove, float moveDir)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) Debug.Log("폰");

        if (onMove)
        {
            moveInput.x = moveDir;
            Vector2 curDir = new Vector2(0f, 0f);
            if (moveDir > 0)
            {
                curDir = new Vector2(1f, 0f);
            }
            else if(moveDir < 0)
            {
                curDir = new Vector2(-1f, 0f);
            }

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
                // 파티클 재생
                if (facingRight)
                {
                    GameObject ps = Instantiate(dashParticle, new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z), transform.rotation);
                    ps.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    GameObject ps = Instantiate(dashParticle, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), transform.rotation);
                    ps.transform.localScale = new Vector3(1, 1, 1);
                }

            }

            // 대쉬 이후 일반 이동
            Debug.Log("Move");
            isMove = true;
            animator.SetBool("IsMove", true);
            currentWeapon.anim?.SetBool("IsMove", true);

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }
        }
        // 키 입력이 끝날 때, 이동 종료 
        if (!onMove)
        {
            isMove = false;
            animator.SetBool("IsMove", false);
            currentWeapon.anim?.SetBool("IsMove", false);
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // 수평속도 즉시 0
        }
    }
    public void RightMove() => DoMove(true, 1f);
    public void LeftMove() => DoMove(true, -1f);
    public void DontMove() => DoMove(false, 0f);
    public IEnumerator StartDash(Vector2 direction)
    {
        // 애니메이션 재생 시작
        animator?.SetBool("IsDash", true);
        currentWeapon.anim?.SetBool("IsDash", true);

        Debug.Log("DeshCoroutine");
        isDashing = true; // isDashing 동안 사용자의 입력을 받지 않음
        isAbleDash = false;
        dashDirection = direction;    //방향
        basePos = rb.position;
        targetPos = basePos + dashDirection * dashDistance;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // 대쉬 거리까지 등속 운동
        while (Vector2.Distance(rb.position, targetPos) > 0.01f)
        {
            targetPos = new Vector2(targetPos.x, rb.position.y);

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

        if (!IsGrounded())
        {
            yield return new WaitForSeconds(0.05f);
            Debug.Log("체공");
        }

        Debug.Log("등속운동 중지");
        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        // 대시 후 애니메이션 복구
        animator?.SetBool("IsDash", false);
        currentWeapon.anim?.SetBool("IsDash", false);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        isAbleDash = true;
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DoJump();
        }
    }
    public void DoJump()
    {
        //공중
        if (!IsGrounded())
        {
            float currentTime = Time.time;

            if (currentTime - lastJumpTapTime < doubleTapThreshold && jumpCount == 2)
            {
                //더블 탭
                Debug.Log("Double Tap Detected!");
                Instantiate(jumpParticle, transform.position, transform.rotation);
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
            // 파티클 재생
            Instantiate(jumpParticle, transform.position, transform.rotation);
            // 점프 횟수 초기화
            jumpCount = 0;
            // 점프 직전에 y속도를 0으로 초기화
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
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

            Vector2 next = Vector2.MoveTowards(rb.position, tarPos, superJumpForce * Time.fixedDeltaTime);
            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

        if (!IsGrounded())
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
            DoAttack(false);
        }

        else if (context.canceled)
        {
            DoAttack(true);
        }
    }
    public void DoAttack(bool type)
    {
        if (type == false)
        {
            // ===== 공격 실행 가능 여부 체크 =====
            isAbleAttack = Time.time - lastAttackTime >= attackCooldown;

            if (!isAbleAttack) return;

            Debug.Log("Attack Start");
            chargingStart = Time.time;

            Debug.Log($"{holdTime}");
            if (holdTime > chargingTime && IsGrounded())
            {
                isCharging = true;

                Debug.Log("Start & Charging");
                //차징 시작 & 차징 중
                animator?.SetTrigger("OnCharging");
                currentWeapon.anim?.SetTrigger("OnCharging");

                animator?.SetBool("IsCharging", isCharging);
                currentWeapon.anim?.SetBool("IsCharging", isCharging);
            }
        }
        else if (type == true)
        {
            Debug.Log("OnAttack");

            isCharging = false;

            // 무기에 따른 애니메이션 선택
            OnWeaponTypeSet(currentWeapon.data.weaponID);

            if (!IsGrounded()) // 공중 체크
            {
                if (!isAbleAttack) return;

                // 공중 4회 공격 딜레이 체크
                if (lastOnAirTime + attackRest >= Time.time)
                    return;

                // 위쪽 반동 추가
                rb.linearVelocity = Vector2.up * rebound;

                //애니메이션 재생 및 무기 애니메이션 재생 중에 공격 이벤트 발생
                animator?.SetTrigger("OnAttack");
                currentWeapon.anim?.SetTrigger("OnAttack");

                // 공격 횟수
                attackCount++;
                Debug.Log($"Attack {attackCount}");

                // 공중공격 4회 - 마지막 공격시간 체크 및 초기화
                if (attackCount == 4)
                {
                    lastOnAirTime = Time.time;
                    attackCount = 0;
                }
            }
            else // 지상 공격
            {
                // 차징시간에 따라 일반공격과 차징공격 분리
                if (holdTime > chargingTime)
                {
                    if (isAbleAttack) // 차징 공격도 쿨타임 체크
                    {
                        Debug.Log("ChargingAttack");

                        animator?.SetBool("IsCharging", isCharging);
                        currentWeapon.anim?.SetBool("IsCharging", isCharging);
                    }
                }
                else
                {
                    if (isAbleAttack) // 일반 공격도 쿨타임 체크
                    {
                        Debug.Log("Attack");
                        // 땅에 닿으면 횟수 초기화
                        attackCount = 0;

                        //애니메이션 재생 및 무기 애니메이션 재생 중에 공격 이벤트 발생
                        animator?.SetTrigger("OnAttack");
                        currentWeapon.anim?.SetTrigger("OnAttack");

                    }
                }
            }
            if (isAbleAttack) lastAttackTime = Time.time;
        }
    }
    public void OnWeaponTypeSet(int weaponID)
    {
        switch (weaponID)
        {
            case 100:
                animator?.SetFloat("AttackType", 0f);
                break;

            case 101:
                animator?.SetFloat("AttackType", 1f);
                break;

            case 102:
                animator?.SetFloat("AttackType", 2f);
                break;

            case 103:
                animator?.SetFloat("AttackType", 1f);
                break;
            
            default:
                Debug.Log("범위에 없는 무기");
                break;
        }
    }

    public void OnDownAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DoDownAttack();
        }
    }
    public void DoDownAttack()
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
    public IEnumerator StartDownAttack()
    {
        Debug.Log("DownAttackCoroutine");

        // 애니메이션 재생
        animator.SetTrigger("OnDropAttack");
        currentWeapon.anim.SetTrigger("OnDropAttack");

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

        // 도착 시 상태 초기화
        rb.gravityScale = originalGravity;
    }


    public void EquipWeapon(IManualWeapon newWeapon)
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
            item.transform.position = Vector3.MoveTowards(item.transform.position, transform.position, lootingSpeed * Time.deltaTime);

            // 아이템과 플레이어가 가까우면 아이템 습득 및 파괴
            float distance = Vector2.Distance(item.transform.position, transform.position);
            if (distance < 0.3f)
            {
                LootableItem loot = item.GetComponent<LootableItem>();
                if (loot != null)
                {
                    item.GetComponent<LootableItem>().OnLooted(condition);
                    Destroy(item.gameObject);
                }
            }
        }
    }
}

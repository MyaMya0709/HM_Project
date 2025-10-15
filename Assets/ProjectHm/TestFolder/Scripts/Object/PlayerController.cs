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
    public IManualWeapon curWeapon;
    public Animator animator;
    public GameObject skillBag;
    public ISkill curSkill;
    public UI_State state;              //InitStateUI (Id => 0==대쉬/1==내려찍기/2==슈퍼점프,  coolTime)
    public UI_Skill cooltimeUI;

    [Header("MovementCheck")]
    public bool isMove = false;
    public bool isDashing = false;
    public bool isAbleDash = true;
    public bool isSuperJump = false;
    
    public bool isHolding = false;
    public bool isCharging = false;
    public bool isSkillHolding = false;
    public bool isSkillCharging = false;

    public bool isNormalAttacking = false;
    public bool isDownAttacking = false;
    public bool isChargingAttacking = false;
    public bool isDashAttacking = false;

    public bool facingRight = false;

    [Header("Desh")]
    public float dashPower = 80f;
    public float dashCooldown = 0.15f;
    public float dashDistance = 4f;
    public Vector2 dashDirection;
    public Vector2 lastLookDirection = Vector2.left;  // 기본은 왼쪽
    public LayerMask obstacle;                        // 장애물 레이어
    public Vector2 basePos;
    public Vector2 targetPos;
    public float lastDashTime = 0f;                   // 마지막으로 대쉬한 시간

    [Header("Jump")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int jumpCount = 0;
    public float superJumpForce = 100f;               // 슈퍼점프 속도
    public float superJumpDistance = 7f;              // 슈퍼점프 높이
    public float lastSuperJumpTime = 0f;              // 마지막으로 슈퍼점프한 시간
    public float lastDownAttackTime = 0f;             // 마지막으로 내려찍기한 시간

    [Header("Attack")]
    public float rebound = 4.5f;
    public float attackingTime;
    public float chargeAttackingTime;

    public int attackCount = 0;                   // 공중 공격 횟수
    public float attackRest = 0.6f;               // 공중공격 4회 이후 딜레이
    public float lastOnAirTime;                   // 4번째 공중공격 시간

    public float startCheckTime;                  // 누른 시간
    public float holdTime;                        // 누르고 있던 시간
    public float attackChargingTime;              // 차징하고 있던 시간
    public float chargeTime = 0.3f;               // 차징 체크 시간
    public float lastAttackTime;                  // 마지막 공격 시간

    [Header("Skill")]
    public float startSkillCheckTime;             // 누른 시간
    public float skillHoldTime;                   // 누르고 있던 시간
    public float skillChargingTime;               // 차징하고 있던 시간
    public float skillChargeTime = 0.3f;          // 차징 체크 시간
    public float lastSkillTime;                   // 마지막 공격 시간

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

    private void Awake()
    {
        condition = GetComponent<PlayerCondition>();
        animator = GetComponent<Animator>();

        if (condition.characterData == null) animator.runtimeAnimatorController = GameManager.Instance.curCharacterData.animator;
        else animator.runtimeAnimatorController = condition.characterData.animator;

        rb = GetComponent<Rigidbody2D>();
        GameObject UI = GameObject.Find("UI");
        state = UI.GetComponentInChildren<UI_State>();
        cooltimeUI = UI.GetComponentInChildren<UI_Skill>();

        //[수정 필요] 무기 장착 로직
        if (weaponHolder.transform.childCount == 0)
        {
            GameManager.Instance.WeaponInit(weaponHolder);
            curWeapon = weaponHolder.GetComponentInChildren<IManualWeapon>();
            Debug.Log("시작시 무기 장착");
        }
        else
        {
            foreach (Transform child in weaponHolder.transform) Destroy(child.gameObject);
            GameManager.Instance.WeaponInit(weaponHolder);
            curWeapon = weaponHolder.GetComponentInChildren<IManualWeapon>();
            Debug.Log("시작시 무기 장착");
        }

        //[수정 필요] 스킬 장착 로직
        if (skillBag.transform.childCount == 0 && GameManager.Instance.skillID >= 0)
        {
            GameManager.Instance.SkillInit(skillBag);
            curSkill = skillBag.GetComponentInChildren<ISkill>();
            Debug.Log("시작시 스킬 장착");
        }
        else if ((skillBag.transform.childCount == 0 || skillBag.transform.childCount != 0) && GameManager.Instance.skillID < 0)
        {
            foreach (Transform child in skillBag.transform) Destroy(child.gameObject);
            curSkill = null;
            Debug.Log("스킬 없음");
        }
        else if (skillBag.transform.childCount != 0 && GameManager.Instance.skillID >= 0)
        {
            foreach (Transform child in skillBag.transform) Destroy(child.gameObject);
            GameManager.Instance.SkillInit(skillBag);
            curSkill = skillBag.GetComponentInChildren<ISkill>();
            Debug.Log("시작시 스킬 장착");
        }

        // 스킬 타이머
        Debug.Log($"curSkill : {curSkill == null}");
        if (curSkill == null) cooltimeUI.gameObject.SetActive(false);
        else
        {
            cooltimeUI.gameObject.SetActive(true);
            cooltimeUI.cooltimeIcon.sprite = curSkill.skillData.sprite;
        }

        //Debug.Log($"curSkill : {condition.playerData.skillID >= 0}");
        //if (condition.playerData.skillID < 0) skillUI.gameObject.SetActive(false);
        //else
        //{
        //    skillUI.gameObject.SetActive(true);
        //    skillUI.skillIcon.sprite = DataManager.Instance.skillDataList[condition.playerData.skillID].sprite;
        //}
    }

    private void Update()
    {
        if (isHolding)
        {
            //차징 시간 체크
            holdTime = Time.time - startCheckTime;
        }

        if (isSkillHolding)
        {
            //차징 시간 체크
            skillHoldTime = Time.time - startSkillCheckTime;
        }

        // 착지 상태 체크해서 애니메이션 전환
        animator.SetBool("IsGrounded", IsGrounded());
        curWeapon.anim.SetBool("IsGrounded", IsGrounded());

        IsLooting();

        //마지막으로 입력된 방향 값과 캐릭터 방향을 비교하고 공격 중이 아니면 반전
        if (lastLookDirection.x < 0 && facingRight && !IsAttacking()) Flip();
        else if (lastLookDirection.x > 0 && !facingRight && !IsAttacking()) Flip();

        //이동시 먼지 파티클 생성
        if (IsGrounded() && isMove)
        {
            dustTimer += Time.deltaTime;
            if (dustTimer >= 0.2f)
            {
                // 현재 위치로 먼지 생성
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

    private void FixedUpdate()
    {
        if (isSuperJump || isDashing || isCharging || isDownAttacking) return; // 슈퍼 점프와 대쉬, 차징, 내려찍기 중에는 이동 안 함

        if (isMove && (!IsAttacking() || !IsGrounded()))
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
            Vector2 curDir = context.ReadValue<Vector2>(); // 입력키 저장
            if (curDir == null) return;            // 입력키 없을때 되돌아가기
            if (curDir.x > 0)
            {
                DoMove(true, curDir.x);
            }
            else
            {
                DoMove(true, curDir.x);
            }
        }
        // 키 입력이 끝날 때, 이동 종료 
        if (context.canceled)
        {
            DoMove(false, 0);
        }
    }
    public void DoMove(bool onMove, float moveDir)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) Debug.Log("폰");

        if (isDownAttacking) return;

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
                //조건 달성 두번째 입력시 => 탭 카운트 2 달성
                tapCount++;
            }
            else
            {
                // 첫번째 키 입력 시, 탭 카운트 1과 입력키 저장
                // 조건 미달성 두번째 키 입력 => 탭 카운트 유지 및 입력키 저장
                tapCount = 1;
                lastkey = curDir;
            }

            lastDashTapTime = currentTime; // 마지막 키 입력시간 저장

            // 더블 탭 성공 체크
            if (tapCount == 2 && (curWeapon.totalDashCooltime <= Time.time - lastDashTime || lastDashTime == 0))
            {
                // 탭 카운트 초기화 및 대쉬 실행
                Debug.Log("DefaultDesh");
                Debug.Log($"{lastDashTime},{curWeapon.totalDashCooltime},{Time.time}");
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
            curWeapon.anim?.SetBool("IsMove", true);

            // 방향이 바뀌면 마지막에 바라본 방향으로 갱신
            if (Mathf.Abs(moveInput.x) > 0.01f)
            {
                lastLookDirection = new Vector2(Mathf.Sign(moveInput.x), 0);
            }
        }
        // 키 입력이 끝날 때, 이동 종료 
        else
        {
            isMove = false;
            animator.SetBool("IsMove", false);
            curWeapon.anim?.SetBool("IsMove", false);
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // 수평속도 즉시 0
        }
    }
    public void RightMove() => DoMove(true, 1f);
    public void LeftMove() => DoMove(true, -1f);
    public void DontMove() => DoMove(false, 0f);
    public IEnumerator StartDash(Vector2 direction)
    {
        lastDashTime = Time.time;
        // 애니메이션 재생 시작
        animator?.SetBool("IsDash", true);
        curWeapon.anim?.SetBool("IsDash", true);

        Debug.Log("DeshCoroutine");
        isDashing = true; // isDashing 동안 사용자의 입력을 받지 않음
        isAbleDash = false;
        dashDirection = direction;    //방향
        basePos = rb.position;
        targetPos = basePos + dashDirection * dashDistance;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        if (curWeapon.isDashAttack)
        {
            isDashAttacking = true;
            // 선 딜레이
            yield return new WaitForSeconds(curWeapon.data.before_Attack_DelayRatio * curWeapon.totalAttackSpeed);
        }

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
            Debug.Log(Time.time);
        }

        if (curWeapon.isDashAttack)
        {
            // 후 딜레이
            yield return new WaitForSeconds(curWeapon.data.after_Attack_DelayRatio * curWeapon.totalAttackSpeed);
            isDashAttacking = false;
        }

        
        Debug.Log("등속운동 중지");
        Debug.Log(Time.time);
        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        // 대시 후 애니메이션 복구
        animator?.SetBool("IsDash", false);
        curWeapon.anim?.SetBool("IsDash", false);

        isDashing = false;

        // UI생성
        state.InitStateUI(0, curWeapon.totalDashCooltime);
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
        // 공격 중 점프 금지
        if (IsAttacking()) return;

        //공중
        if (!IsGrounded())
        {
            float currentTime = Time.time;

            if ((currentTime - lastJumpTapTime < doubleTapThreshold) && (jumpCount == 2) && (curWeapon.totalSuperJumpCooltime <= Time.time - lastSuperJumpTime || lastSuperJumpTime == 0))
            {
                //더블 탭
                Debug.Log("Double Tap Detected!");
                Instantiate(jumpParticle, transform.position, transform.rotation);
                StartCoroutine(SuperJump());
                lastJumpTapTime = -1f; // 리셋

                state.InitStateUI(2, curWeapon.totalSuperJumpCooltime); // UI생성
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
            rb.AddForce(Vector2.up * condition.totalJumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
    public IEnumerator SuperJump()
    {
        lastSuperJumpTime = Time.time;

        Debug.Log("SuperJumpCoroutine");
        isSuperJump = true;

        Vector2 curPos = rb.position;
        Vector2 tarPos = curPos + Vector2.up * superJumpDistance;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

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

        isSuperJump = false;
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
        if (IsAttacking())
        {
            return;
        }
        
        if (type == false)
        {
            Debug.Log("Attack Start");
            startCheckTime = Time.time;

            isHolding = true;

            Debug.Log($"{holdTime}");
            if (holdTime > chargeTime && IsGrounded())
            {
                isCharging = true;

                StartCoroutine(ChargingTimeCheck());

                Debug.Log("Start & Charging");
                //차징 시작 & 차징 중
                animator?.SetTrigger("OnCharging");
                curWeapon.anim?.SetTrigger("OnCharging");

                animator?.SetBool("IsCharging", isCharging);
                curWeapon.anim?.SetBool("IsCharging", isCharging);
            }
        }
        else if (type == true)
        {
            Debug.Log("OnAttack");

            isHolding = false;
            isCharging = false;

            // 무기에 따른 애니메이션 선택
            OnWeaponTypeSet(curWeapon.data.weaponID);

            if (!IsGrounded()) // 공중 체크
            {
                // 공중 4회 공격 딜레이 체크
                if (lastOnAirTime >= Time.time - attackRest)
                    return;

                // 위쪽 반동 추가
                rb.linearVelocity = Vector2.up * rebound;

                //애니메이션 재생 및 무기 애니메이션 재생 중에 공격 이벤트 발생
                StartCoroutine(AttackCoroutine(false));

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
                // 땅에 닿으면 횟수 초기화
                attackCount = 0;

                // 차징시간에 따라 일반공격과 차징공격 분리
                if (holdTime > chargeTime)
                {
                    Debug.Log("ChargingAttack");
                    StartCoroutine(AttackCoroutine(true));
                }
                else
                {
                    Debug.Log("Attack");
                    StartCoroutine(AttackCoroutine(false));
                }
            }
            lastAttackTime = Time.time;
        }
    }
    // 차징 시작 후 차징시간 체크
    public IEnumerator ChargingTimeCheck()
    {
        attackChargingTime = Time.time;

        yield return new WaitUntil(() => !isCharging);

        attackChargingTime = Time.time - attackChargingTime;
    }
    public IEnumerator AttackCoroutine(bool ischarging)
    {
        if (!ischarging)
        {
            isNormalAttacking = true;
            // 선 딜레이
            yield return new WaitForSeconds(curWeapon.data.before_Attack_DelayRatio * curWeapon.totalAttackSpeed);

            // 공격 애니메이션 및 공격
            animator?.SetTrigger("OnAttack");
            curWeapon.anim?.SetTrigger("OnAttack");
            yield return new WaitForSeconds(attackingTime);

            // 후 딜레이
            yield return new WaitForSeconds(curWeapon.data.after_Attack_DelayRatio * curWeapon.totalAttackSpeed);
            isNormalAttacking = false;
        }
        else
        {
            isChargingAttacking = true;
            // 선 딜레이
            yield return new WaitForSeconds(curWeapon.data.before_ChargeAttack_DelayRatio * curWeapon.totalAttackSpeed);

            // 공격 애니메이션 및 공격
            animator?.SetBool("IsCharging", isCharging);
            curWeapon.anim?.SetBool("IsCharging", isCharging);
            yield return new WaitForSeconds(attackingTime);

            // 후 딜레이
            yield return new WaitForSeconds(curWeapon.data.after_ChargeAttack_DelayRatio * curWeapon.totalAttackSpeed);
            isChargingAttacking = false;
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
        if (currentTime - lastDownTapTime < doubleTapThreshold && !IsGrounded() && (curWeapon.totalDownAttackCooltime <= Time.time - lastDownAttackTime || lastDownAttackTime == 0))
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

        isDownAttacking = true;

        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // 애니메이션 재생
        animator.SetTrigger("OnDropAttack");
        curWeapon.anim.SetTrigger("OnDropAttack");

        Debug.Log(Time.time);

        //선 딜레이
        yield return new WaitForSeconds(curWeapon.data.before_DownAttack_DelayRatio);

        // 땅에 닿을 때까지 등속 운동
        while (!IsGrounded())
        {
            rb.linearVelocity = Vector2.down * dashPower;
            yield return null; // 매 프레임 유지
        }
        rb.linearVelocity = Vector2.zero;

        // 도착 시 상태 초기화
        rb.gravityScale = originalGravity;

        //후 딜레이
        yield return new WaitForSeconds(curWeapon.data.after_DownAttack_DelayRatio);

        Debug.Log(Time.time);

        lastDownAttackTime = Time.time;

        isDownAttacking = false;

        state.InitStateUI(1, curWeapon.totalDownAttackCooltime); // UI생성

        Debug.Log(curWeapon.totalDownAttackCooltime);
    }

    private bool IsAttacking()
    {
        if (!isNormalAttacking && !isDownAttacking && !isChargingAttacking && !isDashAttacking)
        {
            return false;
        }
        else return true;
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        Debug.Log($"curSkill : {curSkill == null}");

        if (context.performed)
        {
            Debug.Log("SkillKeyDown");
            DoSkill(false);
        }

        else if (context.canceled)
        {
            Debug.Log("SkillKeyUP");
            DoSkill(true);
        }
    }

    public void DoSkill(bool charge)
    {
        //try
        //{
        //    curSkill = DataManager.Instance.skillPrefabList[condition.playerData.skillID].GetComponent<ISkill>();
        //}
        
        //catch
        //{
        //    Debug.Log("경로에 skill이 없음");
        //    if (curSkill == null) return;
        //}

        if (curSkill == null) return;

        if (!charge)
        {
            Debug.Log("SkillCharge");
            startSkillCheckTime = Time.time;

            isSkillHolding = true;

            Debug.Log($"{skillHoldTime}");
            if (skillHoldTime > skillChargeTime)
            {
                isSkillCharging = true;

                StartCoroutine(SkillChargingTimeCheck());
            }
        }
        else
        {
            Debug.Log("OnSkill");

            isSkillHolding = false;
            isSkillCharging = false;

            Debug.Log($"{skillHoldTime}");

            // 차징시간에 따라 일반공격과 차징공격 분리
            if (skillHoldTime > skillChargeTime)
            {
                Debug.Log("ChargingSkill");
                curSkill.UseChargeSkill();
                cooltimeUI.CooltimeFinish();
            }
            else
            {
                Debug.Log("Skill");
                curSkill.UseSkill();
                cooltimeUI.CooltimeFinish();
            }
        }
    }
    public IEnumerator SkillChargingTimeCheck()
    {
        skillChargingTime = Time.time;

        yield return new WaitUntil(() => !isSkillCharging);

        skillChargingTime = Time.time - skillChargingTime;
    }

    public void EquipWeapon(IManualWeapon newWeapon)
    {
        curWeapon = newWeapon;
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

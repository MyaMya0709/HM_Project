using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public EnemyData enemyData;      // 적의 고정 데이터
    public float curHp;

    [Header("Movement Element")]
    public Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    public bool isAirborne = false;
    public bool isKnockback = false;
    public bool isStun = false;

    [Header("Damage Popup")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private Vector3 headOffset = new Vector3(0, 0.3f, 0);
    [SerializeField] private Vector2 PopupPos;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    public Vector2 hitDir;

    public GameObject droppedItemPrepab;
    public Animator animator;
    public GameObject damageEffect;
    public GameObject hitEffect;

    public bool isDead => curHp <= 0;
    public bool isGamePuase = false;

    public event System.Action OnDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
        curHp = enemyData.maxHealth;
        //target = GameManager.Instance.baseCore.AttackPoint;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if ((target.position - transform.position).normalized.x > 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (isDead || target == null) return;
    }
    private void FixedUpdate()
    {
        if (!isDead && target != null && !isStun && !isAirborne && !isKnockback)
        {
            OnMove();
        }
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    // 물리기반 이동이므로 FixedUpdate에서 사용해야함
    private void OnMove()
    {
        if (target == null || isGamePuase) return;

        // 기지를 향해 이동
        Vector2 tarPos = target.position;
        Vector2 curPos = transform.position;

        Vector2 dir = (tarPos - curPos).normalized;
        Vector2 vel = dir * enemyData.moveSpeed;

        if (enemyData.MoveType == EnemyMoveType.Ground)
        {
            rb.linearVelocity = new Vector2(vel.x , rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = vel;
        }

        animator.SetBool("isMove",true);

        //Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        //Vector2 targetPos = rb.position + dir * monsterData.moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(targetPos);
    }

    public void TakeDamage(float Damage, EffectTypeData effectData, Vector2 dir)
    {
        // 공격이 들어온 방향 저장
        hitDir = dir;
        Debug.Log($"{hitDir}");

        // 피격 위치에 가까운 지점 저장
        float hitX;
        if (hitDir.x < 0) hitX = col.ClosestPoint(transform.position + Vector3.right).x;
        else hitX = col.ClosestPoint(transform.position + Vector3.left).x;

        // 가까운 무작위 위치 저장
        hitX += Random.Range(-0.2f, 0.2f);
        float hitY = transform.position.y + Random.Range(-0.2f, 0.2f);
        Vector3 hitPos = new Vector3(hitX, hitY, transform.position.z);

        // 파티클 생성
        GameObject particle = Instantiate(damageEffect, hitPos, transform.rotation);

        // 피격 방향에 따라 반전
        if (hitDir.x < 0) particle.transform.localScale = new Vector3(-1, 1, 1); 
        
        // 루프 off, 1번 재생 후 삭제 설정
        var main = particle.GetComponent<ParticleSystem>().main;
        main.loop = false;
        main.stopAction = ParticleSystemStopAction.Destroy;

        // 데미지 계산
        curHp -= Damage * effectData.damageMultiple;
        if (curHp <= 0)
        {
            //animator.SetTrigger("isDead");
            Dead();
        }

        // 공격의 효과 적용
        ApplyEffect(effectData);

        // 데미지 팝업 띄우기
        SpawnDamagePopup((int)(Damage * effectData.damageMultiple));
    }

    public void SpawnDamagePopup(int damage)
    {
        // 월드좌표 불러오기
        Bounds b = sr.bounds;

        Vector3 topCenter = new Vector3(b.center.x, b.max.y, b.center.z);
        Vector3 spawnPos = topCenter + headOffset;

        var pop = Instantiate(damagePopupPrefab, spawnPos, Quaternion.identity);
        pop.GetComponent<DamagePopup>().Setup(damage);

        //SpawnsDamagePopups.Instance.DamageDone(damage, transform.position, false);
    }

    public IEnumerator OnPause(float time)
    {

        // 움직임 일시정지
        isGamePuase = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        animator.speed = 0f;

        // 대기
        yield return new WaitForSeconds(time);

        // 다시 재생
        isGamePuase = false;
        if(rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
        if(animator != null) animator.speed = 1f;
    }

    #region Effect
    public void ApplyEffect(EffectTypeData effectData)
    {
        if (effectData.knockback.onoff) StartCoroutine(Knockback(hitDir, effectData.knockback.valueA));                                                       // valueA == Power, valueB, valueC
        if (effectData.airborne.onoff) StartCoroutine(Airborne(effectData.airborne.valueA));                                                                     // valueA == Power, valueB, valueC
        if (effectData.stun.onoff) StartCoroutine(TakeStun(effectData.stun.valueB));                                                                        // valueA, valueB == Duration, valueC
        if (effectData.slow.onoff) StartCoroutine(Slow(effectData.slow.valueA, effectData.slow.valueB));                                                    // valueA, valueB == Duration, valueC
        if (effectData.dotDamage.onoff) StartCoroutine(DotDamage(effectData.dotDamage.valueA, effectData.dotDamage.valueB, effectData.dotDamage.valueC));        // valueA == Damage, valueB == Duration, valueC ==  Delay
    }

    public IEnumerator Slow(float amount, float duration)
    {
        float oriSpeed = enemyData.moveSpeed;
        enemyData.moveSpeed *= (100 - amount)/100;
        yield return new WaitForSeconds(duration);
        enemyData.moveSpeed = oriSpeed;
    }

    public IEnumerator DotDamage(float damage, float duration, float delay)
    {
        float i = Time.time + duration;
        while (i >= Time.time)
        {
            curHp -= damage * 0.2f;
            if (curHp <= 0)
            {
                Dead();
            }

            yield return new WaitForSeconds(delay); // 도트데미지 틱 간격
        }
    }

    public IEnumerator Knockback(Vector2 direction, float knockbackDistance)
    {
        isKnockback = true;

        Debug.Log("knockback");
        Debug.Log($"넉백 방향{direction.x}");
        float knockbackPower = 20f;
        Vector2 knockbackDirection = direction;
        Vector2 basePos = rb.position;
        Vector2 targetPos = basePos + knockbackDirection * knockbackDistance;

        //float originalGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // 넉백 거리까지 등속 운동
        while (Vector2.Distance(rb.position, targetPos) > 0.01f)
        {
            targetPos = new Vector2(targetPos.x, rb.position.y);
            Vector2 next = Vector2.MoveTowards(rb.position, targetPos, knockbackPower * Time.fixedDeltaTime);

            RaycastHit2D hit = Physics2D.Raycast(rb.position, knockbackDirection, knockbackPower * Time.fixedDeltaTime, LayerMask.NameToLayer("Obstacle"));
            if (hit)
            {
                Debug.Log("Obstacle hit, break");
                break;
            }

            rb.MovePosition(next);
            yield return new WaitForFixedUpdate();  // 물리 업데이트 주기에 맞추기
        }

        Debug.Log("등속운동 중지");
        //rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        isKnockback = false;
    }

    public IEnumerator TakeStun(float stunDuration)
    {
        Debug.Log("stun");

        isStun = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        isStun = false;
    }

    public IEnumerator Airborne(float airborneForce)
    {
        isAirborne = true;

        // 운동량 0
        rb.linearVelocity = Vector2.zero;

        // 공중에 띄움
        Vector2 dir = new Vector2(0, 1f);
        rb.AddForce(dir * airborneForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => (IsGrounded()));

        isAirborne = false;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    #endregion

    private List<ItemData> GetDroppedItem()
    {
        // 확률에 따라 나올 수 있는 아이템 리스트
        List<ItemData> possibleItem = new List<ItemData>();

        // 나오는 아이템을 리스트에 추가
        foreach (ItemData item in enemyData.dropItemList)
        {
            // 확률 100이면 무조건 추가
            if (item.dropChance == 100)
            {
                possibleItem.Add(item);
                continue;
            }

            // 1-100, 아이템이 나올 확률
            int randomNumber = Random.Range(1, 101);

            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
            }
        }

        // 아이템이 나오는 경우 리스트 
        if (possibleItem.Count > 0)
        {
            return possibleItem;
        }

        Debug.Log("No Item Dropped");
        return null;
    }

    public void InstantiateItem(Vector2 spawnPosition)
    {
        List<ItemData> droppedItems = GetDroppedItem();

        if (droppedItems != null)
        {
            // 각각 아이템의 실체화 과정
            foreach (ItemData item in droppedItems)
            {
                // 실체화 및 아이템의 변수 데이터 이전
                GameObject ItemGameObject = Instantiate(droppedItemPrepab, spawnPosition, Quaternion.identity);
                ItemGameObject.GetComponent<LootableItem>().Init(item);
            }
        }
    }

    public void AttackBase(BaseCore baseCore)
    {
        baseCore.TakeDamage(enemyData.attackPower);
        target = null;
        Dead();
    }

    // [CallerMemberName] string callername에 함수를 호출한 함수의 이름이 들어감
    protected void Dead([CallerMemberName] string callername = null)
    {
        // 호출 함수 이름이 "TakeDamage" 일 때, 아이템 드랍
        //Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            InstantiateItem(transform.position);
        }

        //사망 처리
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        //사망 애니메이션 재생 후 제거
        animator.speed = 1f;
        animator.SetTrigger("isDead");

        OnDeath?.Invoke();
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Base")) return;

        BaseCore baseCore = collision.GetComponent<BaseCore>();
        if (baseCore != null)
        {
            //가까운 피격 지점에 파티클 생성
            Vector3 hitPos = collision.ClosestPoint(transform.position);
            GameObject particle = Instantiate(hitEffect, hitPos, transform.rotation);

            //루프 off, 1번 재생 후 삭제 설정
            var main = particle.GetComponent<ParticleSystem>().main;
            main.loop = false;
            main.stopAction = ParticleSystemStopAction.Destroy;

            AttackBase(baseCore);
        }
    }
}
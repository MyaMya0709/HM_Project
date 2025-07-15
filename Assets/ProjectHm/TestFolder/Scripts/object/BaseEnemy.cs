using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public EnemyData enemyData;      // 적의 고정 데이터
    public float currentHealth;

    public Animator animator;
    public Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private Vector3 headOffset = new Vector3(0, 0.3f, 0);
    [SerializeField] private Vector2 PopupPos;

    public bool isDamage = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isDead => currentHealth <= 0;

    public event System.Action OnDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = enemyData.maxHealth;
        //target = GameManager.Instance.baseCore.AttackPoint;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDead || target == null) return;
    }
    private void FixedUpdate()
    {
        //if (IsDead || curTarget == null || isDamage) return;

        //OnMove();

        if (!isDead && target != null && !isDamage)
        {
            OnMove();
        }
    }

    // 물리기반 이동이므로 FixedUpdate에서 사용해야함
    private void OnMove()
    {
        if (enemyData.MoveType == EnemyMoveType.Ground)
        {
            // 기지를 향해 이동
            Vector2 dir = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(dir.x * enemyData.moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            Vector2 dir = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(dir.x * enemyData.moveSpeed, dir.y * enemyData.moveSpeed);
        }

        //Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        //Vector2 targetPos = rb.position + dir * monsterData.moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(targetPos);
    }

    public void TakeDamage(BaseWeapon WeaponData, Player player)
    {
        currentHealth -= WeaponData.damage;
        if (currentHealth <= 0)
        {
            Dead();
        }

        ApplyEffect(WeaponData.effectData, player);

        SpawnDamagePopup((int)WeaponData.damage);
    }

    public void SpawnDamagePopup(int damage)
    {
        Bounds b = sr.bounds;
        Vector3 topCenter = new Vector3(b.center.x, b.max.y, b.center.z);
        Vector3 spawnPos = topCenter + headOffset;

        var pop = Instantiate(damagePopupPrefab, spawnPos, Quaternion.identity);
        pop.GetComponent<DamagePopup>().Setup(damage);

        //SpawnsDamagePopups.Instance.DamageDone(damage, transform.position, false);
    }

    public void ApplyEffect(WeaponEffectData effectData, Player player)
    {
        if (effectData.Knockback.onoff) StartCoroutine(Knockback(player.lastLookDirection, effectData.Knockback.valueA));                                        // valueA == Power, valueB, valueC
        if (effectData.Airborne.onoff) StartCoroutine(Airborne(effectData.Airborne.valueA));                                                                     // valueA == Power, valueB, valueC
        if (effectData.Stun.onoff) StartCoroutine(TakeStun(effectData.DotDamage.valueB));                                                                        // valueA, valueB == Duration, valueC
        if (effectData.Slow.onoff) StartCoroutine(Slow(effectData.Slow.valueA, effectData.DotDamage.valueB));                                                    // valueA, valueB == Duration, valueC
        if (effectData.DotDamage.onoff) StartCoroutine(DotDamage(effectData.DotDamage.valueA, effectData.DotDamage.valueB, effectData.DotDamage.valueC));        // valueA == Damage, valueB == Duration, valueC ==  Delay
    }

    public IEnumerator Slow(float amount, float duration)
    {
        enemyData.moveSpeed -= amount;
        yield return new WaitForSeconds(duration);
        enemyData.moveSpeed += amount;
    }

    public IEnumerator DotDamage(float damage, float duration, float delay)
    {
        float i = Time.time + duration;
        while (i >= Time.time)
        {
            currentHealth -= damage * 0.2f;
            if (currentHealth <= 0)
            {
                Dead();
            }

            yield return new WaitForSeconds(delay); // 도트데미지 틱 간격
        }
    }

    public IEnumerator Knockback(Vector2 direction, float knockbackDistance)
    {
        Debug.Log("knockback");
        float knockbackPower = 20f;
        Vector2 knockbackDirection = direction;
        Vector2 basePos = rb.position;
        Vector2 targetPos = basePos + knockbackDirection * knockbackDistance;

        // 타겟 초기화로 이동 중지
        Transform saveTar = target;
        target = null;

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

        // 다시 이동
        target = saveTar;
    }

    public IEnumerator TakeStun(float stunDuration)
    {
        isDamage = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        isDamage = false;
    }

    public IEnumerator Airborne(float airborneForce)
    {
        // 타겟 초기화로 이동 중지
        Transform saveTar = target;
        target = null;

        // 운동량 0
        rb.linearVelocity = Vector2.zero;

        // 공중에 띄움
        Vector2 dir = new Vector2(0, 1f);
        rb.AddForce(dir * airborneForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => (IsGrounded()));

        target = saveTar;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Base")) return;

        BaseCore baseCore = collision.GetComponent<BaseCore>();
        if (baseCore != null)
        {
            AttackBase(baseCore);
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
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemDropList>().InstantiateItem(transform.position);
        }

        //사망 처리
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        
        //animator?.SetTrigger("Dead");
        //사망 애니메이션 재생 후 제거

        OnDeath?.Invoke();
        Destroy(gameObject, 1.5f);
        // 이펙트나 드랍 추가 가능
    }
}
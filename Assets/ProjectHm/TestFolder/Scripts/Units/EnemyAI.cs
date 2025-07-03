using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public UnitStats stats;

    public float currentHealth;
    public Animator animator;

    public Transform curTarget;
    private Rigidbody2D rb;
    public bool isDamage = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isDead => currentHealth <= 0;

    public event System.Action OnDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = stats.maxHealth;
        GameObject baseObj = GameObject.FindWithTag("Base");
        if (baseObj != null)
            curTarget = baseObj.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead || curTarget == null) return;
    }
    private void FixedUpdate()
    {
        //if (IsDead || curTarget == null || isDamage) return;

        //OnMove();

        if (!isDead && curTarget != null && !isDamage)
        {
            OnMove();
        }
    }

    // ������� �̵��̹Ƿ� FixedUpdate���� ����ؾ���
    private void OnMove()
    {
        // ������ ���� �̵�
        Vector2 dir = (curTarget.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * stats.moveSpeed, rb.linearVelocity.y);

        //Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        //Vector2 targetPos = rb.position + dir * stats.moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(targetPos);
    }

    public void TakeDamage(BaseEffectData effectData, PlayerController player)
    {
        currentHealth -= effectData.damage;
        if (currentHealth <= 0)
        {
            Dead();
        }

        ApplyEffect(effectData, player);
    }

    public void ApplyEffect(BaseEffectData effectData, PlayerController player)
    {
        if (effectData.Knockback.onoff) StartCoroutine(Knockback(player.lastLookDirection, effectData.Knockback.valueA));                                        // valueA == Power, valueB, valueC
        if (effectData.Airborne.onoff) StartCoroutine(Airborne(effectData.Airborne.valueA));                                                                     // valueA == Power, valueB, valueC
        if (effectData.Stun.onoff) StartCoroutine(TakeStun(effectData.DotDamage.valueB));                                                                        // valueA, valueB == Duration, valueC
        if (effectData.Slow.onoff) StartCoroutine(Slow(effectData.Slow.valueA, effectData.DotDamage.valueB));                                                    // valueA, valueB == Duration, valueC
        if (effectData.DotDamage.onoff) StartCoroutine(DotDamage(effectData.DotDamage.valueA, effectData.DotDamage.valueB, effectData.DotDamage.valueC));        // valueA == Damage, valueB == Duration, valueC ==  Delay
    }

    public IEnumerator Slow(float amount, float duration)
    {
        stats.moveSpeed -= amount;
        yield return new WaitForSeconds(duration);
        stats.moveSpeed += amount;
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

            yield return new WaitForSeconds(delay); // ��Ʈ������ ƽ ����
        }
    }

    public IEnumerator Knockback(Vector2 direction, float knockbackDistance)
    {
        Debug.Log("knockback");
        float knockbackPower = 20f;
        Vector2 knockbackDirection = direction;
        Vector2 basePos = rb.position;
        Vector2 targetPos = basePos + knockbackDirection * knockbackDistance;

        // Ÿ�� �ʱ�ȭ�� �̵� ����
        Transform saveTar = curTarget;
        curTarget = null;

        //float originalGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        // �˹� �Ÿ����� ��� �
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
            yield return new WaitForFixedUpdate();  // ���� ������Ʈ �ֱ⿡ ���߱�
        }

        Debug.Log("��ӿ ����");
        //rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        // �ٽ� �̵�
        curTarget = saveTar;
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
        // Ÿ�� �ʱ�ȭ�� �̵� ����
        Transform saveTar = curTarget;
        curTarget = null;

        // ��� 0
        rb.linearVelocity = Vector2.zero;

        // ���߿� ���
        Vector2 dir = new Vector2(0, 1f);
        rb.AddForce(dir * airborneForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => (IsGrounded()));

        curTarget = saveTar;
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

        baseCore.TakeDamage(stats.attackPower);
        curTarget = null;
        Dead();
    }

    // [CallerMemberName] string callername�� �Լ��� ȣ���� �Լ��� �̸��� ��
    protected void Dead([CallerMemberName] string callername = null)
    {
        // ȣ�� �Լ� �̸��� "TakeDamage" �� ��, ������ ���
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemLootingList>().InstantiateItem(transform.position);
        }

        //��� ó��
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        
        //animator?.SetTrigger("Dead");
        //��� �ִϸ��̼� ��� �� ����

        OnDeath?.Invoke();
        Destroy(gameObject, 1.5f);
        // ����Ʈ�� ��� �߰� ����
    }
}


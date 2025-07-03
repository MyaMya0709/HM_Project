using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : UnitBase
{
    public Transform curTarget;
    private Rigidbody2D rb;
    public bool isDamage = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private void Start()
    {
        GameObject baseObj = GameObject.FindWithTag("Base");
        if (baseObj != null)
            curTarget = baseObj.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsDead || curTarget == null) return;
    }
    private void FixedUpdate()
    {
        //if (IsDead || curTarget == null || isDamage) return;

        //OnMove();

        if (!IsDead && curTarget != null && !isDamage)
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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public IEnumerator TakeStun(float stunDuration)
    {
        isDamage = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        isDamage = false;
    }

    public IEnumerator AirBorne(float knockbackForce)
    {
        // Ÿ�� �ʱ�ȭ�� �̵� ����
        Transform saveTar = curTarget;
        curTarget = null;

        // ��� 0
        rb.linearVelocity = Vector2.zero;

        // ���߿� ���
        Vector2 dir = new Vector2(0, 1f);
        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

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
    protected override void Dead([CallerMemberName] string callername = null)
    {
        // ȣ�� �Լ� �̸��� "TakeDamage" �� ��, ������ ���
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemLootingList>().InstantiateItem(transform.position);
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        base.Dead();
        // ����Ʈ�� ��� �߰� ����
    }
}


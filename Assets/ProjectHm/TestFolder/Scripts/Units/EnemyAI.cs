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

    // 물리기반 이동이므로 FixedUpdate에서 사용해야함
    private void OnMove()
    {
        // 기지를 향해 이동
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
        // 타겟 초기화로 이동 중지
        Transform saveTar = curTarget;
        curTarget = null;

        // 운동량 0
        rb.linearVelocity = Vector2.zero;

        // 공중에 띄움
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

    // [CallerMemberName] string callername에 함수를 호출한 함수의 이름이 들어감
    protected override void Dead([CallerMemberName] string callername = null)
    {
        // 호출 함수 이름이 "TakeDamage" 일 때, 아이템 드랍
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemLootingList>().InstantiateItem(transform.position);
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        base.Dead();
        // 이펙트나 드랍 추가 가능
    }
}


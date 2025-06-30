using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAI : UnitBase
{
    private Transform target;

    private void Start()
    {
        GameObject baseObj = GameObject.FindWithTag("Base");
        if (baseObj != null)
            target = baseObj.transform;
    }

    private void Update()
    {
        if (IsDead || target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * stats.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Base"))
        {
            BaseCore baseCore = collision.GetComponent<BaseCore>();
            if (baseCore != null)
            {
                AttackBase(baseCore);
            }
        }
    }

    public void AttackBase(BaseCore baseCore)
    {

        baseCore.TakeDamage(stats.attackPower);
        target = null;
        Dead();
    }

    protected override void Dead([CallerMemberName] string callername = null)
    {

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


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
<<<<<<< HEAD
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemLootList>().InstantiateItem(transform.position);
        }
=======
<<<<<<< Updated upstream
=======
        Debug.Log($"Dead Called From {callername}");
        if (callername == "TakeDamage")
        {
            GetComponent<ItemLootingList>().InstantiateItem(transform.position);
        }
>>>>>>> Stashed changes
>>>>>>> 4c7adf8 ([Wip] ì˜¤ë¥˜ ë³µêµ¬ ìž„ì‹œ ì €ìž¥)
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        base.Dead();
        // ÀÌÆåÆ®³ª µå¶ø Ãß°¡ °¡´É
    }
}


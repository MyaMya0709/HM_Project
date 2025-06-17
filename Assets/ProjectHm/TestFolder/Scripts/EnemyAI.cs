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

    //기지클래스 생성 시 활성화
    //public void AttackBase(BaseCore baseCore)
    //{
    //    baseCore.TakeDamage(stats.attackPower);
    //}

    protected override void Dead()
    {
        base.Dead();
        // 이펙트나 드랍 추가 가능
    }
}


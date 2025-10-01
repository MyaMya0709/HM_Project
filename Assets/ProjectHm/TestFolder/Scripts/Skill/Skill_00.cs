using System.Collections.Generic;
using UnityEngine;

public class Skill_00 : ISkill
{
    public List<BaseEnemy> targetList;
    public float range;
    public List<Vector2> explosionPoints;
    public float damage = 50f;
    // knockback , airborne , stun , slow , dotDamage;
    public EffectTypeData effect = new EffectTypeData();

    public override void UseSkill()
    {




    }


    public void FindPoint(Vector2 center)
    {

        for (int i = 0; i < 10; i++)
        {
            // 범위내의 무작위 지점 선택, 10-20 사이의 반지름 지정, 원형내 오브젝트 체크
            Collider2D[] hits = Physics2D.OverlapCircleAll(Random.insideUnitCircle * range + center, Random.Range(10, 20));

            foreach (var hit in hits)
            {
                // 적 태그 체크
                if (hit.CompareTag("Enemy"))
                {
                    // Enemy 스크립트에서 TakeDamage 호출
                    hit.GetComponent<BaseEnemy>()?.TakeDamage(damage, effect, (Vector2)hit.transform.position - center);
                }
            }
        }
        
    } 
}

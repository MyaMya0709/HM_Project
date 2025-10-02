using System.Collections.Generic;
using System.Drawing;
using System.Net;
using UnityEngine;

public class Skill_00 : ISkill
{
    public PlayerCondition player;

    public int subExplosionsCount = 4;          // 소폭발 개수
    public float minSubRadius = 2f;             // 소폭발 최소 반지름
    public float maxSubRadius = 3f;             // 소폭발 최대 반지름
    public float subDamage = 50f;               // 소폭발 데미지
    public float finalRadius = 6f;              // 중심 범위, 대폭발 반지름
    public float finalDamage = 200f;            // 대폭발 데미지

    public LayerMask enemyLayerMask;

    // damageMultiple, knockback , airborne , stun , slow , dotDamage;
    public EffectTypeData effect;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        effect.damageMultiple = 1;
    }

    public override void UseSkill()
    {
        // 스킬 중심지 설정
        Vector2 center = player.transform.position;

        // 소폭발 중심지 설정및, 데미지 구현
        for (int i = 0; i < subExplosionsCount; i++)
        {
            // 소폭발 중심지
            Vector2 point = FindPoint(center, finalRadius);

            // 소폭발 반지름
            float r = Random.Range(minSubRadius, maxSubRadius);

            // 소폭발 데미지
            Explosion(point, r, subDamage);
        }

        // 대폭발 데미지
        Explosion(center, finalRadius, finalDamage);
    }

    public Vector2 FindPoint(Vector2 center, float radius)
    {
        // Mathf.Sqrt(Random.value) => 제곱근을 씌워서, 반지름 방향으로 균등하게 분포
        // 중심으로부터의 거리 = r
        float r = Mathf.Sqrt(Random.value) * radius;

        // 무작위 각도 지정, 2*Mathf.PI == 360도
        float theta = Random.Range(0f, 2 * Mathf.PI);

        // 삼각함수를 이용한 중심으로부터의 위치값
        Vector2 offset = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;

        // 현재 위치 기준으로 포인트 좌표 찾기
        Vector2 point = center + offset;

        return point;
    }

    public void Explosion(Vector2 center, float radius, float damage)
    {
        // 원형내 오브젝트 체크
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius, enemyLayerMask);

        foreach (var hit in hits)
        {
            // 적 태그 체크
            if (hit.CompareTag("Enemy"))
            {
                // Enemy 스크립트에서 TakeDamage 호출
                //((Vector2)hit.transform.position - center).normalized => 공격 방향
                hit.GetComponent<BaseEnemy>()?.TakeDamage(damage, effect, ((Vector2)hit.transform.position - center).normalized);
            }
        }
    }
}

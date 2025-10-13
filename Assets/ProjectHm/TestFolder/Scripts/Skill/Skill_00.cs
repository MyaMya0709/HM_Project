using UnityEngine;

public class Skill_00 : ISkill
{
    public int subExplosionsCount = 4;          // 소폭발 개수
    public float minSubRadius = 2f;             // 소폭발 최소 반지름
    public float maxSubRadius = 3f;             // 소폭발 최대 반지름
    public float subDamage = 50f;               // 소폭발 데미지
    public float finalRadius = 6f;              // 중심 범위, 대폭발 반지름
    public float finalDamage = 200f;            // 대폭발 데미지

    public LayerMask enemyLayerMask;

    public override void UseChargeSkill()
    {
        Debug.Log("UseChargeSkill");
        UseSkill();
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

        //사용된 스킬 제거
        DestroySkill();
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
        Debug.Log("Explosion");
        // 원형내 오브젝트 체크
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius, enemyLayerMask);

        foreach (var hit in hits)
        {
            // 적 태그 체크
            if (hit.CompareTag("Enemy"))
            {
                //((Vector2)hit.transform.position - center).normalized => 공격 방향
                hit.GetComponent<BaseEnemy>()?.TakeDamage(damage, skillData.effect, ((Vector2)hit.transform.position - center).normalized);
            }
        }
        DrawCircle(center, radius);
    }

    public override void SkillRangeCheck()
    {
        Debug.Log("범위 체크");
    }

    /// <summary>
    /// 2D용 원형 디버그 표시 (Scene/Game 뷰 모두 표시됨)
    /// </summary>
    /// <param name="center">원의 중심 (Vector2)</param>
    /// <param name="radius">반지름</param>
    /// <param name="color">선 색상</param>
    /// <param name="duration">유지 시간 (초)</param>
    /// <param name="segments">원형 세그먼트 수 (높을수록 부드러움)</param>
    public static void DrawCircle(Vector2 center, float radius, float duration = 0.5f, int segments = 36)
    {
        Debug.Log("디버깅용 범위 표시");
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector2(radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            // n도 => n라디안
            float angle = angleStep * i * Mathf.Deg2Rad;

            //중심이 center이고, 반지름이 radius인 원의 둘레 중 angle 각도에 해당하는 점의 좌표
            Vector3 nextPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            Debug.DrawLine(prevPoint, nextPoint, Color.red, duration);
            prevPoint = nextPoint;
        }
    }
}
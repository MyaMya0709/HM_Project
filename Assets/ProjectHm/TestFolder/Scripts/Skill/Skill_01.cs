using System.Collections;
using UnityEngine;

public class Skill_01 : ISkill
{
    public float stopTimer = 5f;

    public SpawnManager manager;
    public Object[] enemies;


    private void Start()
    {
        manager = FindFirstObjectByType<SpawnManager>();
    }

    public override void UseChargeSkill()
    {
        Debug.Log("UseChargeSkill");
        UseSkill();
    }

    public override void UseSkill()
    {
        Debug.Log("UseSkill");
        StartCoroutine(TimeStop());

    }

    public override void SkillRangeCheck()
    {
        Debug.Log("범위 체크");
    }

    public IEnumerator TimeStop()
    {
        Debug.Log("스포너 정지");
        //스포너 일시정지 함수 실행
        manager = FindFirstObjectByType<SpawnManager>();

        Debug.Log($"{manager == null}");
        StartCoroutine(manager.OnPause(stopTimer));

        //스폰된 모든 적 찾기
        enemies = FindObjectsByType<BaseEnemy>(FindObjectsSortMode.None);

        //스폰된 적 일시정지 함수 실행
        foreach (BaseEnemy enemy in enemies)
        {
            StartCoroutine(enemy.OnPause(stopTimer));
        }

        yield return new WaitForSeconds(stopTimer);

        //사용된 스킬 제거
        DestroySkill();
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
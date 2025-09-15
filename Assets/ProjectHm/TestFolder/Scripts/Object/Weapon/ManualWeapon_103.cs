using System.Collections.Generic;
using UnityEngine;

public class ManualWeapon_103 : IManualWeapon
{

    public float AttackStunDur = 0.1f;
    public float DownAtkStunDur = 0.25f;
    public float DashAtkStunDur = 0.25f;

    public float chargeTimeLevel2 = 0.6f;         // 차징 2단계 시간
    public float chargeTimeLevel3 = 1.0f;         // 차징 3단계 시간
    public int chargeLevel = 0;                   // 차징 단계
    public float chargeAttackMultiple = 0;        // 차징 단계별 공격력 배수

    public bool isDashAttack = true;

    [Header("Weapon Effect Check")]
    public bool mutipleAttack = false;
    public bool isStun = false;

    [Header("Effects")]
    public GameObject hitEffect;

    public override void Attack()
    {
        Debug.Log("Attack");
        AttackParticle();
        if (!mutipleAttack)
        {
            SingleAttack();
        }
        else
        {
            MutipleAttack();
        }
    }

    public override void DownAttack()
    {
        // 공격 범위 내의 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, totalRange, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            if (enemyCollider.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
            {
                Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
                enemy.TakeDamage(totalAttackPower, data.baseWeaponEffectList[baseWeaponLevel].dropEffect, playerController.lastLookDirection);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(DownAtkStunDur));
                }
            }
        }
    }

    public void ChargingLevel()
    {
        // 차징 단계 확인
        if (playerController.holdTime >= chargeTimeLevel3)
        {
            chargeLevel = 3;
            chargeAttackMultiple = 1.0f;
        }
        else if (playerController.holdTime >= chargeTimeLevel2)
        {
            chargeLevel = 2;
            chargeAttackMultiple = 0.6f;
        }
        else
        {
            chargeLevel = 1;
            chargeAttackMultiple = 0.3f;
        }
        Debug.Log($"Hold: {playerController.holdTime:F2}s → Level {chargeLevel}");
    }

    public override void ChargingAttack()
    {
        Debug.Log("ChargingAttack");

        ChargingLevel();
        //totalDamage *= chargeLevel;
        //effectData.Airborne.onoff = true;
        //effectData.Airborne.valueA *= chargeLevel;

        Vector2 size = new Vector2(0.1f, 1f);                        // 날려보낼 박스 크기
        Vector2 origin = attackPoint.position;                       // 출발점
        Vector2 direction = playerController.lastLookDirection;      // 방향

        // 공격 범위 내의 적 감지
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0f, direction, totalRange * chargeLevel, enemyLayer);

        // 데미지 부여
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
            {
                enemy.TakeDamage( totalAttackPower * chargeAttackMultiple * data.baseWeaponEffectList[baseWeaponLevel].chargeEffect.damageMultiple, data.baseWeaponEffectList[baseWeaponLevel].chargeEffect, playerController.lastLookDirection);
            }
        }

        // ▶ 범위 디버그 사각형 시각화 (게임 씬에서도 보임)
        DrawDebugBox((Vector2)attackPoint.position + direction * (totalRange * 0.5f * chargeLevel), new Vector2(totalRange * chargeLevel, 1.0f), Color.red, 3f);

        //totalDamage /= chargeLevel;
        //effectData.Airborne.onoff = false;
        //effectData.Airborne.valueA /= chargeLevel;
        //chargeLevel = 0;

        // 디버그용 로그
        Debug.Log($"Attack hit {hits.Length} enemies.");
    }

    public override void DashAttack()
    {
        if (!isDashAttack) return;

        Vector2 startPos = playerController.basePos;
        Vector2 endPos = playerController.rb.position;
        Vector2 center = new Vector2(((startPos + endPos) / 2f).x, attackPoint.position.y);
        float dashDis = Vector2.Distance(startPos, endPos);
        Vector2 boxsize = new Vector2(dashDis, 1f); // 넓이 = 대시거리

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, boxsize, 0f, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            // 접근 가능 여부 판단
            if (hit.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
            {
                enemy.TakeDamage(totalAttackPower, data.baseWeaponEffectList[baseWeaponLevel].dashEffect, playerController.lastLookDirection);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(DashAtkStunDur));
                }
            }
        }

        // ▶ 범위 디버그 사각형 시각화 (게임 씬에서도 보임)
        DrawDebugBox(center, boxsize, Color.red, 3f);

        // 디버그용 로그
        Debug.Log($"Attack hit {hits.Length} enemies.");
    }

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        ////공격 범위에서 보는 방향으로 가까운 적 감지
        //RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection, attackRange, enemyLayer);

        //if (hit.collider != null)
        //{
        //    if (hit.collider.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        //    {
        //        Debug.Log($"Attack enemies.");
        //        enemy.TakeDamage(this, playerController);
        //        if (isStun)
        //        {
        //            StartCoroutine(enemy.TakeStun(AttackStunDur));
        //        }
        //    }
        //    DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.green);
        //}
        //else
        //{
        //    DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.red);
        //}

        Vector2 size = new Vector2(0.1f, 1f);                        // 날려보낼 박스 크기
        Vector2 origin = attackPoint.position;                       // 출발점
        Vector2 direction = playerController.lastLookDirection;      // 방향

        // 공격 범위 내의 적 감지
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, totalRange, enemyLayer);

        // 데미지 부여
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
            {
                enemy.TakeDamage(totalAttackPower, data.baseWeaponEffectList[baseWeaponLevel].attackEffect, playerController.lastLookDirection);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(AttackStunDur));
                }
                // ▶ 범위 디버그 사각형 시각화 (게임 씬에서도 보임)
                DrawDebugBox((Vector2)attackPoint.position + direction * (totalRange * 0.5f), new Vector2(totalRange, 1.0f), Color.green, 3f);
            }
        }
        else
        {
            // ▶ 범위 디버그 사각형 시각화 (게임 씬에서도 보임)
            DrawDebugBox((Vector2)attackPoint.position + direction * (totalRange * 0.5f), new Vector2(totalRange, 1.0f), Color.red, 3f);
        }
    }

    public void MutipleAttack()
    {
        Debug.Log("MutipleAttack");

        // 공격 범위 내의 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, totalRange, enemyLayer);

        // 보는 방향
        Vector2 forward = playerController.lastLookDirection;

        foreach (var enemyCollider in hitEnemies)
        {
            Vector2 toTarget = (enemyCollider.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(forward, toTarget);

            if (dot > 0) //0보다 크면 정면
            {
                if (enemyCollider.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
                {
                    // 디버그용 로그
                    Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
                    enemy.TakeDamage(totalAttackPower, data.baseWeaponEffectList[baseWeaponLevel].attackEffect, playerController.lastLookDirection);
                    if (isStun)
                    {
                        StartCoroutine(enemy.TakeStun(AttackStunDur));
                    }
                }
            }
        }
    }

    public void AttackParticle()
    {
        // 생성 위치
        Transform holder = attackPoint;

        // 좌우 방향에 따른 위치 조절 및 생성, 좌우 반전
        GameObject particle = Instantiate(data.attackParticle1, holder);
        particle.transform.localPosition = (Vector3.left * totalRange);
        particle.transform.localScale = new Vector3(-1, 1, 1);

        // 1번 재생 후 삭제, 루프 off, 크기 조절 및 스케일링 모드 설정
        var main = particle.GetComponent<ParticleSystem>().main;
        main.playOnAwake = false;
        main.simulationSpeed = totalAttackSpeed;
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.loop = false;
        main.startSize = 2.5f;

        particle.GetComponent<ParticleSystem>().Play();
    }
    public void DownAttackParticle()
    {
        // 좌우 방향에 따른 위치 조절 및 생성, 좌우 반전
        GameObject particle = Instantiate(data.attackParticle2, playerCondition.transform.position, playerCondition.transform.rotation);

        // 1번 재생 후 삭제, 루프 off, 크기 조절 및 스케일링 모드 설정
        var main = particle.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.loop = false;
        main.startSize = 2f;
    }
    public void ChargeAttackParticle()
    {
        // 생성 위치
        Transform holder = attackPoint;

        // 좌우 방향에 따른 위치 조절 및 생성, 좌우 반전
        GameObject particle = Instantiate(data.attackParticle3, holder);
        particle.transform.localPosition = (Vector3.left * totalRange);
        particle.transform.localScale = new Vector3(-1, 1, 1);

        // 1번 재생 후 삭제, 루프 off, 크기 조절 및 스케일링 모드 설정
        var main = particle.GetComponent<ParticleSystem>().main;
        main.playOnAwake = false;
        main.simulationSpeed = totalAttackSpeed;
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.loop = false;
        main.startSize = 2f;

        particle.GetComponent<ParticleSystem>().Play();
    }
    public void DashAttackParticle()
    {
        if (isDashAttack)
        {// 생성 위치
            Transform holder = attackPoint;

            // 좌우 방향에 따른 위치 조절 및 생성, 좌우 반전
            GameObject particle = Instantiate(data.attackParticle4, holder);
            particle.transform.localPosition = (Vector3.left * totalRange);
            particle.transform.localScale = new Vector3(-1, 1, 1);

            // 1번 재생 후 삭제, 루프 off, 크기 조절 및 스케일링 모드 설정
            var main = particle.GetComponent<ParticleSystem>().main;
            main.playOnAwake = false;
            main.simulationSpeed = totalAttackSpeed;
            main.stopAction = ParticleSystemStopAction.Destroy;
            main.loop = false;
            main.startSize = 2f;

            particle.GetComponent<ParticleSystem>().Play();
        }
    }

    private void DrawDebugBox(Vector2 center, Vector2 size, Color color, float duration)
    {
        Vector2 half = size * 0.5f;

        Vector2 topLeft = center + new Vector2(-half.x, half.y);
        Vector2 topRight = center + new Vector2(half.x, half.y);
        Vector2 bottomLeft = center + new Vector2(-half.x, -half.y);
        Vector2 bottomRight = center + new Vector2(half.x, -half.y);

        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }

    private void DrawSingleLine(Vector2 attatckPoint, Vector2 LookDir, float duration, Color color)
    {
        Debug.DrawLine(attatckPoint, attatckPoint + LookDir.normalized * totalRange, color, duration);
    }
}
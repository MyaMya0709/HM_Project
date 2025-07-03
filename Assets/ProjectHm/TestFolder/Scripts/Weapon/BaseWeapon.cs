
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    public BaseEffectData effectData;

    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRange = 1.2f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float airborneForce = 5f;
    public float AttackStunDur = 1f;
    public float DownAtkStunDur = 1f;
    public float DashAtkStunDur = 1f;

    public float chargeTimeLevel1 = 0.5f;         // ��¡ 1�ܰ� �ð�
    public float chargeTimeLevel2 = 1.0f;         // ��¡ 2�ܰ� �ð�
    public float chargeTimeLevel3 = 1.5f;         // ��¡ 3�ܰ� �ð�
    public int chargeLevel;                     // ��¡ �ܰ�

    [Header("Weapon Effect Check")]
    public bool mutipleAttack = false;
    public bool isStun = true;

    [Header("Effects")]
    public GameObject hitEffect;

    public PlayerController playerController;
    public SpriteRenderer sr;
    public bool facingRight = true;


    private void Start()
    {
        playerController = GetComponentInParent< PlayerController>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerController.lastLookDirection.x < 0 && facingRight)
        {
            Flip();
        }
        else if (playerController.lastLookDirection.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        if (!mutipleAttack)
        {
            SingleAttack();
        }
        else
        {
            MutipleAttack();
        }
    }

    public void DownAttack()
    {
        // ���� ���� ���� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            if (enemyCollider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            {
                Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
                enemy.TakeDamage(effectData, playerController);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(DownAtkStunDur));
                }
            }
        }
    }

    public void ChargingLevel()
    {
        // ��¡ �ܰ� Ȯ��
        if (playerController.holdTime >= chargeTimeLevel3) chargeLevel = 3;
        else if (playerController.holdTime >= chargeTimeLevel2) chargeLevel = 2;
        else if (playerController.holdTime >= chargeTimeLevel1) chargeLevel = 1;
        else chargeLevel = 0;
        Debug.Log($"Hold: {playerController.holdTime:F2}s �� Level {chargeLevel}");
    }

    public void ChargingAttack()
    {
        Debug.Log("ChargingAttack");

        ChargingLevel();
        effectData.damage *= chargeLevel;
        effectData.Airborne.onoff = true;
        effectData.Airborne.valueA *= chargeLevel;

        Vector2 size = new Vector2(0.1f, 1f);                        // �������� �ڽ� ũ��
        Vector2 origin = attackPoint.position;                       // �����
        Vector2 direction = playerController.lastLookDirection;      // ����

        // ���� ���� ���� �� ����
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0f, direction, attackRange * chargeLevel, enemyLayer);

        // ������ �ο�
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            {
                enemy.TakeDamage(effectData,playerController);
            }
        }

        // �� ���� ����� �簢�� �ð�ȭ (���� �������� ����)
        DrawDebugBox((Vector2)attackPoint.position + direction * (attackRange * 0.5f * chargeLevel), new Vector2(attackRange * chargeLevel, 1.0f), Color.red, 3f);

        effectData.damage /= chargeLevel;
        effectData.Airborne.onoff = false;
        effectData.Airborne.valueA /= chargeLevel;
        chargeLevel = 0;

        // ����׿� �α�
        Debug.Log($"Attack hit {hits.Length} enemies.");
    }

    public void DashAttack()
    {
        Vector2 startPos = playerController.basePos;
        Vector2 endPos = playerController.rb.position;
        Vector2 center = new Vector2 (((startPos + endPos) / 2f).x, attackPoint.position.y);
        float dashDis = Vector2.Distance(startPos, endPos);
        Vector2 boxsize = new Vector2(dashDis, 1f); // ���� = ��ðŸ�

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, boxsize, 0f, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            // ���� ���� ���� �Ǵ�
            if (hit.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            {
                enemy.TakeDamage(effectData, playerController);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(DashAtkStunDur));
                }
            }
        }

        // �� ���� ����� �簢�� �ð�ȭ (���� �������� ����)
        DrawDebugBox(center, boxsize, Color.red, 3f);

        // ����׿� �α�
        Debug.Log($"Attack hit {hits.Length} enemies.");
    }

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        //���� �������� ���� �������� ����� �� ����
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            {
                Debug.Log($"Attack enemies.");
                enemy.TakeDamage(effectData, playerController);
                if (isStun)
                {
                    StartCoroutine(enemy.TakeStun(AttackStunDur));
                }
            }
            DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.green);
        }
        else
        {
            DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.red);
        }
    }

    public void MutipleAttack()
    {
        Debug.Log("MutipleAttack");

        // ���� ���� ���� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // ���� ����
        Vector2 forward = playerController.lastLookDirection;

        foreach (var enemyCollider in hitEnemies)
        {
            Vector2 toTarget = (enemyCollider.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(forward, toTarget);

            if (dot > 0) //0���� ũ�� ����
            {
                if (enemyCollider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
                {
                    // ����׿� �α�
                    Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
                    enemy.TakeDamage(effectData, playerController);
                    if (isStun)
                    {
                        StartCoroutine(enemy.TakeStun(AttackStunDur));
                    }
                }
            }
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
        Debug.DrawLine(attatckPoint, attatckPoint + LookDir.normalized * attackRange, color, duration);
    }

    void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }
}

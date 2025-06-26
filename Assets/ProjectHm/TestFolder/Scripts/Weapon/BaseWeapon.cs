
using UnityEngine;
using UnityEngine.UIElements;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRange = 1.2f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public bool mutipleAttack = false;
    public float knockbackForce = 5f;

    [Header("Effects")]
    public GameObject hitEffect;

    public PlayerController playerController;
    public SpriteRenderer sr;
    public bool facingRight = true;


    private void Start()
    {
        playerController = GetComponentInParent< PlayerController>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponent<SpriteRenderer>();
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
        // 공격 범위 내의 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            ToEnemyDamage(enemyCollider);
            // 디버그용 로그
            Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
        }
    }

    public void ChargingAttack()
    {
        Debug.Log("ChargingAttack");

        // 좌우 방향 감지
        Vector2 attackOffset = facingRight ? Vector2.right : Vector2.left;
        // 사각형 공격 범위의 중심점
        Vector2 center = (Vector2)attackPoint.position + attackOffset * (attackRange * 0.5f);
        Vector2 size = new Vector2(attackRange, 1.0f);

        // 공격 범위 내의 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
            center,
            size,
            0f,
            enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            ToEnemyDamage(enemyCollider);

            GameObject enemy = enemyCollider.gameObject;
            // 차징 넉백
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            // 공격 방향 + 수직 방향 추가
            Vector2 dir = new Vector2 ((rb.position - (Vector2)attackPoint.position).x, 1f).normalized;
            rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }

        // ▶ 범위 디버그 사각형 시각화 (게임 씬에서도 보임)
        DrawDebugBox(center, size, Color.red, 3f);

        // 디버그용 로그
        Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
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

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        //공격 범위에서 보는 방향으로 가까운 적 감지
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            ToEnemyDamage(hit.collider);
            Debug.Log($"Attack enemies.");
            DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.green);
        }
        DrawSingleLine(attackPoint.position, playerController.lastLookDirection, 3f, Color.red);
    }

    private void DrawSingleLine(Vector2 attatckPoint, Vector2 LookDir, float duration, Color color)
    {
        Debug.DrawLine(attatckPoint, attatckPoint + LookDir.normalized * attackRange, color, duration);
    }

    public void MutipleAttack()
    {
        Debug.Log("MutipleAttack");

        // 공격 범위 내의 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // 보는 방향
        Vector2 forward = playerController.lastLookDirection;

        foreach (var enemyCollider in hitEnemies)
        {
            Vector2 toTarget = (enemyCollider.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(forward, toTarget);

            if (dot > 0) //0보다 크면 정면
            {
                ToEnemyDamage(enemyCollider);
                // 디버그용 로그
                Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
            }
        }
    }
    private void ToEnemyDamage(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            UnitBase unit = collision.GetComponent<UnitBase>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
            }
        }
    }

    void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }

}

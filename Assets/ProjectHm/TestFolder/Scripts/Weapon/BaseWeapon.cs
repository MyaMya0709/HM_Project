
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
        // ���� ���� ���� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            ToEnemyDamage(enemyCollider);
            // ����׿� �α�
            Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
        }
    }

    public void ChargingAttack()
    {
        Debug.Log("ChargingAttack");

        // �¿� ���� ����
        Vector2 attackOffset = facingRight ? Vector2.right : Vector2.left;
        // �簢�� ���� ������ �߽���
        Vector2 center = (Vector2)attackPoint.position + attackOffset * (attackRange * 0.5f);
        Vector2 size = new Vector2(attackRange, 1.0f);

        // ���� ���� ���� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
            center,
            size,
            0f,
            enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            ToEnemyDamage(enemyCollider);

            GameObject enemy = enemyCollider.gameObject;
            // ��¡ �˹�
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            // ���� ���� + ���� ���� �߰�
            Vector2 dir = new Vector2 ((rb.position - (Vector2)attackPoint.position).x, 1f).normalized;
            rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }

        // �� ���� ����� �簢�� �ð�ȭ (���� �������� ����)
        DrawDebugBox(center, size, Color.red, 3f);

        // ����׿� �α�
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
        //���� �������� ���� �������� ����� �� ����
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
                ToEnemyDamage(enemyCollider);
                // ����׿� �α�
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

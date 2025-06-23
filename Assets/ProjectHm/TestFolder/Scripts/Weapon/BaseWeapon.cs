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
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection, attackRange);
        Vector3 end = hit ? hit.point : (attackPoint.position + (Vector3)playerController.lastLookDirection * attackRange);
        Debug.DrawLine(attackPoint.position, end, hit ? Color.green : Color.red, 1f * Time.deltaTime, false);

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

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        //���� �������� ���� �������� ����� �� ����
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection, attackRange, enemyLayer);
        Color col = hit.collider ? Color.green : Color.red;
        Debug.DrawLine(attackPoint.position, hit ? (Vector2)hit.point : (Vector2)attackPoint.position + playerController.lastLookDirection * attackRange, col, 1f);

        if (hit.collider != null)
        {
            ToEnemyDamage(hit.collider);
            Debug.Log($"Attack enemies.");
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

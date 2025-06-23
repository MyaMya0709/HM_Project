using UnityEngine;

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

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        //공격 범위에서 보는 방향으로 가까운 적 감지
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
}

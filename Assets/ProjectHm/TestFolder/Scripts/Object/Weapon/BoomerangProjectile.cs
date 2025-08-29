using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    public float speed = 8f;           // 이동 속도
    public float maxDistance = 5f;     // 최대 거리
    public float returnSpeed = 10f;    // 복귀 속도
    public float catchDistance = 0.5f; // 플레이어와 닿으면 소멸

    [SerializeField]private Vector2 startPos;
    [SerializeField] private Transform player;
    [SerializeField] private bool isReturning = false;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private WeaponEffectData effect;

    void Update()
    {
        if (!isReturning)
        {
            // 최대 거리 도달 → 복귀 모드
            if (Vector2.Distance(startPos, transform.position) >= maxDistance)
            {
                isReturning = true;
            }
        }
        else
        {
            // 플레이어 방향으로 직선 복귀
            Vector2 dirToPlayer = (player.position - transform.position).normalized;
            rb.linearVelocity = dirToPlayer * returnSpeed;

            // 플레이어에게 닿으면 소멸
            if (Vector2.Distance(player.position, transform.position) <= catchDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(Vector2 dir, Transform owner, WeaponEffectData weaponEffect)
    {
        player = owner;
        startPos = transform.position;
        moveDir = dir.normalized;
        effect = weaponEffect;

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = moveDir * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // TODO: 데미지 처리
            if (other.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
            {
                enemy.TakeDamage(player.GetComponent<PlayerCondition>().totalMasteryStat, effect.attackEffect);
            }
            else
            {
                Debug.Log("enemy 아님");
            }
        }
    }
}

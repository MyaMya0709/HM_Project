using UnityEngine;

public class Projectile_200 : MonoBehaviour
{
    public float damage;         // 공격 데미지
    public float speed ;         // 이동 속도
    public float maxDistance;    // 최대 거리
    public float returnSpeed;    // 복귀 속도
    public float catchDistance = 0.5f; // 플레이어와 닿으면 소멸

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Transform initPoint;
    [SerializeField] private bool isReturning = false;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private WeaponEffectData effect;

    public bool facingRight;

    private void Start()
    {
        facingRight = moveDir.x > 0;
        if (facingRight) Flip();
    }

    void Update()
    {
        if (!isReturning)
        {
            // 최대 거리 도달 → 복귀 모드
            if (Vector2.Distance(startPos, transform.position) >= maxDistance)
            {
                isReturning = true;
                if (isReturning) Flip();
            }
        }
        else
        {
            // 플레이어 방향으로 직선 복귀
            Vector2 dirToPlayer = (initPoint.position - transform.position).normalized;
            rb.linearVelocity = dirToPlayer * returnSpeed;

            // 플레이어에게 닿으면 소멸
            if (Vector2.Distance(initPoint.position, transform.position) <= catchDistance)
            {
                //소멸 직전에 투사체 갯수 감산
                initPoint.GetComponentInParent<AutoWeapon_200>().projectileUnit--;
                Destroy(gameObject);
            }
        }
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }

    public void Init(Vector2 dir, Transform owner, WeaponEffectData weaponEffect, float damage, float moveSpeed, float maxDis, float returnMoveSpeed)
    {
        initPoint = owner;
        startPos = transform.position;
        moveDir = dir.normalized;
        effect = weaponEffect;

        this.damage = damage;
        speed = moveSpeed;
        maxDistance = maxDis;
        returnSpeed = returnMoveSpeed;

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
                enemy.TakeDamage(damage, effect.attackEffect, moveDir);
            }
            else
            {
                Debug.Log("enemy 아님");
            }
        }
    }
}

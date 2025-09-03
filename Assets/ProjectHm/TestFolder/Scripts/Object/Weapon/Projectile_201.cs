using UnityEngine;

public class Projectile_201 : MonoBehaviour
{
    public float damage;         // 공격 데미지
    public float speed;         // 이동 속도
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
       
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }

    public void Init(Vector2 dir, Transform owner, WeaponEffectData weaponEffect, float damage, float moveSpeed, float maxDis)
    {
        initPoint = owner;
        startPos = transform.position;
        moveDir = dir.normalized;
        effect = weaponEffect;

        this.damage = damage;
        speed = moveSpeed;
        maxDistance = maxDis;

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
                enemy.TakeDamage(damage, effect.attackEffect);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("enemy 아님");
            }
        }
        else if (other.CompareTag(""))
        {

        }
    }
}

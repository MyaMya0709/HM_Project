using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile_201 : MonoBehaviour
{
    public float damage;         // 공격 데미지
    public float speed;         // 이동 속도
    public float rotateSpeed = 200f;
    public float maxDistance;    // 최대 거리
    public float returnSpeed;    // 복귀 속도
    public float catchDistance = 0.5f; // 플레이어와 닿으면 소멸

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Transform initPoint;
    [SerializeField] private bool isReturning = false;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private WeaponEffectData effect;
    private Transform target;

    public bool facingRight;

    private void Start()
    {
        facingRight = moveDir.x > 0;
        if (facingRight) Flip();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 목표 방향
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // 회전량 계산
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target)
        {
            // 명중 처리 (데미지 등)
            Destroy(gameObject);
        }
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
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
}

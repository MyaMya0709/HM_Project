using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UnitBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP;
    public int attackPower;
    public int attackSpeed;
    public int moveSpeed;

    protected float curHP;
    protected Rigidbody2D rb;
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHP = maxHP;
    }
    public virtual void Move(Vector2 direction)
    {
        Debug.Log("Move");
        //MovePosition은 FixedUpdate에서만 사용
        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        //아래는 애니메이션 추가 시 활성
        //if (anim)
        //{
        //    anim.SetFloat("MoveX", direction.x);
        //    anim.SetFloat("MoveY", direction.y);
        //    anim.SetBool("IsMoving", direction != Vector2.zero);
        //}
    }
    public virtual void Attack(UnitBase target)
    {
        //플레이어와 몬스터에서 재정의
        target?.TakeDamage(attackPower);
    }
    public virtual void TakeDamage(float damage)
    {
        curHP -= damage;
        anim?.SetTrigger("Hit");
        //피격 애니메이션 추가 예정

        if (curHP <= 0)
        {
            Dead();
        }
    }
    public virtual void Dead()
    {
        anim?.SetTrigger("Dead");
        //사망 애니메이션 추가 예정
        Destroy(gameObject, 1.5f);
    }
}


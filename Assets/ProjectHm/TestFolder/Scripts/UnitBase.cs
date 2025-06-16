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
        //MovePosition�� FixedUpdate������ ���
        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        //�Ʒ��� �ִϸ��̼� �߰� �� Ȱ��
        //if (anim)
        //{
        //    anim.SetFloat("MoveX", direction.x);
        //    anim.SetFloat("MoveY", direction.y);
        //    anim.SetBool("IsMoving", direction != Vector2.zero);
        //}
    }
    public virtual void Attack(UnitBase target)
    {
        //�÷��̾�� ���Ϳ��� ������
        target?.TakeDamage(attackPower);
    }
    public virtual void TakeDamage(float damage)
    {
        curHP -= damage;
        anim?.SetTrigger("Hit");
        //�ǰ� �ִϸ��̼� �߰� ����

        if (curHP <= 0)
        {
            Dead();
        }
    }
    public virtual void Dead()
    {
        anim?.SetTrigger("Dead");
        //��� �ִϸ��̼� �߰� ����
        Destroy(gameObject, 1.5f);
    }
}


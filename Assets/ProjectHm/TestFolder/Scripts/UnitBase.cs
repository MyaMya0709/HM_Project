using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UnitBase : MonoBehaviour
{
    [Header("Stats")]
    public UnitStats stats;

    protected float currentHealth;
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = stats.maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }

    }
    protected virtual void Dead()
    {
        //��� ó��
        animator?.SetTrigger("Dead");
        //��� �ִϸ��̼� ��� �� ����
        Destroy(gameObject, 1.5f);
    }

    public bool IsDead => currentHealth <= 0;
}


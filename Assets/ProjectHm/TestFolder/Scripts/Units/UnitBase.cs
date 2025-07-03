using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UnitBase : MonoBehaviour
{
    [Header("Stats")]
    public UnitStats stats;

    public float currentHealth;
    public Animator animator;

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
    protected virtual void Dead([CallerMemberName] string callername = null)
    {
        //��� ó��
        //animator?.SetTrigger("Dead");
        //��� �ִϸ��̼� ��� �� ����
        OnDeath?.Invoke();
        Destroy(gameObject, 1.5f);
    }

    public bool IsDead => currentHealth <= 0;
    public event System.Action OnDeath;
}


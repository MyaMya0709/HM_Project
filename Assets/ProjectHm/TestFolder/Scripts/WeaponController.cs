using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRange = 1.2f;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    [Header("Effects")]
    public GameObject hitEffect;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Attack");
            // ���� ���� ���� �� ����
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            // �Ǵ� ���� ����
            PlayerController playerController = GetComponent<PlayerController>();
            Vector2 forward = playerController.lastLookDirection;

            foreach (var enemyCollider in hitEnemies)
            {
                OnTriggerEnter2D(enemyCollider);
            }

            // ����׿� �α�
            Debug.Log($"Attack hit {hitEnemies.Length} enemies.");
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
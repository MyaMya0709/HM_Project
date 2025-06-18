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
    public bool mutipleAttack = false;

    [Header("Effects")]
    public GameObject hitEffect;

    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnAttack");
            if (!mutipleAttack)
            {
                SingleAttack();
            }
            else
            {
                MutipleAttack();
            }
        }
    }

    public void SingleAttack()
    {
        Debug.Log("SingleAttack");
        //���� �������� ���� �������� ����� �� ����
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerController.lastLookDirection,attackRange, enemyLayer);

        if(hit.collider != null)
        {
            ToEnemyDamage(hit.collider);
            Debug.Log($"Attack enemies.");
        }
    }

    public void MutipleAttack()
    {
        Debug.Log("MutipleAttack");

        // ���� ���� ���� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // ���� ����
        Vector2 forward = playerController.lastLookDirection;

        foreach (var enemyCollider in hitEnemies)
        {
            Vector2 toTarget = (enemyCollider.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(forward, toTarget);

            if (dot > 0) //0���� ũ�� ����
            {
                ToEnemyDamage(enemyCollider);
                // ����׿� �α�
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

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
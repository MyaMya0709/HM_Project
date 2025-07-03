using UnityEngine;

public class BaseCore : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 200f;
    public float currentHealth;

    [Header("Effects")]
    public GameObject hitEffect;

    public event System.Action OnBaseDestroy;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //Debug.Log($"���� �ǰ�! ���� ü��: {currentHealth}");

        // Ÿ�� ȿ���� �ִٸ� ��üȭ
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        //���� �ı�
        if (currentHealth <= 0)
            OnBaseDestroyed();
    }

    private void OnBaseDestroyed()
    {
        Debug.Log("���� �ı���! ���� ���� ó��");
        // TODO: GameManager�� ���ӿ��� �˸�
        OnBaseDestroy?.Invoke();
        Destroy(gameObject); // or ��Ȱ��ȭ
    }
}

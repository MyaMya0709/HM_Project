using UnityEngine;
using UnityEngine.UI;

public class BaseCore : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 200f;
    public float currentHealth;

    [SerializeField] private Image HPBar;
    public Transform AttackPoint;

    [Header("Effects")]
    public GameObject hitEffect;

    public event System.Action OnBaseDestroy;

    private void Awake()
    {
        // ���� �ı� �� GameOver() ȣ��
        OnBaseDestroy += GameManager.Instance.GameOver;

        currentHealth = maxHealth;
        HPBar.fillAmount = 1;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //Debug.Log($"���� �ǰ�! ���� ü��: {currentHealth}");

        HPBar.fillAmount = currentHealth/maxHealth;

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
        // GameManager�� ���ӿ��� �˸�
        OnBaseDestroy?.Invoke();
        Destroy(gameObject); // or ��Ȱ��ȭ
    }
}

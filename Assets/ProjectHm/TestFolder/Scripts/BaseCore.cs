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
        //Debug.Log($"기지 피격! 현재 체력: {currentHealth}");

        // 타격 효과가 있다면 실체화
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        //기지 파괴
        if (currentHealth <= 0)
            OnBaseDestroyed();
    }

    private void OnBaseDestroyed()
    {
        Debug.Log("기지 파괴됨! 게임 오버 처리");
        // TODO: GameManager에 게임오버 알림
        OnBaseDestroy?.Invoke();
        Destroy(gameObject); // or 비활성화
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

// Damage Popup Prefab에 붙어있음
public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifeTime = 0.8f;
    private TextMeshProUGUI text;
    private Vector3 startPos;

    void Awake()
    {
        // 생성되면 자신의 위치와 기입할 텍스트 경로 설정
        text = GetComponentInChildren<TextMeshProUGUI>();
        startPos = transform.localPosition;
    }

    // 정보 세팅, popup 코루틴 실행
    public void Setup(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(Popup());
    }

    // 일정시간이 지난후 파괴 세팅 및 천천히 위로 이동
    private IEnumerator Popup()
    {
        float elapsed = 0f;
        while (elapsed < lifeTime)
        {
            transform.localPosition = startPos + Vector3.up * (moveSpeed * elapsed);
            text.alpha = Mathf.Lerp(1, 0, elapsed / lifeTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}

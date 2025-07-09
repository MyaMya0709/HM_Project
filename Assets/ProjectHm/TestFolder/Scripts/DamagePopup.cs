using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifeTime = 0.8f;
    private TextMeshProUGUI text;
    private Vector3 startPos;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        startPos = transform.localPosition;
    }

    public void Setup(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(Popup());
    }

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

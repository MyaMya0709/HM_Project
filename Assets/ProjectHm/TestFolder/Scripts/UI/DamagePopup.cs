using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator anim;

    public void Setup(int damage)
    {
        text.text = damage.ToString();
        anim.Play("PopupAnimation");
    }

    // 애니메이션 이벤트에서 호출
    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}

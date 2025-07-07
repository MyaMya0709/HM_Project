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

    // �ִϸ��̼� �̺�Ʈ���� ȣ��
    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}

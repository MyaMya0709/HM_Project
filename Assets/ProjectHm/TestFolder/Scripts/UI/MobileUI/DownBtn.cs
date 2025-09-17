using UnityEngine;
using UnityEngine.EventSystems;

public class DownBtn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("버튼 눌림!");
        player.DoDownAttack();
        // 여기서 눌린 상태 로직 실행 가능
    }
}

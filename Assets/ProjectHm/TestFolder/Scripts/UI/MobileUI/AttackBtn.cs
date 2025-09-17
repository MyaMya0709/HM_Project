using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("버튼 눌림!");
        player.DoAttack(false);
        // 여기서 눌린 상태 로직 실행 가능
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("버튼 뗌!");
        player.DoAttack(true);
        // 여기서 누르기 끝난 후 로직 실행
    }
}

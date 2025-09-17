using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Mobile : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private EventTrigger rightBtn;
    [SerializeField] private EventTrigger leftBtn;
    [SerializeField] private EventTrigger upBtn;
    [SerializeField] private EventTrigger downBtn;
    [SerializeField] private EventTrigger attackBtn;
    [SerializeField] private EventTrigger skillBtn;

    private void Start()
    {
        AddEvent(rightBtn, EventTriggerType.PointerDown, (data) => player.RightMove());
        AddEvent(rightBtn, EventTriggerType.PointerUp, (data) => player.DontMove());

        AddEvent(leftBtn, EventTriggerType.PointerDown, (data) => player.LeftMove());
        AddEvent(leftBtn, EventTriggerType.PointerUp, (data) => player.DontMove());

        AddEvent(upBtn, EventTriggerType.PointerDown, (data) => player.DoJump());

        AddEvent(downBtn, EventTriggerType.PointerDown, (data) => player.DoDownAttack());

        AddEvent(attackBtn, EventTriggerType.PointerDown, (data) => player.DoAttack(false));
        AddEvent(attackBtn, EventTriggerType.PointerUp, (data) => player.DoAttack(true));
    }

    // 공통적인 이벤트 등록 함수
    private void AddEvent(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        if (trigger == null) return;

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
}

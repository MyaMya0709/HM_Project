using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    public SlotData slotData;

    [SerializeField] private Button selecButton;
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private PlayerCondition player;

    public SlotType slotType;
    public TMP_Text slotName;
    public Image slotIcon;
    public TMP_Text slotDescription;


    protected void Awake()
    {
        selecButton.onClick.AddListener(() =>
        {
            //플레이어에게 정보 전달
            if (selectionUI.activeSelf)
            {
                OnClickSlot();
                Time.timeScale = 1f;
                selectionUI.SetActive(false);
            }
        });
    }

    public void SlotInit(GameObject GO, Transform initPos, SlotData data, UI_Selection selecUI)
    {
        selectionUI = selecUI.gameObject;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCondition>();

        slotData = data;
        slotType = data.type;
        slotName.text = data.name;
        //slotIcon.sprite = data.icon;     // 아이콘 추가시 활성화
        slotDescription.text = data.description;

        Instantiate(GO, initPos);
    }

    public void OnClickSlot()
    {
        switch (slotType)
        {
            case SlotType.StatUP:
                player.SelecStatLevelUP(slotData.statType);
                break;

                // TODO: 무기 추가 + 레벨업 통합하기
            case SlotType.WeaponGet:
                //자동무기 추가
                Debug.Log("자동무기 추가");
                break;

            case SlotType.WeaponUP:
                //자동무기 업글
                Debug.Log("자동무기 업글");
                break;
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    public SlotData slotData;

    [SerializeField] private Button selecButton;
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerCondition playerCondition;

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

    public void SlotInit(GameObject GO, Transform initPos, SlotData data, UI_Selection selecUI, GameObject player)
    {
        selectionUI = selecUI.gameObject;
        playerController = player.GetComponent<PlayerController>();
        playerCondition = player.GetComponent<PlayerCondition>();

        slotData = data;
        slotType = data.type;
        slotName.text = data.tatle;
        //slotIcon.sprite = data.icon;     // 아이콘 추가시 활성화
        slotDescription.text = data.description;

        Instantiate(GO, initPos);
    }

    public void OnClickSlot()
    {
        switch (slotType)
        {
            case SlotType.StatUP:
                Debug.Log("스탯 업");
                playerCondition.SelecStatLevelUP(slotData.statLvType);
                break;

            // TODO: 무기 추가 + 레벨업 통합하기
            case SlotType.ManualWeapon:
                Debug.Log("수동무기 렙업");
                playerController.curWeapon.SelecLevelUp();
                break;

            case SlotType.AutoWeapon:
                Debug.Log("자동무기 추가 or 렙업");
                playerCondition.AutoWeaponSet(slotData.weaponID);
                break;
        }
    }
}

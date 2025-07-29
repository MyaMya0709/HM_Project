using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class UI_Selection : UI
{
    [SerializeField] private RectTransform slots;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int maxSlotCount = 3;
    public List<SlotData> slotDatas = new();
    public GameObject player;

    protected override void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        Debug.Log("SelecUI 실행");
        SetSlot();
    }

    public void SetSlot()
    {
        // 슬롯의 유무 체크 후 제거
        int slotIndex = slots.childCount;
        if (slotIndex != 0)
        {
            Debug.Log("SelecSlot 있음");
            for (int i = 0; i < slotIndex; i++)
            {
                RectTransform child = (RectTransform)slots.GetChild(i);
                Debug.Log($"Child {i}: {child.name}");
                Destroy(child.gameObject);
            }
            Debug.Log("SelecSlot 제거");
        }

        // 생성할 슬롯의 데이터 리스트 체크
        SlotListSet();

        // 슬롯 생성 및 정보 세팅
        for (int i = 0; i < maxSlotCount; i++)
        {
            slotPrefab.GetComponent<SelectionSlot>().SlotInit(slotPrefab, slots, SlotDataSet(),this, player);
        }
        Debug.Log("SelecSlot 리셋");
    }

    public void SlotListSet()
    {
        List<SlotData> list = new();

        foreach (SlotData slotData in DataManager.Instance.allSelecSlotDatas)
        {
            if ((slotData.type == SlotType.ManualWeapon) && (slotData.weaponID != player.GetComponent<PlayerController>().currentWeapon.data.weaponID))
                continue;

            list.Add(slotData);
        }

        slotDatas = list;
    }

    public SlotData SlotDataSet()
    {
        int selscData = Random.Range(0, DataManager.Instance.allSelecSlotDatas.Count);
        SlotData data = DataManager.Instance.allSelecSlotDatas[selscData];

        return data;
    }
}

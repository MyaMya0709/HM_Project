using UnityEngine;

public class UI_Selection : UI
{
    [SerializeField] private RectTransform slots;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int maxSlotCount = 3;

    private void Start()
    {
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

        // 슬롯 생성 및 정보 세팅
        for (int i = 0; i < maxSlotCount; i++)
        {
            GameObject GO = Instantiate(slotPrefab, slots);
            // GO의 SelectionSlot컴퍼넌트에 접근해서 정보 셋팅하기
        }
        Debug.Log("SelecSlot 리셋");
    }
}

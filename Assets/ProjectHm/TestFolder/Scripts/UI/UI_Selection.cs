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
        // ������ ���� üũ �� ����
        int slotIndex = slots.childCount;
        if (slotIndex != 0)
        {
            Debug.Log("SelecSlot ����");
            for (int i = 0; i < slotIndex; i++)
            {
                RectTransform child = (RectTransform)slots.GetChild(i);
                Debug.Log($"Child {i}: {child.name}");
                Destroy(child.gameObject);
            }
            Debug.Log("SelecSlot ����");
        }

        // ���� ���� �� ���� ����
        for (int i = 0; i < maxSlotCount; i++)
        {
            GameObject GO = Instantiate(slotPrefab, slots);
            // GO�� SelectionSlot���۳�Ʈ�� �����ؼ� ���� �����ϱ�
        }
        Debug.Log("SelecSlot ����");
    }
}

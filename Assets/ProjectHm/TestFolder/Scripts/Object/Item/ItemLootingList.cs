using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLootingList : MonoBehaviour
{
    public GameObject droppedItemPrepab;
    public List<ItemData> ItemList = new List<ItemData>();

    private List<ItemData> GetDroppedItem()
    {
        int randomNumber = Random.Range(1,101);             // 1-100, �������� ���� Ȯ��
        List<ItemData> possibleItem = new List<ItemData>(); // Ȯ���� ���� ���� �� �ִ� ������ ����Ʈ

        // ������ �������� ����Ʈ�� �߰�
        foreach (ItemData item in ItemList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
            }
        }

        // �������� ������ ��� ����Ʈ 
        if (possibleItem.Count > 0)
        {
            //ItemData droppedItem = possibleItem[Random.Range(0,possibleItem.Count)];
            //return droppedItem;
            return possibleItem;
        }

        Debug.Log("No Item Dropped");
        return null;
    }

    public void InstantiateItem(Vector2 spawnPosition)
    {
        List<ItemData> droppedItems = GetDroppedItem();

        if (droppedItems != null)
        {
            // ���� �������� ��üȭ ����
            foreach (ItemData item in droppedItems)
            {
                // ��üȭ �� �������� ���� ������ ����
                GameObject ItemGameObject = Instantiate(droppedItemPrepab, spawnPosition, Quaternion.identity);
                ItemGameObject.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
                ItemGameObject.GetComponent<LootableItem>().ItemType = item.itemType;
                ItemGameObject.GetComponent<LootableItem>().amount = item.itemAmount;

                // ƨ�ܼ� ����Ǵ� ���
                float dropForce = 20f;
                Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
                ItemGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}

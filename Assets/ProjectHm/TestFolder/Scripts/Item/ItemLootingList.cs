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
        int randomNumber = Random.Range(1,101);             // 1-100, 아이템이 나올 확률
        List<ItemData> possibleItem = new List<ItemData>(); // 확률에 따라 나올 수 있는 아이템 리스트

        // 나오는 아이템을 리스트에 추가
        foreach (ItemData item in ItemList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
            }
        }

        // 아이템이 나오는 경우 리스트 
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
            // 각각 아이템의 실체화 과정
            foreach (ItemData item in droppedItems)
            {
                // 실체화 및 아이템의 변수 데이터 이전
                GameObject ItemGameObject = Instantiate(droppedItemPrepab, spawnPosition, Quaternion.identity);
                ItemGameObject.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
                ItemGameObject.GetComponent<LootableItem>().ItemType = item.itemType;
                ItemGameObject.GetComponent<LootableItem>().amount = item.itemAmount;

                // 튕겨서 드랍되는 모션
                float dropForce = 20f;
                Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
                ItemGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}

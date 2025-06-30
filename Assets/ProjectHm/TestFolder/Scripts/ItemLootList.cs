using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemLootList : MonoBehaviour
{
    public GameObject droppedItemPrepab;
    public List<ItemData> ItemList = new List<ItemData>();

    private ItemData GetDroppedItem()
    {
        int randomNumber = Random.Range(1,101); // 1-100
        List<ItemData> possibleItem = new List<ItemData>();
        foreach (ItemData item in ItemList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
            }
        }
        if (possibleItem.Count > 0)
        {
            ItemData droppedItem = possibleItem[Random.Range(0,possibleItem.Count)];
            return droppedItem;
        }
        Debug.Log("No Item Dropped");
        return null;
    }

    public void InstantiateItem(Vector2 spawnPosition)
    {
        ItemData droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject ItemGameObject = Instantiate(droppedItemPrepab, spawnPosition, Quaternion.identity);
            ItemGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.itemSprite;


            float dropForce = 30f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f),Random.Range(0f, 1f));
            ItemGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    }
}

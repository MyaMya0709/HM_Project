using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public float itemAmount;
    public int dropChance;
    public ItemType itemType;

    public ItemData(string name, string description, float amount, int chance, ItemType type)
    {
        itemName = name;
        itemDescription = description;
        itemAmount = amount;
        dropChance = chance;
        itemType = type;
    }
}

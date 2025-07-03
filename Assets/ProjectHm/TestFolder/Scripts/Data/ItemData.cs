using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
<<<<<<< HEAD
    public int dropChance;

    public ItemData(string name, string description, int chance)
    {
        itemName = name;
        itemDescription = description;
        dropChance = chance;
=======
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
>>>>>>> 4c7adf8 ([Wip] 오류 복구 임시 저장)
    }
}

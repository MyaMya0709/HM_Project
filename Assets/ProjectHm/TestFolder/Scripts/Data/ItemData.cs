using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int dropChance;

    public ItemData(string name, string description, int chance)
    {
        itemName = name;
        itemDescription = description;
        dropChance = chance;
    }
}

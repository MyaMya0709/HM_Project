using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public float itemAmount;            // 돈, 경험치에 사용
    public int dropChance;              // 아이템의 드롭 확률
    public ItemType itemType;           // 아이템의 종류
    public int buffID;                  // DataManager에서 가져올 버프 아이디
}

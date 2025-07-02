using UnityEngine;

public interface ILootable
{
    void OnLooted(PlayerController Player);
}

public class LootableItem : MonoBehaviour, ILootable
{
    public ItemType ItemType;
    public float amount;
    //public BuffData buffData;
    public void OnLooted(PlayerController player)
    {
        switch (ItemType)
        {
            case ItemType.Gold:
                player.curGold += (int)amount;
                break;
            case ItemType.Exp:
                player.curExp += amount;
                break;
            case ItemType.Heart:
                //player.currentHealth += amount;
                break;
            case ItemType.Buff:
                //player.ApplyBuff(buffData);
                break;
            case ItemType.Skill:
                //player.currentHealth += amount;
                break;
        } 
    }
}
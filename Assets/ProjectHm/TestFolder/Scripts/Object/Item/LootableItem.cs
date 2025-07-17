using UnityEngine;

public class LootableItem : MonoBehaviour
{
    public ItemType ItemType;
    public float amount;
    //public BuffData buffData;

    public SpriteRenderer sr;
    public Rigidbody2D rb;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(ItemData data)
    {
        //데이터 이전
        sr.sprite = data.itemSprite;
        ItemType = data.itemType;
        amount = data.itemAmount;

        // 튕겨서 드랍되는 모션
        float dropForce = 20f;
        Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
        rb.AddForce(dropDir * dropForce, ForceMode2D.Impulse);
    }

    public void OnLooted(Player player)
    {
        switch (ItemType)
        {
            case ItemType.Gold:
                player.curGold += (int)amount;
                break;
            case ItemType.Exp:
                player.curExp += amount;
                player.UpdateExp();
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
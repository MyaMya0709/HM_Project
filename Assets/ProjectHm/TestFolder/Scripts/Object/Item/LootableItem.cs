using UnityEngine;

public class LootableItem : MonoBehaviour
{
    public ItemType ItemType;
    public float amount;
    public int buffID;

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
        buffID = data.buffID;

        // 튕겨서 드랍되는 모션
        float dropForce = 20f;
        Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
        rb.AddForce(dropDir * dropForce, ForceMode2D.Impulse);
    }

    public void OnLooted(PlayerCondition player)
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
                // [수정 필요] DataManager의 버프 프리펩을 인스턴스하여 버프 실행
                IBuff buffObj = Instantiate(DataManager.Instance.buffPrefabList[buffID]).GetComponent<IBuff>();
                buffObj.ApplyBuff();
                break;
        }
    }
}
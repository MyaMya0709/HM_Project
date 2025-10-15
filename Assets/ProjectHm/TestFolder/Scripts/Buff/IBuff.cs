using UnityEngine;

public abstract class IBuff : MonoBehaviour
{
    // 스탯 버프 정보 기입
    public int ID;
    public string Name;
    public string Description;
    public Sprite sprite;
    public float duration;
    public UI_State state;
    public PlayerCondition player;

    private void Awake()
    {
        state = FindFirstObjectByType<UI_State>();
        player = FindFirstObjectByType<PlayerCondition>();
    }

    public abstract void ApplyBuff();
}

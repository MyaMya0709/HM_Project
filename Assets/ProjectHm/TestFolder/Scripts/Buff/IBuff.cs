using UnityEngine;

public abstract class IBuff : MonoBehaviour
{
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

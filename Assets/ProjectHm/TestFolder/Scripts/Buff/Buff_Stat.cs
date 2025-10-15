using UnityEngine;
using UnityEngine.WSA;

public class Buff_Stat : IBuff
{

    // 버프 효과 / Vlaue : 0~1 => 디버프, 1~ => 버프
    public StatType statType;
    public float buffValue;

    public override void ApplyBuff()
    {
        StartCoroutine(player.OnBuff(this));
        state.buffIcon = sprite;
        state.InitStateUI(4, duration);
    }

}

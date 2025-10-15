using UnityEngine;
using UnityEngine.WSA;

public class Buff_Stat : IBuff
{
    // [수정 필요] 1버프에 2이상의 스탯 상승 하락이 있을 수 있음
    // 버프 효과 / Vlaue : 0~1 => 디버프, 1~ => 버프
    public StatType statType;
    public float buffValue;

    public override void ApplyBuff()
    {
        //버프 실행
        StartCoroutine(player.OnBuff(this));
        
        //버프 아이콘 전달 및 아이콘 UI생성
        state.buffIcon = sprite;
        state.InitStateUI(4, duration);
    }

}

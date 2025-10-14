using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Buff")]
public class BuffData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public Sprite sprite;

    // 버프 효과 / Vlaue : 0~1 => 디버프, 1~ => 버프
    public StatType statType;
    public float buffValue;
    public float duration;
}
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Slot")]

public class SlotData : ScriptableObject
{
    public SlotType type;
    public string tatle;
    public Sprite icon;
    [TextArea] public string description;
    public StatType statType;               // 스탯 업그레이드
    public int weaponID;               // 자동무기 장착 및 업그레이드 or 수동무기 휘발성 업그레이드

}
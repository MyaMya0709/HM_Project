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
    //public IWeapon weapon;              // 자동무기 장착
    //public WeaponUpData weaponUpData;   // 자동무기 업그레이드
}


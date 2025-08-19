using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MWP")]
public class ManualWeaponData : ScriptableObject
{
    public int weaponID;
    public int price;

    public List<float> baseMoveList;
    public List<float> baseDamageList;
    public List<float> baseRangeList;

    public List<float> selecMoveList;
    public List<float> selecDamageList;
    public List<float> selecRangeList;

    // 레벨로 원하는 값 찾기
    public List<int> enhanceCostList;
    public List<WeaponEffectData> baseWeaponEffectList;
    public List<WeaponEffectData> selecWeaponEffectList;
}

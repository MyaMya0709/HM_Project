using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MWP")]
public class ManualWeaponData : ScriptableObject
{
    public int weaponID;

    public List<float> baseMoveList;
    public List<float> baseDamageList;
    public List<float> baseRangeList;

    public List<float> selecMoveList;
    public List<float> selecDamageList;
    public List<float> selecRangeList;

    public WeaponEffectData attackEffect;
    public WeaponEffectData downEffect;
    public WeaponEffectData chargeEffect;
    public WeaponEffectData dashEffect;
}

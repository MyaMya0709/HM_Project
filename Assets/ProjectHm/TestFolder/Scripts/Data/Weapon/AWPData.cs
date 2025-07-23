using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AWP")]
public class AutoWeaponData : ScriptableObject
{
    public int weaponID;
    
    //효과 데이터 처림한것처럼 ABCD로 구성, 6스탯
    public List<float> baseDamageList;
    public List<float> baseRangeList;
    public List<float> selecDamageList;
    public List<float> selecRangeList;
}

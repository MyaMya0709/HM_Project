using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AWP")]
public class AutoWeaponData : ScriptableObject
{
    public int weaponID;
    public int price;

    //효과 데이터 처리한것처럼 ABCD로 구성, 6스탯
    public List<float> damageList;
    public List<float> rangeList;
    public List<float> speedList;
    public List<float> statList0;
    public List<float> statList1;
    public List<float> statList2;

}

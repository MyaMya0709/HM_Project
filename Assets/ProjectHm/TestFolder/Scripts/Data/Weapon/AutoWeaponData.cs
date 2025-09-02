using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AWP")]
public class AutoWeaponData : ScriptableObject
{
    public int weaponID;
    public int price;

    //효과 데이터 처리한것처럼 ABCD로 구성
    public List<float> statFloatList0;
    public List<float> statFloatList1;
    public List<float> statFloatList2;
    public List<float> statFloatList3;
    public List<float> statFloatList4;
    public List<int> statIntList0;
    public List<int> statIntList1;
    public List<int> statIntList2;
    public List<int> statIntList3;
    public List<int> statIntList4;

    //효과 데이터 처리한것처럼 ABCD로 구성, 6스탯
    public List<float> masteryStatFloatList0;
    public List<float> masteryStatFloatList1;
    public List<float> masteryStatFloatList2;
    public List<float> masteryStatFloatList3;
    public List<float> masteryStatFloatList4;
    public List<int> masteryStatIntList0;
    public List<int> masteryStatIntList1;
    public List<int> masteryStatIntList2;
    public List<int> masteryStatIntList3;
    public List<int> masteryStatIntList4;

    // 레벨로 원하는 값 찾기
    public List<WeaponEffectData> selecWeaponEffectList;
}

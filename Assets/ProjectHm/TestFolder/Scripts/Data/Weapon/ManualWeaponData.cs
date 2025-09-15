using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MWP")]
public class ManualWeaponData : ScriptableObject
{
    public int weaponID;
    public int price;

    public List<float> baseAttackPowerList;
    public List<float> baseAttackSpeedList;
    public List<float> baseRangeList;
    public List<float> baseMoveSpeedList;
    public List<float> basejumpPowerList;
    public List<float> baseDashPowerList;
    public List<float> baseSuperJumpPowerList;

    public List<float> selecAttackPowerList;
    public List<float> selecAttackSpeedList;
    public List<float> selecRangeList;
    public List<float> selecMoveSpeedList;
    public List<float> selecjumpPowerList;
    public List<float> selecDashPowerList;
    public List<float> selecSuperJumpPowerList;

    // 레벨로 원하는 값 찾기
    public List<int> enhanceCostList;
    public List<WeaponEffectData> baseWeaponEffectList;
    public List<WeaponEffectData> selecWeaponEffectList;

    // 파티클 일반공격, 내려찍기, 차징이펙트, 차징공격, 대쉬공격 순
    public GameObject attackParticle1;
    public GameObject attackParticle2;
    public GameObject attackParticle3;
    public GameObject attackParticle4;
    public GameObject attackParticle5;
}

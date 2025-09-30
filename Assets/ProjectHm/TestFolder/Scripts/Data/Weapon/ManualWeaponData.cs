using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MWP")]
public class ManualWeaponData : ScriptableObject
{
    public int weaponID;
    public int price;
    public float chargeAttackSpeedMultiple;

    [Header("DelayRatio")]
    // 각 공격의 선딜 후딜 값
    public float before_Attack_DelayRatio;
    public float after_Attack_DelayRatio;
    public float before_DownAttack_DelayRatio;
    public float after_DownAttack_DelayRatio;
    public float before_ChargeAttack_DelayRatio;
    public float after_ChargeAttack_DelayRatio;
    public float before_DashAttack_DelayRatio;
    public float after_DashAttack_DelayRatio;

    [Header("BaseStatList")]
    public List<float> baseAttackPowerList;
    public List<float> baseAttackSpeedList;
    public List<float> baseRangeList;
    public List<float> baseMoveSpeedList;
    public List<float> baseJumpPowerList;
    public List<float> baseDashCooltimeList;
    public List<float> baseSuperJumpCooltimeList;
    public List<float> baseDownAttackCooltimeList;

    [Header("SelecStatList")]
    public List<float> selecAttackPowerList;
    public List<float> selecAttackSpeedList;
    public List<float> selecRangeList;
    public List<float> selecMoveSpeedList;
    public List<float> selecJumpPowerList;
    public List<float> selecDashCooltimeList;
    public List<float> selecSuperJumpCooltimeList;
    public List<float> selecDownAttackCooltimeList;

    [Header("Enhance/Effect List")]
    // 레벨로 원하는 값 찾기
    public List<int> enhanceCostList;
    public List<WeaponEffectData> baseWeaponEffectList;
    public List<WeaponEffectData> selecWeaponEffectList;

    [Header("Particle")]
    // 파티클 일반공격, 내려찍기, 차징이펙트, 차징공격, 대쉬공격 순
    public GameObject attackParticle1;
    public GameObject attackParticle2;
    public GameObject attackParticle3;
    public GameObject attackParticle4;
    public GameObject attackParticle5;
}

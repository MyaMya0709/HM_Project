using System.Collections.Generic;
using UnityEngine;

public abstract class IAutoWeapon : MonoBehaviour
{
    public AutoWeaponData data;

    public int baseWeaponLevel;
    public int selecWeaponLevel;

    [Header("Attack Settings")]
    public float baseDamage;
    public float selecDamage;
    public float totalWeaponDamage;
    public float totalDamage;

    public float baseRange;
    public float selecRange;
    public float totalRange;

    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public SpriteRenderer sr;
    public LayerMask enemyLayer;

    protected virtual void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    public void BaseLevelUp()
    {
        baseWeaponLevel++;
        baseDamage = data.baseDamageList[baseWeaponLevel];
        baseRange = data.baseRangeList[baseWeaponLevel];

        GetTotalStat();
    }

    public void SelecLevelUp()
    {
        Debug.Log("자동무기 선택지 렙업");
        selecWeaponLevel++;
        selecDamage = data.selecDamageList[selecWeaponLevel];
        selecRange = data.selecRangeList[selecWeaponLevel];

        GetTotalStat();
    }

    // 총스탯 계산 함수
    public void GetTotalStat()
    {
        totalWeaponDamage = baseDamage * selecDamage;
        totalDamage = totalWeaponDamage + playerCondition.totalAttackPower;
        totalRange = baseRange * selecRange;
    }

    // 게임 시작용 초기화 함수
    public void SetStat()
    {
        baseDamage = data.baseDamageList[baseWeaponLevel];
        baseRange = data.baseRangeList[baseWeaponLevel];

        selecWeaponLevel = 0;
        selecDamage = data.selecDamageList[selecWeaponLevel];
        selecRange = data.selecRangeList[selecWeaponLevel];

        GetTotalStat();
    }

    // 게임 초기화용 함수
    public void Clear()
    {
        baseWeaponLevel = 0;
        SetStat();
    }
}

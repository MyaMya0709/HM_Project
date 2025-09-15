using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAutoWeapon : MonoBehaviour
{
    public AutoWeaponData data;
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public LayerMask enemyLayer;

    public int selecWeaponLevel;

    [Header("Attack Settings")]
    public float selecFloatStat0;
    public float selecFloatStat1;
    public float selecFloatStat2;
    public float selecFloatStat3;
    public float selecFloatStat4;
    public int selecIntStat0;
    public int selecIntStat1;
    public int selecIntStat2;
    public int selecIntStat3;
    public int selecIntStat4;

    public static event Action OnWeaponLevelUp;

    protected virtual void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();

        SetStat();
    }

    public abstract void Attack();

    public void SelecLevelUp()
    {
        Debug.Log("자동무기 선택지 렙업");
        selecWeaponLevel++;
        selecFloatStat0 = data.statFloatList0[selecWeaponLevel];
        selecFloatStat1 = data.statFloatList1[selecWeaponLevel];
        selecFloatStat2 = data.statFloatList2[selecWeaponLevel];
        selecFloatStat3 = data.statFloatList3[selecWeaponLevel];
        selecFloatStat4 = data.statFloatList4[selecWeaponLevel];
        selecIntStat0 = data.statIntList0[selecWeaponLevel];
        selecIntStat1 = data.statIntList1[selecWeaponLevel];
        selecIntStat2 = data.statIntList2[selecWeaponLevel];
        selecIntStat3 = data.statIntList3[selecWeaponLevel];
        selecIntStat4 = data.statIntList4[selecWeaponLevel];

        OnWeaponLevelUp?.Invoke();
    }

    // 게임 시작용 초기화 함수
    public void SetStat()
    {
        selecWeaponLevel = 0;
        selecFloatStat0 = data.statFloatList0[selecWeaponLevel];
        selecFloatStat1 = data.statFloatList1[selecWeaponLevel];
        selecFloatStat2 = data.statFloatList2[selecWeaponLevel];
        selecFloatStat3 = data.statFloatList3[selecWeaponLevel];
        selecFloatStat4 = data.statFloatList4[selecWeaponLevel];
        selecIntStat0 = data.statIntList0[selecWeaponLevel];
        selecIntStat1 = data.statIntList1[selecWeaponLevel];
        selecIntStat2 = data.statIntList2[selecWeaponLevel];
        selecIntStat3 = data.statIntList3[selecWeaponLevel];
        selecIntStat4 = data.statIntList4[selecWeaponLevel];
    }

    // 게임 초기화용 함수
    public void Clear()
    {
        SetStat();
    }
}

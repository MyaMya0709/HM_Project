using System.Collections.Generic;
using UnityEngine;

public abstract class IAutoWeapon : MonoBehaviour
{
    public AutoWeaponData data;
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public SpriteRenderer sr;
    public LayerMask enemyLayer;
    public Animator animator;

    public int selecWeaponLevel;

    [Header("Attack Settings")]
    public float selecDamage;
    public float selecRange;
    public float selecSpeed;
    public float selecStat0;
    public float selecStat1;
    public float selecStat2;

    protected virtual void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public abstract void Attack();


    public void SelecLevelUp()
    {
        Debug.Log("자동무기 선택지 렙업");
        selecWeaponLevel++;
        selecDamage = data.damageList[selecWeaponLevel];
        selecRange = data.rangeList[selecWeaponLevel];
    }

    // 게임 시작용 초기화 함수
    public void SetStat()
    {
        selecWeaponLevel = 0;
        selecDamage = data.damageList[selecWeaponLevel];
        selecRange = data.rangeList[selecWeaponLevel];
    }

    // 게임 초기화용 함수
    public void Clear()
    {
        SetStat();
    }
}

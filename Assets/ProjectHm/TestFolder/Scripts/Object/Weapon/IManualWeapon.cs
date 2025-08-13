using UnityEngine;

public abstract class IManualWeapon : MonoBehaviour
{
    public ManualWeaponData data;

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

    private void OnEnable() => PlayerCondition.OnPlayerStatUp += GetTotalStat;
    private void OnDisable() => PlayerCondition.OnPlayerStatUp -= GetTotalStat;

    protected virtual void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponentInChildren<SpriteRenderer>();
        SetStat();
    }

    public abstract void Attack();
    public abstract void DownAttack();
    public abstract void ChargingAttack();
    public abstract void DashAttack();
    
    public void BaseLevelUp()
    {
        baseWeaponLevel++;
        baseDamage = data.baseDamageList[baseWeaponLevel];
        baseRange = data.baseRangeList[baseWeaponLevel];

        if(playerCondition != null) GetTotalStat();
        else
        {
            totalWeaponDamage = baseDamage * selecDamage;
            totalDamage = totalWeaponDamage;
            totalRange = baseRange * selecRange;
        }
    }

    public void SelecLevelUp()
    {
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

    // 게임 시작용 스텟 세팅 함수
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

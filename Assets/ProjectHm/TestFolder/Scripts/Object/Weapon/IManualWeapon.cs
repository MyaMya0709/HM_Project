using UnityEngine;

public abstract class IManualWeapon : MonoBehaviour
{
    public ManualWeaponData data;
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public SpriteRenderer sr;
    public LayerMask enemyLayer;

    public Transform attackPoint;

    public int baseWeaponLevel;
    public int selecWeaponLevel;

    [Header("Attack Settings")]
    public float baseAttackPower;
    public float baseAttackSpeed;
    public float baseRange;
    public float baseMoveSpeed;
    public float baseJumpPower;
    public float baseDashPower;
    public float baseSuperJumpPower;

    public float selecAttackPower;
    public float selecAttackSpeed;
    public float selecRange;
    public float selecMoveSpeed;
    public float selecJumpPower;
    public float selecDashPower;
    public float selecSuperJumpPower;
    
    public float totalWeaponAttackPower;
    public float totalWeaponAttackSpeed;
    public float totalWeaponRange;
    public float totalWeaponMoveSpeed;
    public float totalWeaponJumpPower;
    public float totalWeaponDashPower;
    public float totalWeaponSuperJumpPower;

    public float totalAttackPower;
    public float totalAttackSpeed;
    public float totalRange;
    public float totalMoveSpeed;
    public float totalJumpPower;
    public float totalDashPower;
    public float totalSuperJumpPower;

    private void OnEnable() => PlayerCondition.OnPlayerStatUp += GetTotalStat;
    private void OnDisable() => PlayerCondition.OnPlayerStatUp -= GetTotalStat;

    protected virtual void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponentInChildren<SpriteRenderer>();

        selecWeaponLevel = 0;
        TotalStatSet();
    }

    public abstract void Attack();
    public abstract void DownAttack();
    public abstract void ChargingAttack();
    public abstract void DashAttack();
    
    public void BaseLevelUp()
    {
        baseWeaponLevel++;

        TotalStatSet();
    }

    public void SelecLevelUp()
    {
        selecWeaponLevel++;

        TotalStatSet();
    }

    // 총스탯 계산 함수
    public void GetTotalStat()
    {
        if (playerCondition != null)
        {
            totalWeaponAttackPower = baseAttackPower* selecAttackPower;
            totalWeaponAttackSpeed = baseAttackSpeed * selecAttackSpeed;
            totalWeaponRange = baseRange * selecRange;
            totalWeaponMoveSpeed = baseMoveSpeed * selecMoveSpeed;
            totalWeaponJumpPower = baseJumpPower * selecJumpPower;
            totalWeaponDashPower = baseDashPower * selecDashPower;
            totalWeaponSuperJumpPower = baseSuperJumpPower * selecSuperJumpPower;

            totalAttackPower = playerCondition.totalAttackPower + totalWeaponAttackPower;
            totalAttackSpeed = playerCondition.totalAttackPower + totalWeaponAttackSpeed;
            totalRange = totalWeaponRange;
            totalMoveSpeed = playerCondition.totalMoveSpeed + totalWeaponMoveSpeed;
            totalJumpPower = playerCondition.totalJumpPower + totalWeaponJumpPower;
            totalDashPower = playerCondition.totalDashPower + totalWeaponDashPower;
            totalSuperJumpPower = playerCondition.totalSuperJumpPower + totalWeaponSuperJumpPower;
        }
        else
        {
            Debug.Log("playerCondition 없음");
        }
    }

    // 게임 시작용 스텟 세팅 함수
    public void TotalStatSet()
    {
        baseAttackPower = data.baseAttackPowerList[baseWeaponLevel];
        baseAttackSpeed = data.baseAttackSpeedList[baseWeaponLevel];
        baseRange = data.baseRangeList[baseWeaponLevel];
        baseMoveSpeed = data.baseMoveSpeedList[baseWeaponLevel];
        baseJumpPower = data.basejumpPowerList[baseWeaponLevel];
        baseDashPower = data.baseDashPowerList[baseWeaponLevel];
        baseSuperJumpPower = data.selecSuperJumpPowerList[baseWeaponLevel];

        selecAttackPower = data.selecAttackSpeedList[selecWeaponLevel];
        selecAttackSpeed = data.selecAttackSpeedList[selecWeaponLevel];
        selecRange = data.selecRangeList[selecWeaponLevel];
        selecMoveSpeed = data.selecMoveSpeedList[selecWeaponLevel];
        selecJumpPower = data.selecjumpPowerList[selecWeaponLevel];
        selecDashPower = data.selecDashPowerList[selecWeaponLevel];
        selecSuperJumpPower = data.selecSuperJumpPowerList[selecWeaponLevel];

        GetTotalStat();
    }

    // 게임 초기화용 함수
    public void Clear()
    {
        baseWeaponLevel = 0;
        TotalStatSet();
    }
}

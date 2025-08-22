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

    private void Awake()
    {
        selecWeaponLevel = 0;
        StartStatSet();
    }

    protected virtual void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerCondition = GetComponentInParent<PlayerCondition>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponentInChildren<SpriteRenderer>();

        GetTotalStat();
    }

    public abstract void Attack();
    public abstract void DownAttack();
    public abstract void ChargingAttack();
    public abstract void DashAttack();
    
    public void BaseLevelUp()
    {
        Debug.Log("무기 영구 강화 레벨업");
        baseWeaponLevel++;

        baseAttackPower = data.baseAttackPowerList[baseWeaponLevel];
        baseAttackSpeed = data.baseAttackSpeedList[baseWeaponLevel];
        baseRange = data.baseRangeList[baseWeaponLevel];
        baseMoveSpeed = data.baseMoveSpeedList[baseWeaponLevel];
        baseJumpPower = data.basejumpPowerList[baseWeaponLevel];
        baseDashPower = data.baseDashPowerList[baseWeaponLevel];
        baseSuperJumpPower = data.selecSuperJumpPowerList[baseWeaponLevel];

        TotalStatSet();
        GetTotalStat();
    }
    public void SelecLevelUp()
    {
        Debug.Log("선택지 임시 무기 레벨업");
        selecWeaponLevel++;

        selecAttackPower = data.selecAttackSpeedList[selecWeaponLevel];
        selecAttackSpeed = data.selecAttackSpeedList[selecWeaponLevel];
        selecRange = data.selecRangeList[selecWeaponLevel];
        selecMoveSpeed = data.selecMoveSpeedList[selecWeaponLevel];
        selecJumpPower = data.selecjumpPowerList[selecWeaponLevel];
        selecDashPower = data.selecDashPowerList[selecWeaponLevel];
        selecSuperJumpPower = data.selecSuperJumpPowerList[selecWeaponLevel];

        TotalStatSet();
        GetTotalStat();

        playerCondition.GetTotalStat();
    }

    // 게임 시작용 스텟 세팅 함수
    public void StartStatSet()
    {
        Debug.Log("시작할 때, 무기 스탯 세팅");
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

        TotalStatSet();
    }

    // 무기의 종합 스탯
    public void TotalStatSet()
    {
        Debug.Log("무기의 종합 스탯 세팅");
        totalWeaponAttackPower = baseAttackPower * selecAttackPower;
        totalWeaponAttackSpeed = baseAttackSpeed * selecAttackSpeed;
        totalWeaponRange = baseRange * selecRange;
        totalWeaponMoveSpeed = baseMoveSpeed * selecMoveSpeed;
        totalWeaponJumpPower = baseJumpPower * selecJumpPower;
        totalWeaponDashPower = baseDashPower * selecDashPower;
        totalWeaponSuperJumpPower = baseSuperJumpPower * selecSuperJumpPower;
    }

    // 총스탯 계산 함수
    public void GetTotalStat()
    {
        if (playerCondition != null)
        {
            totalAttackPower = (playerCondition.baseAttackPower + totalWeaponAttackPower) * DataManager.Instance.selecAttackPowerDic[playerCondition.selecAttackPowerLv];
            totalAttackSpeed = (playerCondition.baseAttackSpeed + totalWeaponAttackSpeed) * DataManager.Instance.selecAttckSpeedDic[playerCondition.selecAttackSpeedLv];
            totalRange = totalWeaponRange;
            totalMoveSpeed = (playerCondition.baseMoveSpeed + totalWeaponMoveSpeed) * DataManager.Instance.selecMovePowerDic[playerCondition.selecMovePowerLv][0];
            totalJumpPower = (playerCondition.baseJumpPower + totalWeaponJumpPower) * DataManager.Instance.selecMovePowerDic[playerCondition.selecMovePowerLv][1];
            totalDashPower = (playerCondition.baseDashPower + totalWeaponDashPower) * DataManager.Instance.selecActPowerDic[playerCondition.selecActPowerLv][0];
            totalSuperJumpPower = (playerCondition.baseSuperJumpPower + totalWeaponSuperJumpPower) * DataManager.Instance.selecActPowerDic[playerCondition.selecActPowerLv][1];
        }
        else
        {
            totalAttackPower = totalWeaponAttackPower;
            totalAttackSpeed = totalWeaponAttackSpeed;
            totalRange = totalWeaponRange;
            totalMoveSpeed = totalWeaponMoveSpeed;
            totalJumpPower = totalWeaponJumpPower;
            totalDashPower = totalWeaponDashPower;
            totalSuperJumpPower = totalWeaponSuperJumpPower;
            Debug.Log("playerCondition 없음");
        }
    }

    // 게임 초기화용 함수
    public void Clear()
    {
        baseWeaponLevel = 0;
        TotalStatSet();
    }
}

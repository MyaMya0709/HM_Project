using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class IManualWeapon : MonoBehaviour
{
    public ManualWeaponData data;
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public SpriteRenderer sr;
    public LayerMask enemyLayer;
    public Animator anim;

    public Transform attackPoint;

    public int baseWeaponLevel;
    public int selecWeaponLevel;

    [Header("Attack Settings")]
    public float baseAttackPower;
    public float baseAttackSpeed;
    public float baseRange;
    public float baseMoveSpeed;
    public float baseJumpPower;
    public float baseDashCooltime;
    public float baseSuperJumpCooltime;
    public float baseDownAttackCooltime;

    public float selecAttackPower;
    public float selecAttackSpeed;
    public float selecRange;
    public float selecMoveSpeed;
    public float selecJumpPower;
    public float selecDashCooltime;
    public float selecSuperJumpCooltime;
    public float selecDownAttackCooltime;

    public float totalWeaponAttackPower;
    public float totalWeaponAttackSpeed;
    public float totalWeaponRange;
    public float totalWeaponMoveSpeed;
    public float totalWeaponJumpPower;
    public float totalWeaponDashCooltime;
    public float totalWeaponSuperJumpCooltime;
    public float totalWeaponDownAttackCooltime;

    public float totalAttackPower;
    public float totalAttackSpeed;
    public float totalRange;
    public float totalMoveSpeed;
    public float totalJumpPower;
    public float totalDashCooltime;
    public float totalSuperJumpCooltime;
    public float totalDownAttackCooltime;

    [Header("Weapon Effect Check")]
    public bool mutipleAttack = false;
    public bool isStun = false;
    public bool isDashAttack = true;

    [Header("Particle")]
    public GameObject chargingParticle;


    private void Awake()
    {
        
        
    }

    protected virtual void Start()
    {
        selecWeaponLevel = 0;
        StartStatSet();

        playerController = transform.parent.parent.GetComponent<PlayerController>();
        playerCondition = transform.parent.parent.GetComponent<PlayerCondition>();
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

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
        baseJumpPower = data.baseJumpPowerList[baseWeaponLevel];
        baseDashCooltime = data.baseDashCooltimeList[baseWeaponLevel];
        baseSuperJumpCooltime = data.baseSuperJumpCooltimeList[baseWeaponLevel];
        baseDownAttackCooltime = data.baseDownAttackCooltimeList[baseWeaponLevel];

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
        selecJumpPower = data.selecJumpPowerList[selecWeaponLevel];
        selecDashCooltime = data.selecDashCooltimeList[selecWeaponLevel];
        selecSuperJumpCooltime = data.selecSuperJumpCooltimeList[selecWeaponLevel];
        selecDownAttackCooltime = data.selecDownAttackCooltimeList[selecWeaponLevel];

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
        baseJumpPower = data.baseJumpPowerList[baseWeaponLevel];
        baseDashCooltime = data.baseDashCooltimeList[baseWeaponLevel];
        baseSuperJumpCooltime = data.selecSuperJumpCooltimeList[baseWeaponLevel];
        baseDownAttackCooltime = data.baseDownAttackCooltimeList[baseWeaponLevel];

        selecAttackPower = data.selecAttackSpeedList[selecWeaponLevel];
        selecAttackSpeed = data.selecAttackSpeedList[selecWeaponLevel];
        selecRange = data.selecRangeList[selecWeaponLevel];
        selecMoveSpeed = data.selecMoveSpeedList[selecWeaponLevel];
        selecJumpPower = data.selecJumpPowerList[selecWeaponLevel];
        selecDashCooltime = data.selecDashCooltimeList[selecWeaponLevel];
        selecSuperJumpCooltime = data.selecSuperJumpCooltimeList[selecWeaponLevel];
        selecDownAttackCooltime = data.selecDownAttackCooltimeList[selecWeaponLevel];

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
        totalWeaponDashCooltime = baseDashCooltime * selecDashCooltime;
        totalWeaponSuperJumpCooltime = baseSuperJumpCooltime * selecSuperJumpCooltime;
        totalWeaponDownAttackCooltime = baseDownAttackCooltime * selecDownAttackCooltime;
    }
    // 총스탯 계산 함수
    public void GetTotalStat()
    {
        if (playerCondition == null) playerCondition = transform.parent.parent.GetComponent<PlayerCondition>();

        totalAttackPower = (playerCondition.baseAttackPower + totalWeaponAttackPower) * DataManager.Instance.selecAttackPowerDic[playerCondition.selecAttackPowerLv] * playerCondition.TotalStatBuff(StatType.AttackPower);
        totalAttackSpeed = (playerCondition.baseAttackSpeed + totalWeaponAttackSpeed) * DataManager.Instance.selecAttckSpeedDic[playerCondition.selecAttackSpeedLv] * playerCondition.TotalStatBuff(StatType.AttackSpeed);
        totalRange = totalWeaponRange;
        totalMoveSpeed = (playerCondition.baseMoveSpeed + totalWeaponMoveSpeed) * DataManager.Instance.selecMovePowerDic[playerCondition.selecMovePowerLv][0] * playerCondition.TotalStatBuff(StatType.MoveSpeed);
        totalJumpPower = (playerCondition.baseJumpPower + totalWeaponJumpPower) * DataManager.Instance.selecMovePowerDic[playerCondition.selecMovePowerLv][1] * playerCondition.TotalStatBuff(StatType.JumpPower);
        totalDashCooltime = (playerCondition.baseDashCooltime + totalWeaponDashCooltime) * DataManager.Instance.selecActPowerDic[playerCondition.selecActPowerLv][0] * playerCondition.TotalStatBuff(StatType.DashCooltime);
        totalSuperJumpCooltime = (playerCondition.baseSuperJumpCooltime + totalWeaponSuperJumpCooltime) * DataManager.Instance.selecActPowerDic[playerCondition.selecActPowerLv][1] * playerCondition.TotalStatBuff(StatType.SuperJumpCooltime);
        totalDownAttackCooltime = (playerCondition.baseDownAttackCooltime + totalWeaponDownAttackCooltime) * DataManager.Instance.selecActPowerDic[playerCondition.selecActPowerLv][2] * playerCondition.TotalStatBuff(StatType.DownAttackCooltime);


        // 인게임 플레이어가 존재할 경우
        if (playerController == null) playerController = transform.parent.parent.GetComponent<PlayerController>();
        if (anim == null) anim = GetComponent<Animator>();

        // 쿨타임 설정 및 애니메이션 재생 속도 세팅
        playerController.attackingTime = 1f / totalAttackSpeed;
        anim.SetFloat("AttackSpeed", totalAttackSpeed / (1 - data.before_Attack_DelayRatio - data.after_Attack_DelayRatio));
        playerController.animator.SetFloat("AttackSpeed", totalAttackSpeed / (1 - data.before_Attack_DelayRatio - data.after_Attack_DelayRatio));

        playerController.chargeAttackingTime = 1f / totalAttackSpeed / data.chargeAttackSpeedMultiple;
        anim.SetFloat("ChargeAttackSpeed", totalAttackSpeed / (1 - data.before_ChargeAttack_DelayRatio - data.after_ChargeAttack_DelayRatio));
        playerController.animator.SetFloat("ChargeAttackSpeed", totalAttackSpeed / (1 - data.before_ChargeAttack_DelayRatio - data.after_ChargeAttack_DelayRatio));

    }

    // 게임 초기화용 함수
    public void Clear()
    {
        baseWeaponLevel = 0;
        TotalStatSet();
    }
}

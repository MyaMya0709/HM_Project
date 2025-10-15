using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// [수정 필요] PlayerController와 데이터 로드 순서 확실히 할것
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCondition : MonoBehaviour
{
    public PlayerController controller;
    public IManualWeapon curWeapon;
    public CharacterData characterData;
    public PlayerData playerData;

    [Header("Stats")]
    public float baseAttackPower;
    public float baseAttackSpeed;
    public float baseMoveSpeed;
    public float baseJumpPower;
    public float baseDashCooltime;
    public float baseSuperJumpCooltime;
    public float baseDownAttackCooltime;
    public int baseMasteryStat;

    public int selecAttackPowerLv;
    public int selecAttackSpeedLv;
    public int selecMovePowerLv;
    public int selecActPowerLv;
    public int selecMasteryLv;

    public float totalAttackPower;
    public float totalAttackSpeed;
    public float totalMoveSpeed;
    public float totalJumpPower;
    public float totalDashCooltime;
    public float totalSuperJumpCooltime;
    public float totalDownAttackCooltime;
    public int totalMasteryStat;

    public int curGold = 0;
    public float maxExp;
    public float curExp = 0;
    public float curBuff = 0f;

    public int maxLevel = 50;
    public int playerLevel;

    [SerializeField] private Image expBar;
    public List<IAutoWeapon> autoWeapons;

    public static event Action OnPlayerLevelUp;
    public List<IBuff> buffList;

    protected void Awake()
    {
        expBar = FindAnyObjectByType<UI_Main>().expBar;
        controller = GetComponent<PlayerController>();

        playerData = GameManager.Instance.SetPlayerData();
        characterData = GameManager.Instance.SetCharacterData();
        playerLevel = 0;
        maxExp = DataManager.Instance.maxExpDic[playerLevel];
        curExp = 0;
        UpdateExp();
    }

    private void Start()
    {
        if (controller.curWeapon == null) curWeapon = GameManager.Instance.weaponData;
        else curWeapon = controller.curWeapon;

        autoWeapons = new List<IAutoWeapon>();

        StartStatSetting();
    }

    public void LevelUP()
    {
        playerLevel++;
        curExp = 0f;
        maxExp = DataManager.Instance.maxExpDic[playerLevel];

        // TODO : 플레이어 스텟 계산

        // 플레이어 렙업 이벤트 발생 호출
        OnPlayerLevelUp?.Invoke();

        UpdateExp();
    }

    public void UpdateExp()
    {
        //expBar 업데이트
        expBar.fillAmount = Mathf.Min(1, curExp / maxExp);

        //레벨업 가능 여부 판단
        if (curExp >= maxExp && playerLevel < maxLevel)
        {
            //초과된 경험치는 다음 레벨로 이전 - 로직 추가
            LevelUP();
        }
    }

    public void StartStatSetting()
    {
        // 영구 스탯 + 캐릭터 보너스 스탯 불러오기
        baseAttackPower = DataManager.Instance.attackPowerDic[playerData.attackPowerLevel];
        baseAttackSpeed = DataManager.Instance.attackSpeedDic[playerData.attackSpeedLevel];
        baseMoveSpeed = DataManager.Instance.movePowerDic[playerData.movePowerLevel][0];
        baseJumpPower = DataManager.Instance.movePowerDic[playerData.movePowerLevel][1];
        baseDashCooltime = DataManager.Instance.actPowerDic[playerData.actPowerLevel][0];
        baseSuperJumpCooltime = DataManager.Instance.actPowerDic[playerData.actPowerLevel][1];
        baseDownAttackCooltime = DataManager.Instance.actPowerDic[playerData.actPowerLevel][2];
        baseMasteryStat = DataManager.Instance.masteryStatDic[playerData.masteryLevel];
        Debug.Log("기본 스탯 로드");

        // 보너스 스탯 적용
        if (characterData.bonusStatValue != null && characterData.bonusStatType != null)
        {
            for (int i = 0; i < characterData.bonusStatType.Count; i++)
            {
                StatType statType = characterData.bonusStatType[i];
                switch (statType)
                {
                    case StatType.AttackPower:
                        baseAttackPower += characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : attackPower");
                        break;

                    case StatType.AttackSpeed:
                        baseAttackSpeed += characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : attackSpeed");
                        break;

                    case StatType.MoveSpeed:
                        baseMoveSpeed += characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : MoveSpeed");
                        break;

                    case StatType.JumpPower:
                        baseJumpPower += characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : JumpPower");
                        break;

                    case StatType.DashCooltime:
                        baseDashCooltime -= characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : DashCooltime");
                        break;

                    case StatType.SuperJumpCooltime:
                        baseSuperJumpCooltime -= characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : SuperJumpCooltime");
                        break;

                    case StatType.DownAttackCooltime:
                        baseDownAttackCooltime -= characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : DownAttackCooltime");
                        break;

                    case StatType.Mastery:
                        baseMasteryStat += (int)characterData.bonusStatValue[i];
                        Debug.Log("스탯 적용 : Mastery");
                        break;

                    default:
                        Debug.Log("스탯적용 불가");
                        break;
                }
            }
            Debug.Log("보너스 스탯 적용");
        }

        // 선택지 스탯 레벨 초기화
        selecAttackPowerLv = 0;
        selecAttackSpeedLv = 0;
        selecMovePowerLv = 0;
        selecActPowerLv = 0;
        selecMasteryLv = 0;

        // 무기 스탯 계산
        controller.curWeapon.TotalStatSet();

        // 초기화된 종합 스탯 계산
        GetTotalStat();

        // 무기의 종합 스탯 계산
        controller.curWeapon.GetTotalStat();

        // 자동무기 리스트 초기화
        // 자동무기 레벨 초기화
    }

    public void GetTotalStat()
    {
        Debug.Log("플레이어 종합 스탯 세팅");
        totalAttackPower = (baseAttackPower + curWeapon.totalWeaponAttackPower) * DataManager.Instance.selecAttackPowerDic[selecAttackPowerLv] * TotalStatBuff(StatType.AttackPower);
        totalAttackSpeed = (baseAttackSpeed + curWeapon.totalWeaponAttackSpeed) * DataManager.Instance.selecAttckSpeedDic[selecAttackSpeedLv] * TotalStatBuff(StatType.AttackSpeed);
        totalMoveSpeed = (baseMoveSpeed + curWeapon.totalWeaponMoveSpeed) * DataManager.Instance.selecMovePowerDic[selecMovePowerLv][0] * TotalStatBuff(StatType.MoveSpeed);
        totalJumpPower = (baseJumpPower + curWeapon.totalWeaponJumpPower) * DataManager.Instance.selecMovePowerDic[selecMovePowerLv][1] * TotalStatBuff(StatType.JumpPower);
        totalDashCooltime = (baseDashCooltime + curWeapon.totalWeaponDashCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][0] * TotalStatBuff(StatType.DashCooltime);
        totalSuperJumpCooltime = (baseSuperJumpCooltime + curWeapon.totalWeaponSuperJumpCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][1] * TotalStatBuff(StatType.SuperJumpCooltime);
        totalDownAttackCooltime = (baseDownAttackCooltime + curWeapon.totalWeaponDownAttackCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][2] * TotalStatBuff(StatType.DownAttackCooltime);
        totalMasteryStat = (int)(baseMasteryStat * DataManager.Instance.selecMasteryStatDic[selecMasteryLv] * TotalStatBuff(StatType.Mastery));
    }

    public void SelecStatLevelUP(StatLvType stat)
    {
        switch (stat)
        {
            case StatLvType.AttackPower:
                selecAttackPowerLv++;
                totalAttackPower = (baseAttackPower + curWeapon.totalWeaponAttackPower) * DataManager.Instance.selecAttackPowerDic[selecAttackPowerLv] * TotalStatBuff(StatType.AttackPower);
                break;
            case StatLvType.AttackSpeed:
                selecAttackSpeedLv++;
                totalAttackSpeed = (baseAttackSpeed + curWeapon.totalWeaponAttackSpeed) * DataManager.Instance.selecAttckSpeedDic[selecAttackSpeedLv] * TotalStatBuff(StatType.AttackSpeed);
                break;
            case StatLvType.MovePower:
                selecMovePowerLv++;
                totalMoveSpeed = (baseMoveSpeed + curWeapon.totalWeaponMoveSpeed) * DataManager.Instance.selecMovePowerDic[selecMovePowerLv][0] * TotalStatBuff(StatType.MoveSpeed);
                totalJumpPower = (baseJumpPower + curWeapon.totalWeaponJumpPower) * DataManager.Instance.selecMovePowerDic[selecMovePowerLv][1] * TotalStatBuff(StatType.JumpPower);
                break;
            case StatLvType.ActPower:
                selecActPowerLv++;
                totalDashCooltime = (baseDashCooltime + curWeapon.totalWeaponDashCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][0] * TotalStatBuff(StatType.DashCooltime);
                totalSuperJumpCooltime = (baseSuperJumpCooltime + curWeapon.totalWeaponSuperJumpCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][1] * TotalStatBuff(StatType.SuperJumpCooltime);
                totalDownAttackCooltime = (baseDownAttackCooltime + curWeapon.totalWeaponDownAttackCooltime) * DataManager.Instance.selecActPowerDic[selecActPowerLv][2] * TotalStatBuff(StatType.DownAttackCooltime);
                break;
            case StatLvType.Mastery:
                selecMasteryLv++;
                totalMasteryStat = (int)(baseMasteryStat * DataManager.Instance.selecMasteryStatDic[selecMasteryLv] * TotalStatBuff(StatType.Mastery));
                break;
            default:
                Debug.Log("존재하지 않는 StatType");
                break;
        }
        curWeapon.GetTotalStat();
    }

    public void AutoWeaponSet(int weaponID)
    {
        bool isOverlap = false;

        // 무기 중복 확인
        foreach (IAutoWeapon WP in autoWeapons)
        {
            if (WP.data.weaponID == weaponID)
            {
                Debug.Log("중복무기있음");
                isOverlap = true;
                WP.SelecLevelUp();
                break;
            }
        }
        if (!isOverlap)
        {
            // 프리펩 가져오기
            GameObject autoWeapon = DataManager.Instance.autoPrefabList[weaponID - 200];
            // 데이터 가져오기
            autoWeapon.GetComponent<IAutoWeapon>().data = DataManager.Instance.autoDataList[weaponID - 200];
            // 무기 실체화
            Transform weaponPos = gameObject.transform.Find("AutoWeapons");
            GameObject clone = Instantiate(autoWeapon, weaponPos);
            // 플레이어 무기 리스트에 추가
            autoWeapons.Add(clone.GetComponent<IAutoWeapon>());
        }
    }

    // [수정 필요] 버프 실행 로직
    public IEnumerator OnBuff(IBuff buff)
    {
        switch (buff)
        {
            case Buff_Stat:

                Debug.Log("버프 시작");
                Buff_Stat statBuff = buff.GetComponent<Buff_Stat>();

                // 버프 리스트 순회 하면서 동일한 유형의 버프 찾기
                foreach (IBuff b in buffList)
                {
                    // 동일 유형의 버프 체크, 중복시 이전 버프 제거
                    if(b.TryGetComponent<Buff_Stat>(out Buff_Stat sb) && sb.statType == statBuff.statType) buffList.Remove(sb);

                }

                buffList.Add(buff);

                GetTotalStat();
                curWeapon.GetTotalStat();

                yield return new WaitForSeconds(buff.duration);

                if (buffList.Contains(buff))
                {
                    buffList.Remove(buff);
                    GetTotalStat();
                    curWeapon.GetTotalStat();
                }
                break;
        }

        Destroy(buff.gameObject);
    }

    public float TotalStatBuff(StatType type)
    {
        float totalbuff = 0f;

        if (buffList != null)
        {
            // 받은 버프 리스트에서 스탯 타입으로 버프 찾기
            foreach (IBuff buff in buffList)
            {
                // Buff_Stat이고 버프를 넣을 스탯과 같은 버프인지 체크
                if (buff.TryGetComponent<Buff_Stat>(out Buff_Stat statBuff) && statBuff.statType == type)
                {
                    // 버프 디버프 체크
                    switch (statBuff.buffValue > 1)
                    {
                        // 버프
                        case true:
                            totalbuff += statBuff.buffValue - 1;
                            break;

                        // 디버프
                        case false:
                            if(statBuff.buffValue == 0) Debug.Log("buffValue : 0");
                            totalbuff -= statBuff.buffValue;
                            break;
                    }
                }
            }
        }
        return totalbuff += 1;
    }
}
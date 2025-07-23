using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCondition : MonoBehaviour
{
    public PlayerController controller;

    [Header("Stats")]
    public PlayerData playerData;

    public float baseMoveSpeed;
    public float baseAttackPower;
    public float baseAttackSpeed;

    public int selecMoveSpeedLv;
    public int selecAttackPowerLv;
    public int selecAttackSpeedLv;

    public float totalMoveSpeed;
    public float totalAttackPower;
    public float totalAttackSpeed;

    public int curGold = 0;
    public float maxExp;
    public float curExp = 0;
    public float curBuff = 0f;

    public int maxLevel = 50;
    public int playerLevel;

    [SerializeField] private Image expBar;
    public List<IAutoWeapon> autoWeapons;

    protected void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        playerData = DataManager.Instance.playerData;
        playerLevel = 0;
        maxExp = DataManager.Instance.statUpTableData.maxExpList[playerLevel];
        curExp = 0;
        UpdateExp();

        StartStatSetting();
        autoWeapons = new List<IAutoWeapon>();
    }

    public void LevelUP()
    {
        playerLevel++;
        curExp = 0f;
        maxExp = DataManager.Instance.statUpTableData.maxExpList[playerLevel];

        // TODO : 플레이어 스텟 계산

        Time.timeScale = 0f;
        UIManager.Instance.selecUI.gameObject.SetActive(true);

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
        // 영구 스탯 불러오기
        baseMoveSpeed = DataManager.Instance.statUpTableData.moveSpeedDic[playerData.moveSpeedLevel];
        baseAttackPower = DataManager.Instance.statUpTableData.attackPowerDic[playerData.attackPowerLevel];
        baseAttackSpeed = DataManager.Instance.statUpTableData.attackSpeedDic[playerData.attackSpeedLevel];

        // 선택지 스탯 레벨 초기화
        selecMoveSpeedLv = 0;
        selecAttackPowerLv = 0;
        selecAttackSpeedLv = 0;

        // 초기화된 종합 스탯 계산
        totalMoveSpeed = baseMoveSpeed * DataManager.Instance.selecMoveSpeedDic[selecMoveSpeedLv];
        totalAttackPower = baseAttackPower * DataManager.Instance.selecAttackPowerDic[selecAttackPowerLv];
        totalAttackSpeed = baseAttackSpeed * DataManager.Instance.selecAttckSpeedDic[selecAttackSpeedLv];

        // 자동무기 리스트 초기화
        // 자동무기 레벨 초기화
    }

    public void SelecStatLevelUP(StatType stat)
    {
        switch (stat)
        {
            case StatType.moveSpeed:
                selecMoveSpeedLv++;
                break;
            case StatType.attackPower:
                selecAttackPowerLv++;
                break;
            case StatType.attackSpeed:
                selecAttackSpeedLv++;
                break;
            default:
                Debug.Log("존재하지 않는 StatType");
                break;
        }
        StatSetting(stat);
    }

    public void StatSetting(StatType stat)
    {
        switch (stat)
        {
            case StatType.moveSpeed:
                totalMoveSpeed = baseMoveSpeed * DataManager.Instance.selecMoveSpeedDic[selecMoveSpeedLv];
                break;

            case StatType.attackPower:
                totalAttackPower = baseAttackPower * DataManager.Instance.selecAttackPowerDic[selecAttackPowerLv];
                break;

            case StatType.attackSpeed:
                totalAttackSpeed = baseAttackSpeed * DataManager.Instance.selecAttckSpeedDic[selecAttackSpeedLv];
                break;

            default:
                Debug.Log("존재하지 않는 StatType");
                break;
        }
    }

    public void AutoWeaponSet(int weaponID)
    {
        bool isOverlap = false;

        // 무기 중복 확인
        foreach (IAutoWeapon WP in autoWeapons)
        {
            if (WP.data.weaponID == weaponID)
            {
                WP.selecWeaponLevel++;
                isOverlap = true;
                break;
            }
        }

        if (!isOverlap)
        {
            // 프리펩 가져오기
            Object autoWeapon = DataManager.Instance.autoPrefabDic[weaponID];
            // 데이터 가져오기
            autoWeapon.GetComponent<IAutoWeapon>().data = DataManager.Instance.autoDataDic[weaponID];
            // 플레이어 무기 리스트에 추가
            autoWeapons.Add(autoWeapon.GetComponent<IAutoWeapon>());
            // 무기 실체화
            Instantiate(autoWeapon, gameObject.transform.Find("AutoWeapons"));
        }

    }
}
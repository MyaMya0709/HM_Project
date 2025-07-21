using System.Collections;
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

    public float selecMoveSpeed;
    public float selecAttackPower;
    public float selecAttackSpeed;

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

        StatSetting();
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
            LevelUP();
        }
    }

    public void StatSetting()
    {
        baseMoveSpeed = DataManager.Instance.statUpTableData.moveSpeedDic[playerData.moveSpeedLevel];
        baseAttackPower = DataManager.Instance.statUpTableData.attackPowerDic[playerData.attackPowerLevel];
        baseAttackSpeed = DataManager.Instance.statUpTableData.attackSpeedDic[playerData.attackSpeedLevel];

        totalMoveSpeed = baseMoveSpeed;
        totalAttackPower = baseAttackPower;
        totalAttackSpeed = baseAttackSpeed;
    }
}
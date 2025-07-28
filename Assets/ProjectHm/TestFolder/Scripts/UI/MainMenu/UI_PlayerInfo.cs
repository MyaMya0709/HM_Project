using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfo : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] private RectTransform characterPanel;

    [SerializeField] private TMP_Text playerLevel;
    [SerializeField] private TMP_Text ATKPowerLevel;
    [SerializeField] private TMP_Text ATKSpeedLevel;
    [SerializeField] private TMP_Text moveSpeedLevel;
    [SerializeField] private TMP_Text jumpPowerLevel;
    [SerializeField] private TMP_Text statLevel;

    [SerializeField] private TMP_Text levelUpCost;
    [SerializeField] private TMP_Text ATKPower;
    [SerializeField] private TMP_Text ATKSpeed;
    [SerializeField] private TMP_Text moveSpeed;
    [SerializeField] private TMP_Text jumpPower;
    [SerializeField] private TMP_Text stat;

    [SerializeField] private Button playerLevelUp;
    [SerializeField] private Button ATKPowerUp;
    [SerializeField] private Button ATKSpeedUp;
    [SerializeField] private Button moveSpeedUp;
    [SerializeField] private Button jumpPowerUp;
    [SerializeField] private Button statup;

    [SerializeField] private TMP_Text curGold;

    [SerializeField] private Button levelReset;
    [SerializeField] private Button saveButton;

    public int statUpPoint;

    private void Awake()
    {
        playerData = GameManager.Instance.playerData;
        if (playerData == null) Debug.Log("UI_PlayerInfo에 playerData로드 안됨");
        PlayerInfoSet();
        ButtonSet();
        statUpPoint = playerData.playerLevel;
    }

    public void PlayerInfoSet()
    {
        playerLevel.text = playerData.playerLevel.ToString();

        ATKPowerLevel.text = playerData.attackPowerLevel.ToString();
        ATKSpeedLevel.text = playerData.attackSpeedLevel.ToString();
        moveSpeedLevel.text = playerData.moveSpeedLevel.ToString();
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        levelUpCost.text = DataManager.Instance.levelUpCostDic[playerData.playerLevel].ToString();

        ATKPower.text = DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString();
        ATKSpeed.text = DataManager.Instance.attackSpeedDic[playerData.attackSpeedLevel].ToString();
        moveSpeed.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();

        curGold.text = playerData.haveGold.ToString();
    }

    public void ButtonSet()
    {
        if (playerData.playerLevel >= 50)
            playerLevelUp.gameObject.SetActive(false);
        else
            playerLevelUp.gameObject.SetActive(true);
    }

    public void PlayerLevelUp()
    {
        if (gameObject.activeSelf)
        {
            if (playerData.haveGold >= DataManager.Instance.levelUpCostDic[playerData.playerLevel] || playerData.playerLevel <= 49)
            {
                playerData.haveGold -= DataManager.Instance.levelUpCostDic[playerData.playerLevel];
                playerData.playerLevel++;
                statUpPoint++;

                //바뀐 정보값 수정
                playerLevel.text = playerData.playerLevel.ToString();
                levelUpCost.text = DataManager.Instance.levelUpCostDic[playerData.playerLevel].ToString();

                //레벨업 후 최대레벨 달성했으면 버튼 숨김
                if (playerData.playerLevel >= 50) playerLevelUp.gameObject.SetActive(false);
            }

            else if (playerData.haveGold < DataManager.Instance.levelUpCostDic[playerData.playerLevel])
            {
                // 알림창 - 골드부족
                Debug.Log("레벨업 골드 부족");
            }

            else if (playerData.playerLevel > 49)
            {
                //알림창 - 레벨 최대치
                Debug.Log("레벨 최대치");
            }

        }
    }
    public void StatLevelUp(StatType statType)
    {
        if (gameObject.activeSelf)
        {
            switch (statType)
            {
                case StatType.attackPower:
                    if (statUpPoint != 0 || playerData.attackPowerLevel <= 15)
                    {
                        playerData.attackPowerLevel++;
                        statUpPoint--;

                        ATKPowerLevel.text = $"Lv.{playerData.attackPowerLevel.ToString()}";
                        //보너스 스탯이 있을때 조건으로 보너스 스탯 표시 분기점 만들기
                        ATKPower.text = $"{DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString()}";

                    }


                    break;
            }

            
        }
            
    }
}
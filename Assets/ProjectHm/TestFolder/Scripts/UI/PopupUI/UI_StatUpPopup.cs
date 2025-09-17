using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatUpPopup : MonoBehaviour
{
    [Header("PlayerData")]
    public int playerLevel;
    public int statUpPoint;

    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int movePowerLevel;
    public int actPowerLevel;
    public int masteryLevel;

    [Header("Level")]
    [SerializeField] private TMP_Text ATKPowerLevelTMP;
    [SerializeField] private TMP_Text ATKSpeedLevelTMP;
    [SerializeField] private TMP_Text movePowerLevelTMP;
    [SerializeField] private TMP_Text actPowerLevelTMP;
    [SerializeField] private TMP_Text masteryLevelTMP;
    
    [Header("Stat")]
    [SerializeField] private TMP_Text ATKPowerTMP;
    [SerializeField] private TMP_Text ATKSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;
    [SerializeField] private TMP_Text dashPowerTMP;
    [SerializeField] private TMP_Text superJumpPowerTMP;
    [SerializeField] private TMP_Text masteryStatTMP;

    [Header("StatUpBtn")]
    [SerializeField] private Button ATKPowerUpBtn;
    [SerializeField] private Button ATKSpeedUpBtn;
    [SerializeField] private Button movePowerUpBtn;
    [SerializeField] private Button actPowerUpBtn;
    [SerializeField] private Button masteryStatUpBtn;

    [Header("StatDownBtn")]
    [SerializeField] private Button ATKPowerDownBtn;
    [SerializeField] private Button ATKSpeedDownBtn;
    [SerializeField] private Button movePowerDownBtn;
    [SerializeField] private Button actPowerDownBtn;
    [SerializeField] private Button masteryStatDownBtn;

    [SerializeField] private Button saveButton;

    [SerializeField] private UI_InfoPanel infoPanel;

    private void OnEnable()
    {
        DataSet();
        StatPopupSet();
        StatButtonSet();
    }

    public void DataSet()
    {
        playerLevel = GameManager.Instance.playerLevel;
        statUpPoint = GameManager.Instance.statUpPoint;

        attackPowerLevel = GameManager.Instance.attackPowerLevel;
        attackSpeedLevel = GameManager.Instance.attackSpeedLevel;
        movePowerLevel = GameManager.Instance.movePowerLevel;
        actPowerLevel = GameManager.Instance.actPowerLevel;
        masteryLevel = GameManager.Instance.masteryLevel;

        Debug.Log("DataSet 완료");
    }

    // 버튼 오브젝트 활성/비활성 업데이트
    public void StatButtonSet()
    {
        if (attackPowerLevel >= 10) ATKPowerUpBtn.gameObject.SetActive(false);
        else ATKPowerUpBtn.gameObject.SetActive(true);
        if (attackPowerLevel <= 0) ATKPowerDownBtn.gameObject.SetActive(false);
        else ATKPowerDownBtn.gameObject.SetActive(true);

        if (attackSpeedLevel >= 10) ATKSpeedUpBtn.gameObject.SetActive(false);
        else ATKSpeedUpBtn.gameObject.SetActive(true);
        if (attackSpeedLevel <= 0) ATKSpeedDownBtn.gameObject.SetActive(false);
        else ATKSpeedDownBtn.gameObject.SetActive(true);

        if (movePowerLevel >= 10) movePowerUpBtn.gameObject.SetActive(false);
        else movePowerUpBtn.gameObject.SetActive(true);
        if (movePowerLevel <= 0) movePowerDownBtn.gameObject.SetActive(false);
        else movePowerDownBtn.gameObject.SetActive(true);

        if (actPowerLevel >= 10) actPowerUpBtn.gameObject.SetActive(false);
        else actPowerUpBtn.gameObject.SetActive(true);
        if (actPowerLevel <= 0) actPowerDownBtn.gameObject.SetActive(false);
        else actPowerDownBtn.gameObject.SetActive(true);

        if (masteryLevel >= 10) masteryStatUpBtn.gameObject.SetActive(false);
        else masteryStatUpBtn.gameObject.SetActive(true);
        if (masteryLevel <= 0) masteryStatDownBtn.gameObject.SetActive(false);
        else masteryStatDownBtn.gameObject.SetActive(true);

        Debug.Log("StatButtonSet 완료");
    }

    // 모든 레벨/스탯 업데이트
    public void StatPopupSet()
    {
        ATKPowerLevelTMP.text = $"Lv.{attackPowerLevel}";
        ATKSpeedLevelTMP.text = $"Lv.{attackSpeedLevel}";
        movePowerLevelTMP.text = $"Lv.{movePowerLevel}";
        actPowerLevelTMP.text = $"Lv.{actPowerLevel}";
        masteryLevelTMP.text = $"Lv.{masteryLevel}";

        ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[attackPowerLevel]}";
        ATKSpeedTMP.text = $"{DataManager.Instance.attackSpeedDic[attackSpeedLevel]}";

        moveSpeedTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][0]}";
        jumpPowerTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][1]}";
        dashPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][0]}";
        superJumpPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][1]}";
        masteryStatTMP.text = $"{DataManager.Instance.masteryStatDic[masteryLevel]}";

    }

    // 함수 안에서 버튼 오브젝트의 활성화/비활성화 결정 후 실행
    #region StatUp & Down
    public void ATKPowerLevelUP()
    {
        if (statUpPoint <= 0 || attackPowerLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (attackPowerLevel < 10)
            {
                attackPowerLevel++;
                statUpPoint--;

                ATKPowerLevelTMP.text = $"Lv.{attackPowerLevel}";
                ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[attackPowerLevel]}";

                // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
                if (attackPowerLevel >= 10) ATKPowerUpBtn.gameObject.SetActive(false);
                else if (attackPowerLevel == 1 && !ATKPowerDownBtn.gameObject.activeSelf) ATKPowerDownBtn.gameObject.SetActive(true);
                Debug.Log("ATKPowerLvUP!");
            }
        }
    }
    public void ATKSpeedLevelUP()
    {
        if (statUpPoint <= 0 || attackSpeedLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (attackSpeedLevel < 10)
            {
                attackSpeedLevel++;
                statUpPoint--;

                ATKSpeedLevelTMP.text = $"Lv.{attackSpeedLevel}";
                ATKSpeedTMP.text = $"{DataManager.Instance.attackSpeedDic[attackSpeedLevel]}";

                // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
                if (attackSpeedLevel >= 10) ATKSpeedUpBtn.gameObject.SetActive(false);
                else if (attackSpeedLevel == 1 && !ATKSpeedDownBtn.gameObject.activeSelf) ATKSpeedDownBtn.gameObject.SetActive(true);
                Debug.Log("ATKSpeedLvUP!");
            }
        }
    }
    public void MovePowerLevelUP()
    {
        if (statUpPoint <= 0 || movePowerLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (movePowerLevel < 10)
        {
            movePowerLevel++;
            statUpPoint--;

            movePowerLevelTMP.text = $"Lv.{movePowerLevel}";
            moveSpeedTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][0]}";
            jumpPowerTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][1]}";

            // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
            if (movePowerLevel >= 10) movePowerUpBtn.gameObject.SetActive(false);
            else if (movePowerLevel == 1 && !movePowerDownBtn.gameObject.activeSelf) movePowerDownBtn.gameObject.SetActive(true);
            Debug.Log("movePowerLvUP!");
        }
    }
    public void ActPowerLevelUP()
    {
        if (statUpPoint <= 0 || actPowerLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (actPowerLevel < 10)
        {
            actPowerLevel++;
            statUpPoint--;

            actPowerLevelTMP.text = $"Lv.{actPowerLevel}";
            dashPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][0]}";
            superJumpPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][1]}";

            // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
            if (actPowerLevel >= 10) actPowerUpBtn.gameObject.SetActive(false);
            else if (actPowerLevel == 1 && !actPowerDownBtn.gameObject.activeSelf) actPowerDownBtn.gameObject.SetActive(true);
            Debug.Log("actPowerLvUP!");
        }
    }
    public void MasteryLevelUP()
    {
        if (statUpPoint <= 0 || masteryLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (masteryLevel < 10)
            {
                masteryLevel++;
                statUpPoint--;

                masteryLevelTMP.text = $"Lv.{masteryLevel}";
                masteryStatTMP.text = $"{DataManager.Instance.masteryStatDic[masteryLevel]}";

                // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
                if (masteryLevel >= 10) masteryStatUpBtn.gameObject.SetActive(false);
                else if (masteryLevel == 1 && !masteryStatDownBtn.gameObject.activeSelf) masteryStatDownBtn.gameObject.SetActive(true);
                Debug.Log("MasteryLvUP!");
            }
        }
    }

    public void ATKPowerLevelDown()
    {
        if (attackPowerLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (attackPowerLevel > 0)
            {
                attackPowerLevel--;
                statUpPoint++;

                ATKPowerLevelTMP.text = $"Lv.{attackPowerLevel}";
                ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[attackPowerLevel]}";

                // 렙다운 버튼 클릭 후, 0렙이면 레벨다운 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (attackPowerLevel <= 0) ATKPowerDownBtn.gameObject.SetActive(false);
                else if (attackPowerLevel == 9 && !ATKPowerUpBtn.gameObject.activeSelf) ATKPowerUpBtn.gameObject.SetActive(true);
                Debug.Log("ATKPowerLv Down!");
            }
        }
    }
    public void ATKSpeedLevelDown()
    {
        if (attackSpeedLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (attackSpeedLevel > 0)
            {
                attackSpeedLevel--;
                statUpPoint++;

                ATKSpeedLevelTMP.text = $"Lv.{attackSpeedLevel}";
                ATKSpeedTMP.text = $"{DataManager.Instance.attackSpeedDic[attackSpeedLevel]}";

                // 렙다운 버튼 클릭 후, 0렙이면 레벨다운 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (attackSpeedLevel <= 0) ATKSpeedDownBtn.gameObject.SetActive(false);
                else if (attackSpeedLevel == 9 && !ATKSpeedUpBtn.gameObject.activeSelf) ATKSpeedUpBtn.gameObject.SetActive(true);
                Debug.Log("ATKSpeedLv Down!");
            }
        }
    }
    public void MovePowerLevelDown()
    {
        if (movePowerLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (movePowerLevel > 0)
            {
                movePowerLevel--;
                statUpPoint++;

                movePowerLevelTMP.text = $"Lv.{movePowerLevel}";
                moveSpeedTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][0]}";
                jumpPowerTMP.text = $"{DataManager.Instance.movePowerDic[movePowerLevel][1]}";

                // 렙다운 버튼 클릭 후, 0렙이면 레벨다운 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (movePowerLevel <= 0) movePowerDownBtn.gameObject.SetActive(false);
                else if (movePowerLevel == 9 && !movePowerUpBtn.gameObject.activeSelf) movePowerUpBtn.gameObject.SetActive(true);
                Debug.Log("moveSpeedLv Down!");
            }
        }
    }
    public void ActPowerLevelDown()
    {
        if (actPowerLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (actPowerLevel > 0)
            {
                actPowerLevel--;
                statUpPoint++;

                actPowerLevelTMP.text = $"Lv.{actPowerLevel}";
                dashPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][0]}";
                superJumpPowerTMP.text = $"{DataManager.Instance.actPowerDic[actPowerLevel][1]}";

                // 렙다운 버튼 클릭 후, 0렙이면 레벨다운 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (actPowerLevel <= 0) actPowerDownBtn.gameObject.SetActive(false);
                else if (actPowerLevel == 9 && !actPowerUpBtn.gameObject.activeSelf) actPowerUpBtn.gameObject.SetActive(true);
                Debug.Log("ActPowerLv Down!");
            }
        }
    }
    public void MasteryLevelDown()
    {
        if (masteryLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (masteryLevel > 0)
            {
                masteryLevel--;
                statUpPoint++;

                masteryLevelTMP.text = $"Lv.{masteryLevel}";
                masteryStatTMP.text = $"{DataManager.Instance.masteryStatDic[masteryLevel]}";

                // 렙다운 버튼 클릭 후, 0렙이면 레벨다운 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (masteryLevel <= 0) masteryStatDownBtn.gameObject.SetActive(false);
                else if (masteryLevel == 9 && !masteryStatUpBtn.gameObject.activeSelf) masteryStatUpBtn.gameObject.SetActive(true);
                Debug.Log("MasteryLv Down!");
            }
        }
    }
    #endregion

    public void OnSaveButton()
    {
        GameManager.Instance.playerLevel = playerLevel;
        GameManager.Instance.statUpPoint = statUpPoint;

        GameManager.Instance.attackPowerLevel = attackPowerLevel;
        GameManager.Instance.attackSpeedLevel = attackSpeedLevel;
        GameManager.Instance.movePowerLevel = movePowerLevel;
        GameManager.Instance.actPowerLevel = actPowerLevel;
        GameManager.Instance.masteryLevel = masteryLevel;

        GameManager.Instance.GetPlayerData();
        GameManager.Instance.SavePlayerData();
        infoPanel.InfoPanelSet();

        Debug.Log("Save 완료");

        gameObject.SetActive(false);
    }

    public void OnCancel()
    {
        DataSet();
        gameObject.SetActive(false);
    }
}

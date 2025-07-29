using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatUpPopup : MonoBehaviour
{
    public PlayerData data;

    [Header("Level")]
    [SerializeField] private TMP_Text ATKPowerLevelTMP;
    [SerializeField] private TMP_Text ATKSpeedLevelTMP;
    [SerializeField] private TMP_Text moveSpeedLevelTMP;
    [SerializeField] private TMP_Text jumpPowerLevelTMP;
    [SerializeField] private TMP_Text statLevelTMP;
    
    [Header("Stat")]
    [SerializeField] private TMP_Text ATKPowerTMP;
    [SerializeField] private TMP_Text ATKSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;
    [SerializeField] private TMP_Text statTMP;

    [Header("StatUpBtn")]
    [SerializeField] private Button ATKPowerUpBtn;
    [SerializeField] private Button ATKSpeedUpBtn;
    [SerializeField] private Button moveSpeedUpBtn;
    [SerializeField] private Button jumpPowerUpBtn;
    [SerializeField] private Button statupBtn;

    [Header("StatDownBtn")]
    [SerializeField] private Button ATKPowerDownBtn;
    [SerializeField] private Button ATKSpeedDownBtn;
    [SerializeField] private Button moveSpeedDownBtn;
    [SerializeField] private Button jumpPowerDownBtn;
    [SerializeField] private Button statDownBtn;

    [SerializeField] private Button saveButton;
    public int statUpPoint;

    private void OnEnable()
    {
        StatPopupSet();
        ButtonSet();
    }

    // 버튼 오브젝트 활성/비활성 업데이트
    public void ButtonSet()
    {
        if (data.attackPowerLevel >= 10) ATKPowerUpBtn.gameObject.SetActive(false);
        else ATKPowerUpBtn.gameObject.SetActive(true);
        if (data.attackPowerLevel <= 0) ATKPowerDownBtn.gameObject.SetActive(false);
        else ATKPowerDownBtn.gameObject.SetActive(true);

        if (data.attackSpeedLevel >= 10) ATKSpeedUpBtn.gameObject.SetActive(false);
        else ATKSpeedUpBtn.gameObject.SetActive(true);
        if (data.attackSpeedLevel <= 0) ATKSpeedDownBtn.gameObject.SetActive(false);
        else ATKSpeedDownBtn.gameObject.SetActive(true);

        if (data.moveSpeedLevel >= 10) moveSpeedUpBtn.gameObject.SetActive(false);
        else moveSpeedUpBtn.gameObject.SetActive(true);
        if (data.moveSpeedLevel <= 0) moveSpeedDownBtn.gameObject.SetActive(false);
        else moveSpeedDownBtn.gameObject.SetActive(true);

        // 이후 다른 스탯 구현시 추가예정
    }

    // 모든 레벨/스탯 업데이트
    public void StatPopupSet()
    { 
        ATKPowerLevelTMP.text = data.attackPowerLevel.ToString();
        ATKSpeedLevelTMP.text = data.attackSpeedLevel.ToString();
        moveSpeedLevelTMP.text = data.moveSpeedLevel.ToString();
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[data.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[data.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[data.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
    }

    // 함수 안에서 버튼 오브젝트의 활성화/비활성화 결정 후 실행
    #region StatUp & Down
    public void ATKPowerLevelUP()
    {
        if (statUpPoint <= 0 || data.attackPowerLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (data.attackPowerLevel < 10)
            {
                data.attackPowerLevel++;
                statUpPoint--;

                ATKPowerLevelTMP.text = $"Lv.{data.attackPowerLevel.ToString()}";
                ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[data.attackPowerLevel].ToString()}";

                // 렙업 버튼 클릭 후, 만렙이면 버튼 비활성화 or 레벨이 1이면 레벨다운 버튼 활성화
                if (data.attackPowerLevel >= 10) ATKPowerUpBtn.gameObject.SetActive(false);
                else if (data.attackPowerLevel == 1 && !ATKPowerDownBtn.gameObject.activeSelf) ATKPowerDownBtn.gameObject.SetActive(true);
                Debug.Log("ATKPowerLvUP!");
            }
        }
    }

    public void ATKSpeedLevelUP()
    {
        if (statUpPoint <= 0 || data.attackSpeedLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (data.attackSpeedLevel < 10)
            {
                data.attackSpeedLevel++;
                statUpPoint--;

                ATKSpeedLevelTMP.text = $"Lv.{data.attackSpeedLevel.ToString()}";
                ATKSpeedTMP.text = $"{DataManager.Instance.attackPowerDic[data.attackSpeedLevel].ToString()}";
                Debug.Log("ATKSpeedLvUP!");
            }
        }
    }

    public void MoveSpeedLevelUP()
    {
        if (statUpPoint <= 0 || data.moveSpeedLevel >= 10)
        {
            //TODO: 포인트부족 popup 호출 or 최고렙 달성 popup 호출
            Debug.Log("statUpPoint부족 or 이미 최고렙 달성");
            return;
        }

        if (data.moveSpeedLevel < 10)
        {
            data.moveSpeedLevel++;
            statUpPoint--;

            moveSpeedLevelTMP.text = $"Lv.{data.moveSpeedLevel.ToString()}";
            moveSpeedTMP.text = $"{DataManager.Instance.attackPowerDic[data.moveSpeedLevel].ToString()}";
            Debug.Log("moveSpeedLvUP!");
        }
    }

    public void ATKPowerLevelDown()
    {
        if (data.attackPowerLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (data.attackPowerLevel > 0)
            {
                data.attackPowerLevel--;
                statUpPoint++;

                ATKPowerLevelTMP.text = $"Lv.{data.attackPowerLevel.ToString()}";
                ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[data.attackPowerLevel].ToString()}";

                // 렙다운 버튼 클릭 후, 0렙이면 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (data.attackPowerLevel <= 0) ATKPowerDownBtn.gameObject.SetActive(false);
                else if (data.attackPowerLevel == 9 && !ATKPowerUpBtn.gameObject.activeSelf) ATKPowerUpBtn.gameObject.SetActive(true);
                Debug.Log("ATKPowerLv Down!");
            }
        }
    }

    public void ATKSpeedLevelDown()
    {
        if (data.attackSpeedLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (data.attackSpeedLevel > 0)
            {
                data.attackSpeedLevel--;
                statUpPoint++;

                ATKSpeedLevelTMP.text = $"Lv.{data.attackSpeedLevel.ToString()}";
                ATKSpeedTMP.text = $"{DataManager.Instance.attackSpeedDic[data.attackSpeedLevel].ToString()}";

                // 렙다운 버튼 클릭 후, 0렙이면 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (data.attackSpeedLevel <= 0) ATKSpeedDownBtn.gameObject.SetActive(false);
                else if (data.attackSpeedLevel == 9 && !ATKSpeedDownBtn.gameObject.activeSelf) ATKSpeedDownBtn.gameObject.SetActive(true);
                Debug.Log("ATKSpeedLv Down!");
            }
        }
    }

    public void MoveSpeedLevelDown()
    {
        if (data.moveSpeedLevel <= 0)
        {
            //TODO: 최저레벨 popup 호출
            Debug.Log("이미 0 레벨!");
            return;
        }

        if (gameObject.activeSelf)
        {
            if (data.moveSpeedLevel > 0)
            {
                data.moveSpeedLevel--;
                statUpPoint++;

                moveSpeedLevelTMP.text = $"Lv.{data.moveSpeedLevel.ToString()}";
                moveSpeedTMP.text = $"{DataManager.Instance.moveSpeedDic[data.moveSpeedLevel].ToString()}";

                // 렙다운 버튼 클릭 후, 0렙이면 버튼 비활성화 or 레벨이 9이면 레벨업 버튼 활성화
                if (data.moveSpeedLevel <= 0) moveSpeedDownBtn.gameObject.SetActive(false);
                else if (data.moveSpeedLevel == 9 && !moveSpeedDownBtn.gameObject.activeSelf) moveSpeedDownBtn.gameObject.SetActive(true);
                Debug.Log("moveSpeedLv Down!");
            }
        }
    }
    #endregion

    public void OnSaveButton()
    {
        GameManager.Instance.playerData = data;
        gameObject.SetActive(false);
    }
}

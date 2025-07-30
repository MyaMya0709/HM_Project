using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_LevelUpPopup : MonoBehaviour
{
    public PlayerData data;
    public int curLevel;
    public int beforeLevel;
    public int afterLevel;
    public int statUpPoint;
    public int curGold;

    [SerializeField] private RectTransform levelUpArea;
    [SerializeField] private RectTransform maxLevel;
    [SerializeField] private TMP_Text beforeLevelTMP;
    [SerializeField] private TMP_Text afterLevelTMP;

    [SerializeField] private RectTransform spendGoldArea;
    [SerializeField] private TMP_Text levelUpCostTMP;
    [SerializeField] private Button playerLevelUpBtn;

    [SerializeField] private TMP_Text curGoldTMP;

    [SerializeField] private UI_InfoPanel infoPanel;

    private void OnEnable()
    {
        curLevel = data.playerLevel;
        statUpPoint = data.statUpPoint;
        curGold = data.haveGold;

        PopupSet();
    }

    public void OnLevelUp()
    {
        if (gameObject.activeSelf)
        {
            if (curGold >= DataManager.Instance.levelUpCostDic[curLevel] && curLevel <= 49)
            {
                curGold -= DataManager.Instance.levelUpCostDic[curLevel];
                curGoldTMP.text = $"{curGold}";
                curLevel++;
                statUpPoint++;
                
                //팝업창 세팅
                PopupSet();
                Debug.Log("Level UP!");
            }

            else if (curGold < DataManager.Instance.levelUpCostDic[curLevel])
            {
                // 알림창 - 골드부족
                Debug.Log("레벨업 골드 부족");
            }
        }
    }

    public void PopupSet()
    {
        // 최대 레벨 체크 후 정보창 세팅
        if (curLevel >= 50)
        {
            playerLevelUpBtn.gameObject.SetActive(false);
            spendGoldArea.gameObject.SetActive(false);
            levelUpArea.gameObject.SetActive(false);
            maxLevel.gameObject.SetActive(true);
            Debug.Log("최대 레벨!");
        }
        else
        {
            playerLevelUpBtn.gameObject.SetActive(true);
            spendGoldArea.gameObject.SetActive(true);
            levelUpArea.gameObject.SetActive(true);
            maxLevel.gameObject.SetActive(false);

            beforeLevelTMP.text = $"Lv.{curLevel}";
            afterLevelTMP.text = $"Lv.{curLevel + 1}";
            levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[curLevel].ToString();
        }
    }

    public void OnExit()
    {
        data.playerLevel = curLevel;
        data.statUpPoint = statUpPoint;
        data.haveGold = curGold;

        GameManager.Instance.GetPlayerData(data);
        infoPanel.InfoPanelSet();

        gameObject.SetActive(false);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_LevelUpPopup : MonoBehaviour
{
    public int beforeLevel;
    public int afterLevel;

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
        PopupSet();
    }

    public void OnLevelUp()
    {
        if (gameObject.activeSelf)
        {
            if (GameManager.Instance.curGold >= DataManager.Instance.levelUpCostDic[GameManager.Instance.playerLevel] && GameManager.Instance.playerLevel <= 49)
            {
                GameManager.Instance.playerLevel++;
                GameManager.Instance.statUpPoint++;
                
                //팝업창 세팅
                PopupSet();
                Debug.Log("Level UP!");

                GameManager.Instance.SpendGold(DataManager.Instance.levelUpCostDic[GameManager.Instance.playerLevel]);
                curGoldTMP.text = $"{GameManager.Instance.curGold}";
            }

            else if (GameManager.Instance.curGold < DataManager.Instance.levelUpCostDic[GameManager.Instance.playerLevel])
            {
                // 알림창 - 골드부족
                Debug.Log("레벨업 골드 부족");
            }
        }
    }

    public void PopupSet()
    {
        // 최대 레벨 체크 후 정보창 세팅
        if (GameManager.Instance.playerLevel >= 50)
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

            beforeLevelTMP.text = $"Lv.{GameManager.Instance.playerLevel}";
            afterLevelTMP.text = $"Lv.{GameManager.Instance.playerLevel + 1}";
            levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[GameManager.Instance.playerLevel].ToString();
        }
    }

    public void OnExit()
    {
        GameManager.Instance.GetPlayerData();

        infoPanel.InfoPanelSet();

        gameObject.SetActive(false);
    }
}

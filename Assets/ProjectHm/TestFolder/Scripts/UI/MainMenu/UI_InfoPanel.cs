using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoPanel : MonoBehaviour
{
    [Header("PlayerSet")]
    public PlayerData playerData;

    [SerializeField] private TMP_Text playerLevelTMP;
    [SerializeField] private TMP_Text ATKPowerLevelTMP;
    [SerializeField] private TMP_Text ATKSpeedLevelTMP;
    [SerializeField] private TMP_Text moveSpeedLevelTMP;
    [SerializeField] private TMP_Text jumpPowerLevelTMP;
    [SerializeField] private TMP_Text statLevelTMP;

    [SerializeField] private TMP_Text ATKPowerTMP;
    [SerializeField] private TMP_Text ATKSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;
    [SerializeField] private TMP_Text statTMP;

    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Button statSetBtn;

    [Header("CharacterSet")]
    public CharacterData characterData;

    [SerializeField] private Sprite characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;

    [SerializeField] private RectTransform levelUpPopup;
    [SerializeField] private RectTransform statUpPopup;

    private void Awake()
    {
        InfoPanelSet();
    }

    public void InfoPanelSet()
    {
        playerData = GameManager.Instance.playerData;
        if (playerData == null) Debug.Log("playerData로드 안됨");

        // DataManager의 characterDataList를 순회
        foreach (CharacterData CData in DataManager.Instance.characterDataList)
        {
            // playerData의 characterID와 같은 아이디의 CharacterData 찾아서 참조
            if (CData.ID == playerData.characterID) characterData = CData;
        }
        if (characterData == null) Debug.Log("characterData로드 안됨");

        playerLevelTMP.text = playerData.playerLevel.ToString();

        ATKPowerLevelTMP.text = playerData.attackPowerLevel.ToString();
        ATKSpeedLevelTMP.text = playerData.attackSpeedLevel.ToString();
        moveSpeedLevelTMP.text = playerData.moveSpeedLevel.ToString();
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        //스탯 값 세팅
        StatSet();

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;
    }

    // 스탯 표시 함수
    public void StatSet()
    {
        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[playerData.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();

        // 보너스 스탯 표기
        if (characterData.bonusStatValue != null && characterData.bonusStatType != null)
        {
            StatType statType;
            for (int i = 0; i < characterData.bonusStatType.Count - 1; i++)
            {
                statType = characterData.bonusStatType[i];
                switch (statType)
                {
                    case StatType.attackPower:
                        ATKPowerTMP.text += $"+({characterData.bonusStatValue[i].ToString()})";
                        Debug.Log("스탯 적용 : attackPower");
                        break;

                    case StatType.attackSpeed:
                        ATKSpeedTMP.text += $"+({characterData.bonusStatValue[i].ToString()})";
                        Debug.Log("스탯 적용 : attackSpeed");
                        break;

                    case StatType.moveSpeed:
                        moveSpeedTMP.text += $"+({characterData.bonusStatValue[i].ToString()})";
                        Debug.Log("스탯 적용 : moveSpeed");
                        break;
                    default:
                        Debug.Log("스탯적용 불가");
                        break;
                }
            }
        }
    }

    public void OnLevelUpPopup()
    {
        UI_LevelUpPopup popup = levelUpPopup.GetComponent<UI_LevelUpPopup>();
        if (!levelUpPopup.gameObject.activeSelf) levelUpPopup.gameObject.SetActive(true);
        popup.playerData = playerData;
    }

    public void OnStatUpPopup()
    {
        UI_StatUpPopup popup = statUpPopup.GetComponent<UI_StatUpPopup>();
        if (!statUpPopup.gameObject.activeSelf) statUpPopup.gameObject.SetActive(true);
        popup.data = playerData;
    }
}
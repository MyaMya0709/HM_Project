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

    [SerializeField] private Image characterImage;
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
        playerData = GameManager.Instance.SetPlayerData();
        if (playerData == null) Debug.Log("playerData·Îµå ¾ÈµÊ");

        characterData = GameManager.Instance.SetCharacterData();
        if (characterData == null) Debug.Log("characterData·Îµå ¾ÈµÊ");

        playerLevelTMP.text = playerData.playerLevel.ToString();

        ATKPowerLevelTMP.text = playerData.attackPowerLevel.ToString();
        ATKSpeedLevelTMP.text = playerData.attackSpeedLevel.ToString();
        moveSpeedLevelTMP.text = playerData.moveSpeedLevel.ToString();
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        //½ºÅÈ °ª ¼¼ÆÃ
        StatSet();

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;
    }

    // ½ºÅÈ Ç¥½Ã ÇÔ¼ö
    public void StatSet()
    {
        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[playerData.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();

        // º¸³Ê½º ½ºÅÈ Ç¥±â
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
                        Debug.Log("½ºÅÈ Àû¿ë : attackPower");
                        break;

                    case StatType.attackSpeed:
                        ATKSpeedTMP.text += $"+({characterData.bonusStatValue[i].ToString()})";
                        Debug.Log("½ºÅÈ Àû¿ë : attackSpeed");
                        break;

                    case StatType.moveSpeed:
                        moveSpeedTMP.text += $"+({characterData.bonusStatValue[i].ToString()})";
                        Debug.Log("½ºÅÈ Àû¿ë : moveSpeed");
                        break;
                    default:
                        Debug.Log("½ºÅÈÀû¿ë ºÒ°¡");
                        break;
                }
            }
        }
    }

    public void OnLevelUpPopup()
    {
        UI_LevelUpPopup popup = levelUpPopup.GetComponent<UI_LevelUpPopup>();
        if (!levelUpPopup.gameObject.activeSelf) levelUpPopup.gameObject.SetActive(true);
        popup.data = playerData;
    }

    public void OnStatUpPopup()
    {
        UI_StatUpPopup popup = statUpPopup.GetComponent<UI_StatUpPopup>();
        if (!statUpPopup.gameObject.activeSelf) statUpPopup.gameObject.SetActive(true);
        popup.data = playerData;
    }
}
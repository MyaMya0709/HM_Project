using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoPanel : MonoBehaviour
{
    [Header("PlayerSet")]
    public PlayerData data;

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

    [Header("Popup")]
    [SerializeField] private RectTransform levelUpPopup;
    [SerializeField] private RectTransform statUpPopup;

    private void OnEnable()
    {
        InfoPanelSet();
    }
    private void OnDisable()
    {
        GameManager.Instance.SavePlayerData();
    }

    public void InfoPanelSet()
    {
        data = GameManager.Instance.SetPlayerData();
        if (data == null) Debug.Log("playerData로드 안됨");

        characterData = GameManager.Instance.SetCharacterData();
        if (characterData == null) Debug.Log("characterData로드 안됨");

        //레벨 값 세팅
        playerLevelTMP.text = $"Lv.{data.playerLevel.ToString()}";
        ATKPowerLevelTMP.text = $"Lv.{data.attackPowerLevel.ToString()}";
        ATKSpeedLevelTMP.text = $"Lv.{data.attackSpeedLevel.ToString()}";
        moveSpeedLevelTMP.text = $"Lv.{data.moveSpeedLevel.ToString()}";
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        //스탯 값 세팅
        StatSet();

        //레벨 업 버튼 체크
        if (data.playerLevel >= 50) levelUpBtn.gameObject.SetActive(false);
        else levelUpBtn.gameObject.SetActive(true);

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;
    }

    // 스탯 표시 함수
    public void StatSet()
    {
        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[data.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[data.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[data.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();

        // 보너스 스탯 표기
        if (characterData.bonusStatValue != null && characterData.bonusStatType != null)
        {
            for (int i = 0; i < characterData.bonusStatType.Count; i++)
            {
                StatType statType = characterData.bonusStatType[i];
                switch (statType)
                {
                    case StatType.attackPower:
                        ATKPowerTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : attackPower");
                        break;

                    case StatType.attackSpeed:
                        ATKSpeedTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : attackSpeed");
                        break;

                    case StatType.moveSpeed:
                        moveSpeedTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : moveSpeed");
                        break;
                    default:
                        Debug.Log("스탯적용 불가");
                        break;
                }
            }
            Debug.Log("보너스 스탯 적용");
        }
    }

    public void OnLevelUpPopup()
    {
        UI_LevelUpPopup popup = levelUpPopup.GetComponent<UI_LevelUpPopup>();
        popup.data = data;
        if (!levelUpPopup.gameObject.activeSelf) levelUpPopup.gameObject.SetActive(true);
    }

    public void OnStatUpPopup()
    {
        UI_StatUpPopup popup = statUpPopup.GetComponent<UI_StatUpPopup>();
        popup.data = data;
        if (!statUpPopup.gameObject.activeSelf) statUpPopup.gameObject.SetActive(true);
    }

    public void OnChangeCharacter(Button btn)
    {

    }
}
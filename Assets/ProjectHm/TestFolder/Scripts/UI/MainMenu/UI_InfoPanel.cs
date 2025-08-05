using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_InfoPanel : MonoBehaviour
{
    [Header("PlayerSet")]
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
    [SerializeField] private CharacterData characterData;
    [SerializeField] private List<CharacterData> characterDataList;

    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;

    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;


    [Header("Popup")]
    [SerializeField] private RectTransform levelUpPopup;
    [SerializeField] private RectTransform statUpPopup;

    private void OnEnable()
    {
        InfoPanelSet();
    }
    private void OnDisable()
    {
        //info창이 비활성화 될때마다 Json 저장
        GameManager.Instance.SavePlayerData();
    }

    public void InfoPanelSet()
    {
        characterData = GameManager.Instance.SetCharacterData();
        if (characterData == null) Debug.Log("characterData로드 안됨");

        //레벨 값 세팅
        playerLevelTMP.text = $"Lv.{GameManager.Instance.playerLevel.ToString()}";
        ATKPowerLevelTMP.text = $"Lv.{GameManager.Instance.attackPowerLevel.ToString()}";
        ATKSpeedLevelTMP.text = $"Lv.{GameManager.Instance.attackSpeedLevel.ToString()}";
        moveSpeedLevelTMP.text = $"Lv.{GameManager.Instance.moveSpeedLevel.ToString()}";
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        //스탯 값 세팅
        StatSet();

        //레벨 업 버튼 체크
        if (GameManager.Instance.playerLevel >= 50) levelUpBtn.gameObject.SetActive(false);
        else levelUpBtn.gameObject.SetActive(true);

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;
    }

    // 스탯 표시 함수
    public void StatSet()
    {
        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[GameManager.Instance.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[GameManager.Instance.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[GameManager.Instance.moveSpeedLevel].ToString();
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
        if (!levelUpPopup.gameObject.activeSelf) levelUpPopup.gameObject.SetActive(true);
    }

    public void OnStatUpPopup()
    {
        UI_StatUpPopup popup = statUpPopup.GetComponent<UI_StatUpPopup>();
        if (!statUpPopup.gameObject.activeSelf) statUpPopup.gameObject.SetActive(true);
    }

    public void OnChangeCharacter(Button btn)
    {
        if (btn == leftBtn)
        {
            Debug.Log("좌측버튼 클릭");
            for (int i = 0; i < GameManager.Instance.openCharacterIDList.Count; i++)
            {
                if (GameManager.Instance.openCharacterIDList[i] == GameManager.Instance.characterID)
                {
                    if (i - 1 < 0)
                        GameManager.Instance.characterID = GameManager.Instance.openCharacterIDList[GameManager.Instance.openCharacterIDList.Count - 1];
                    else
                    {
                        GameManager.Instance.characterID = GameManager.Instance.openCharacterIDList[i - 1];
                    }

                    Debug.Log("이전 캐릭터 정보");
                    break;
                }
            }
            GameManager.Instance.GetCharacterData();
            GameManager.Instance.SavePlayerData();
            InfoPanelSet();
        }
        else if (btn == rightBtn)
        {
            Debug.Log("우측버튼 클릭");
            for (int i = 0; i < GameManager.Instance.openCharacterIDList.Count; i++)
            {
                if (GameManager.Instance.openCharacterIDList[i] == GameManager.Instance.characterID)
                {
                    if (i + 1 >= GameManager.Instance.openCharacterIDList.Count)
                        GameManager.Instance.characterID = GameManager.Instance.openCharacterIDList[0];
                    else
                    {
                        GameManager.Instance.characterID = GameManager.Instance.openCharacterIDList[i + 1];
                    }
                    
                    Debug.Log("다음 캐릭터 정보");
                    break;
                }
            }
            GameManager.Instance.GetCharacterData();
            GameManager.Instance.SavePlayerData();
            InfoPanelSet();
        }
    }
}
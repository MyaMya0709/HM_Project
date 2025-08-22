using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_InfoPanel : MonoBehaviour
{
    [Header("PlayerSet")]
    [SerializeField] private TMP_Text playerLevelTMP;
    [SerializeField] private TMP_Text ATKPowerLevelTMP;
    [SerializeField] private TMP_Text ATKSpeedLevelTMP;
    [SerializeField] private TMP_Text movePowerLevelTMP;
    [SerializeField] private TMP_Text actPowerLevelTMP;
    [SerializeField] private TMP_Text masteryLevelTMP;

    [SerializeField] private TMP_Text ATKPowerTMP;
    [SerializeField] private TMP_Text ATKSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;
    [SerializeField] private TMP_Text statTMP;

    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Button statSetBtn;

    [Header("CharacterSet")]
    [SerializeField] private int characterID;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;

    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button selecBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private GameObject selectedCheck;
    [SerializeField] private GameObject unlockCheck;

    [Header("Popup")]
    [SerializeField] private RectTransform levelUpPopup;
    [SerializeField] private RectTransform statUpPopup;
    [SerializeField] private RectTransform purchasePopup;

    public bool isPurchase = false;
    public bool isUnlock = false;

    private void OnEnable()
    {
        characterID = GameManager.Instance.characterID;
        characterData = GameManager.Instance.curCharacterData;
        if (characterData == null) Debug.Log("characterData로드 안됨");

        InfoPanelSet();
    }
    private void OnDisable()
    {
        //info창이 비활성화 될때마다 Json 저장
        GameManager.Instance.SavePlayerData();
    }

    public void InfoPanelSet()
    {
        UnlockCheck();
        PurchaseChack();

        //레벨 값 세팅
        playerLevelTMP.text = $"Lv.{GameManager.Instance.playerLevel.ToString()}";
        ATKPowerLevelTMP.text = $"Lv.{GameManager.Instance.attackPowerLevel.ToString()}";
        ATKSpeedLevelTMP.text = $"Lv.{GameManager.Instance.attackSpeedLevel.ToString()}";
        movePowerLevelTMP.text = $"Lv.{GameManager.Instance.moveSpeedLevel.ToString()}";
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        StatSet();                 //스탯 값 세팅
        ButtonSet();               //버튼 세팅
    }

    public void ButtonSet()
    {
        //레벨 업 버튼 체크
        if (GameManager.Instance.playerLevel >= 50) levelUpBtn.gameObject.SetActive(false);
        else levelUpBtn.gameObject.SetActive(true);

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;

        // 캐릭터 선택/구매 버튼 세팅
        if (isUnlock)
        {
            if (isPurchase)
            {
                if (characterID == GameManager.Instance.characterID)
                {
                    unlockCheck.SetActive(false);
                    selectedCheck.SetActive(true);
                    selecBtn.gameObject.SetActive(false);
                    buyBtn.gameObject.SetActive(false);
                }
                else
                {
                    unlockCheck.SetActive(false);
                    selectedCheck.SetActive(false);
                    selecBtn.gameObject.SetActive(true);
                    buyBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                unlockCheck.SetActive(false);
                selectedCheck.SetActive(false);
                selecBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(true);
            }

        }
        else
        {
            unlockCheck.SetActive(true);
            selectedCheck.SetActive(false);
            selecBtn.gameObject.SetActive(false);
            buyBtn.gameObject.SetActive(false);
        }
    }

    // 스탯 표시 함수
    public void StatSet()
    {
        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[GameManager.Instance.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[GameManager.Instance.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.movePowerDic[GameManager.Instance.moveSpeedLevel].ToString();
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
                    case StatType.AttackPower:
                        ATKPowerTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : attackPower");
                        break;

                    case StatType.AttackSpeed:
                        ATKSpeedTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : attackSpeed");
                        break;

                    case StatType.MoveSpeed:
                        moveSpeedTMP.text += $"+({characterData.bonusStatValue[i]})";
                        Debug.Log("스탯 적용 : moveSpeed");
                        break;
                    default:
                        Debug.Log("스탯적용 불가");
                        break;
                }
            }
            Debug.Log("보너스 스탯 표기 적용");
        }
    }

    public void UnlockCheck()
    {
        // 현재 보여지는 캐릭터가 해금 되었는지 확인
        for (int i = 0; i < GameManager.Instance.unlockCharacterList.Count; i++)
        {
            if (GameManager.Instance.unlockCharacterList[i] == characterID)
            {
                isUnlock = true;
                break;
            }
            else isUnlock = false;
        }
    }
    public void PurchaseChack()
    {
        // 현재 보여지는 캐릭터를 구입하였는지 확인
        for (int i = 0; i < GameManager.Instance.purchaseCharacterList.characterIDs.Count; i++)
        {
            if (GameManager.Instance.purchaseCharacterList.characterIDs[i] == characterID)
            {
                isPurchase = true;
                break;
            }
            else isPurchase= false;
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
        // 캐릭터 데이터 전체 순회
        for (int i = 0; i < DataManager.Instance.characterDataList.Count; i++)
        {
            // 데이터 리스트에서 현재 위치 확인
            if (DataManager.Instance.characterDataList[i].ID == characterID)
            {
                if (btn == leftBtn)
                {
                    // 이전 캐릭터의 ID, Data 세팅
                    Debug.Log("좌측버튼 클릭");

                    // 현 위치가 0일 때
                    if (i - 1 < 0)
                    {
                        characterData = DataManager.Instance.characterDataList[DataManager.Instance.characterDataList.Count - 1];
                        characterID = characterData.ID;
                    }
                    // 현 위치가 0이 아닐때
                    else
                    {
                        characterData = DataManager.Instance.characterDataList[i - 1];
                        characterID = characterData.ID;
                    }

                    Debug.Log("이전 캐릭터 정보");
                    break;
                }
                else if (btn == rightBtn)
                {
                    //다음 캐릭터의 ID, Data 세팅
                    Debug.Log("우측버튼 클릭");

                    // 현 위치가 리스트의 마지막일 때
                    if (i + 1 >= DataManager.Instance.characterDataList.Count)
                    {
                        characterData = DataManager.Instance.characterDataList[0];
                        characterID = characterData.ID;
                    }
                    // 현 위치가 리스트의 마지막이 아닐 때
                    else
                    {
                        characterData = DataManager.Instance.characterDataList[i + 1];
                        characterID = characterData.ID;
                    }

                    Debug.Log("다음 캐릭터 정보");
                    break;
                }
            } 
        }
        InfoPanelSet();
    }

    public void OnSelecCharacter()
    {
        GameManager.Instance.characterID = characterID;
        GameManager.Instance.GetCharacterData();
        GameManager.Instance.GetPlayerData();
        ButtonSet();
        Debug.Log("캐릭터 선택");
    }

    public void OnPurchasePopup()
    {
        purchasePopup.GetComponent<UI_PurchasePopup>().dataID = characterID;
        if (!purchasePopup.gameObject.activeSelf) purchasePopup.gameObject.SetActive(true);
    }
}
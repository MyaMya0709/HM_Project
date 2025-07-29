using TMPro;
using UnityEditor.Experimental.GraphView;
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

    //[SerializeField] private TMP_Text levelUpCostTMP;

    //[SerializeField] private Button playerLevelUpBtn;
    //[SerializeField] private Button ATKPowerUpBtn;
    //[SerializeField] private Button ATKSpeedUpBtn;
    //[SerializeField] private Button moveSpeedUpBtn;
    //[SerializeField] private Button jumpPowerUpBtn;
    //[SerializeField] private Button statupBtn;

    //[SerializeField] private TMP_Text curGoldTMP;



    public int statUpPoint;

    private void Awake()
    {
        playerData = GameManager.Instance.playerData;
        //characterData = DataManager.Instance.characterDataDic[playerData.characterID];
        if (playerData == null) Debug.Log("playerData로드 안됨");
        if (characterData == null) Debug.Log("characterData로드 안됨");

        // TODO: 스탯 추가시 확장
        statUpPoint = playerData.playerLevel
            - playerData.attackPowerLevel
            - playerData.attackSpeedLevel
            - playerData.moveSpeedLevel;

        InfoPanelSet();
    }

    public void InfoPanelSet()
    {
        playerLevelTMP.text = playerData.playerLevel.ToString();

        ATKPowerLevelTMP.text = playerData.attackPowerLevel.ToString();
        ATKSpeedLevelTMP.text = playerData.attackSpeedLevel.ToString();
        moveSpeedLevelTMP.text = playerData.moveSpeedLevel.ToString();
        //jumpPowerLevel.text = playerData.moveSpeedLevel.ToString();
        //statLevel.text = playerData.moveSpeedLevel.ToString();

        //levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[playerData.playerLevel].ToString();

        ATKPowerTMP.text = DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString();
        ATKSpeedTMP.text = DataManager.Instance.attackSpeedDic[playerData.attackSpeedLevel].ToString();
        moveSpeedTMP.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //jumpPower.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();
        //stat.text = DataManager.Instance.moveSpeedDic[playerData.moveSpeedLevel].ToString();

        //curGoldTMP.text = playerData.haveGold.ToString();

        characterName.text = characterData.Name;
        characterDescription.text = characterData.Description;

    }

    //public void ButtonSet()
    //{
    //    if (playerData.playerLevel >= 50) playerLevelUpBtn.gameObject.SetActive(false);
    //    else playerLevelUpBtn.gameObject.SetActive(true);

    //    if (playerData.attackPowerLevel >= 10) ATKPowerUpBtn.gameObject.SetActive(false);
    //    else ATKPowerUpBtn.gameObject.SetActive(true);

    //    if (playerData.attackSpeedLevel >= 10) ATKSpeedUpBtn.gameObject.SetActive(false);
    //    else ATKSpeedUpBtn.gameObject.SetActive(true);

    //    if (playerData.moveSpeedLevel >= 10) moveSpeedUpBtn.gameObject.SetActive(false);
    //    else moveSpeedUpBtn.gameObject.SetActive(true);

    //    // 이후 다른 스탯 구현시 추가예정
    //}

    //public void PlayerLevelUp()
    //{
    //    if (gameObject.activeSelf)
    //    {
    //        if (playerData.haveGold >= DataManager.Instance.levelUpCostDic[playerData.playerLevel] || playerData.playerLevel <= 49)
    //        {
    //            playerData.haveGold -= DataManager.Instance.levelUpCostDic[playerData.playerLevel];
    //            playerData.playerLevel++;
    //            statUpPoint++;

    //            //바뀐 정보값 수정
    //            playerLevelTMP.text = playerData.playerLevel.ToString();
    //            //levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[playerData.playerLevel].ToString();

    //            //레벨업 후 최대레벨 달성했으면 버튼 숨김
    //            //if (playerData.playerLevel >= 50) playerLevelUpBtn.gameObject.SetActive(false);
    //        }

    //        else if (playerData.haveGold < DataManager.Instance.levelUpCostDic[playerData.playerLevel])
    //        {
    //            // 알림창 - 골드부족
    //            Debug.Log("레벨업 골드 부족");
    //        }

    //        else if (playerData.playerLevel > 49)
    //        {
    //            //알림창 - 레벨 최대치
    //            Debug.Log("레벨 최대치");
    //        }

    //    }
    //}

    //public void ATKPowerLevelUP()
    //{
    //    if (statUpPoint <= 0)
    //    {
    //        //포인트부족 popup 호출
    //        Debug.Log("statUpPoint부족!");
    //        return;
    //    }

    //    if (gameObject.activeSelf)
    //    {
    //        if (playerData.attackPowerLevel < 10)
    //        {
    //            playerData.attackPowerLevel++;
    //            statUpPoint--;

    //            ATKPowerLevelTMP.text = $"Lv.{playerData.attackPowerLevel.ToString()}";
    //            //보너스 스탯이 있을때 조건으로 보너스 스탯 표시 분기점 만들기
    //            ATKPowerTMP.text = $"{DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString()}";
    //            //ATKPower.text = $"{DataManager.Instance.attackPowerDic[playerData.attackPowerLevel].ToString()}(+보너스 스탯)";
    //            //if (playerData.attackPowerLevel >= 10) { ATKPowerUpBtn.gameObject.SetActive(false); }
    //            Debug.Log("ATKPowerLvUP!");
    //        }
    //    }
    //}

    //public void ATKSpeedLevelUP()
    //{
    //    if (statUpPoint <= 0)
    //    {
    //        //포인트부족 popup 호출
    //        Debug.Log("statUpPoint부족!");
    //        return;
    //    }

    //    if (gameObject.activeSelf)
    //    {
    //        if (playerData.attackSpeedLevel < 10)
    //        {
    //            playerData.attackSpeedLevel++;
    //            statUpPoint--;

    //            ATKSpeedLevelTMP.text = $"Lv.{playerData.attackSpeedLevel.ToString()}";
    //            //보너스 스탯이 있을때 조건으로 보너스 스탯 표시 분기점 만들기
    //            ATKSpeedTMP.text = $"{DataManager.Instance.attackPowerDic[playerData.attackSpeedLevel].ToString()}";
    //            //ATKSpeed.text = $"{DataManager.Instance.attackPowerDic[playerData.attackSpeedLevel].ToString()}(+보너스 스탯)";
    //            //if (playerData.attackSpeedLevel >= 10) { ATKSpeedUpBtn.gameObject.SetActive(false); }
    //            Debug.Log("ATKSpeedLvUP!");
    //        }
    //    }
    //}

    //public void MoveSpeedLevelUP()
    //{
    //    if (statUpPoint <= 0)
    //    {
    //        //포인트부족 popup 호출
    //        Debug.Log("statUpPoint부족!");
    //        return;
    //    }

    //    if (playerData.moveSpeedLevel < 10)
    //    {
    //        playerData.moveSpeedLevel++;
    //        statUpPoint--;

    //        moveSpeedLevelTMP.text = $"Lv.{playerData.moveSpeedLevel.ToString()}";
    //        //보너스 스탯이 있을때 조건으로 보너스 스탯 표시 분기점 만들기
    //        moveSpeedTMP.text = $"{DataManager.Instance.attackPowerDic[playerData.moveSpeedLevel].ToString()}";
    //        //moveSpeed.text = $"{DataManager.Instance.attackPowerDic[playerData.moveSpeedLevel].ToString()}(+보너스 스탯)";

    //        //if (playerData.moveSpeedLevel >= 10) { moveSpeedUpBtn.gameObject.SetActive(false); }
    //        Debug.Log("moveSpeedLvUP!");
    //    }
    //}

    public void OnLevelUpPopup()
    {
        if(!levelUpPopup.gameObject.activeSelf) levelUpPopup.gameObject.SetActive(true);
    }

    public void OnStatUpPopup()
    {
        if (!statUpPopup.gameObject.activeSelf) statUpPopup.gameObject.SetActive(true);
    }
}
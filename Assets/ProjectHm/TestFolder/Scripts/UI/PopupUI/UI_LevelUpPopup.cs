using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_LevelUpPopup : MonoBehaviour
{
    public PlayerData data;
    public int statUpPoint;

    [SerializeField] private TMP_Text playerLevelTMP;
    [SerializeField] private TMP_Text levelUpCostTMP;
    [SerializeField] private Button playerLevelUpBtn;

    [SerializeField] private TMP_Text curGoldTMP;

    private void Awake()
    {
        if (data.playerLevel >= 50) playerLevelUpBtn.gameObject.SetActive(false);
        else playerLevelUpBtn.gameObject.SetActive(true);

        levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[data.playerLevel].ToString();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerLevelUp()
    {
        if (gameObject.activeSelf)
        {
            if (data.haveGold >= DataManager.Instance.levelUpCostDic[data.playerLevel] || data.playerLevel <= 49)
            {
                data.haveGold -= DataManager.Instance.levelUpCostDic[data.playerLevel];
                data.playerLevel++;
                statUpPoint++;

                //바뀐 정보값 수정
                playerLevelTMP.text = data.playerLevel.ToString();
                //levelUpCostTMP.text = DataManager.Instance.levelUpCostDic[playerData.playerLevel].ToString();

                //레벨업 후 최대레벨 달성했으면 버튼 숨김
                //if (playerData.playerLevel >= 50) playerLevelUpBtn.gameObject.SetActive(false);
            }

            else if (data.haveGold < DataManager.Instance.levelUpCostDic[data.playerLevel])
            {
                // 알림창 - 골드부족
                Debug.Log("레벨업 골드 부족");
            }

            else if (data.playerLevel > 49)
            {
                //알림창 - 레벨 최대치
                Debug.Log("레벨 최대치");
            }

        }
    }
}

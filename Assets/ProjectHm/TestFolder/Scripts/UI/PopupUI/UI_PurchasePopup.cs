using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PurchasePopup : MonoBehaviour
{
    [Header("Purchase")]
    [SerializeField] private TMP_Text spendGoldTMP;
    [SerializeField] private TMP_Text changeGoldTMP;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private UI_Enhance enhanceUI;
    [SerializeField] private UI_InfoPanel playerInfoUI;

    public int dataID;

    private void OnEnable()
    {
        TMPSet();
    }

    public void TMPSet()
    {
        if (dataID / 100 == 0)
        {
            spendGoldTMP.text = $"{DataManager.Instance.characterDataList[dataID].price}";
            changeGoldTMP.text = $"{GameManager.Instance.curGold - DataManager.Instance.characterDataList[dataID].price}";
            GameManager.Instance.SetGold();
        }
        if (dataID / 100 == 1)
        {
            spendGoldTMP.text = $"{DataManager.Instance.manualDataList[dataID - 100].price}";
            changeGoldTMP.text = $"{GameManager.Instance.curGold - DataManager.Instance.manualDataList[dataID - 100].price}";
            GameManager.Instance.SetGold();
        }
    }

    public void Onbuy()
    {
        if (dataID / 100 == 0)
        {
            GameManager.Instance.BuyCharacter(dataID);
            GameManager.Instance.SetGold();
            playerInfoUI.InfoPanelSet();
        }
        if (dataID / 100 == 1)
        {
            GameManager.Instance.BuyWeapon(dataID);
            GameManager.Instance.SetGold();
            enhanceUI.RightInfoSet();
        }
        OnExit();
    }

    public void OnExit()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }
}

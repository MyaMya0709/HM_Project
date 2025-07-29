using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Continue : MonoBehaviour
{
    [SerializeField] private Button stageButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private RectTransform continuePanel;
    [SerializeField] private RectTransform stagePanel;
    [SerializeField] private RectTransform infoPanel;
    [SerializeField] private RectTransform storePanel;
    [SerializeField] private RectTransform enhancePanel;
    [SerializeField] private TMP_Text curGold;
    

    protected void Awake()
    {
        continuePanel = GetComponent<RectTransform>();
        curGold.text = GameManager.Instance.playerData.haveGold.ToString();

        stageButton.onClick.AddListener(() =>
        {
            if (continuePanel.gameObject.activeSelf)
            {
                Clear();
                stagePanel.gameObject.SetActive(true);
            }
        });

        infoButton.onClick.AddListener(() =>
        {
            if (continuePanel.gameObject.activeSelf)
            {
                Clear();
                infoPanel.gameObject.SetActive(true);
            }
        });

        storeButton.onClick.AddListener(() =>
        {
            if (continuePanel.gameObject.activeSelf)
            {
                Clear();
                storePanel.gameObject.SetActive(true);
            }
        });

        enhanceButton.onClick.AddListener(() =>
        {
            if (continuePanel.gameObject.activeSelf)
            {
                Clear();
                enhancePanel.gameObject.SetActive(true);
            }
        });

        exitButton.onClick.AddListener(() =>
         {
             if (continuePanel.gameObject.activeSelf)
             {
                 Clear();
                 stagePanel.gameObject.SetActive(true);
                 continuePanel.gameObject.SetActive(false);
             }
         });
    }

    public void Clear()
    {
        stagePanel.gameObject.SetActive(false);
        infoPanel.gameObject.SetActive(false);
        storePanel.gameObject.SetActive(false);
        enhancePanel.gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        continuePanel.gameObject.SetActive(true);
        Clear();
        stagePanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(stageButton.gameObject);
    }
}

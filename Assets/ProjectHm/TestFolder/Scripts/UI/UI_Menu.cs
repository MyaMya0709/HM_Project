using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI
{
    [SerializeField] private Button settingBotton;
    [SerializeField] private Button continueBotton;
    [SerializeField] private Button exitBotton;
    [SerializeField] private RectTransform menuPanel;

    protected override void Awake()
    {
        base.Awake();

        menuPanel = GetComponent<RectTransform>();

        settingBotton.onClick.AddListener(() =>
        {
            if (menuPanel.gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                menuPanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                menuPanel.gameObject.SetActive(true);
            }
        });

        continueBotton.onClick.AddListener(() =>
        {
            if (menuPanel.gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                menuPanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                menuPanel.gameObject.SetActive(true);
            }
        });

        exitBotton.onClick.AddListener(() =>
        {
            if (menuPanel.gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                menuPanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                menuPanel.gameObject.SetActive(true);
            }
        });
    }
}

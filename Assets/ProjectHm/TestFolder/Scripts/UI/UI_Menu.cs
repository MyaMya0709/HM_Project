using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private RectTransform menuPanel;

    protected override void Awake()
    {
        base.Awake();

        menuPanel = GetComponent<RectTransform>();

        settingButton.onClick.AddListener(() =>
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

        continueButton.onClick.AddListener(() =>
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

        exitButton.onClick.AddListener(() =>
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

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : UI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button mainExitButton;
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform settingPanel;
    [SerializeField] private RectTransform continuePanel;

    protected override void Awake()
    {
        base.Awake();

        startButton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                //°ÔÀÓ ¾À ·Îµå & Æ©Åä¸®¾ó UI On
                SceneLoader.Instance.LoadSceneAsync("InGame");
            }
        });

        continueButton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                // CiontinueUI Open
                continuePanel.GetComponent<UI_Continue>().OpenUI();
            }
        });

        settingButton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                // SettingUI Open
                settingPanel.gameObject.SetActive(true);
            }
        });

        mainExitButton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                QuitGame();
            }
        });
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

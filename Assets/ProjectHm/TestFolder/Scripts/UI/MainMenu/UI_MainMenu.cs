using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : UI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button mainExitButton;
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform startMenuPanel;

    protected override void Awake()
    {
        base.Awake();

        startButton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                // CiontinueUI Open
                startMenuPanel.GetComponent<UI_Continue>().OpenUI();
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

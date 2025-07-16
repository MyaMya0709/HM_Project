using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : UI
{
    [SerializeField] private Button exitButton;
    [SerializeField] private RectTransform settingPanel;
    [SerializeField] private RectTransform mainPanel;

    protected override void Awake()
    {
        base.Awake();

        settingPanel = GetComponent<RectTransform>();

        exitButton.onClick.AddListener(() =>
        {
            if (settingPanel.gameObject.activeSelf)
            {
                settingPanel.gameObject.SetActive(false);
                mainPanel.gameObject.SetActive(true);
            }
        });
    }
}

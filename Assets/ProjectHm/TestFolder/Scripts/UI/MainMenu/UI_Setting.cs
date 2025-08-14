using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : UI
{
    [SerializeField] private Button exitButton;
    [SerializeField] private RectTransform settingPanel;

    protected override void Awake()
    {
        base.Awake();

        settingPanel = GetComponent<RectTransform>();

        exitButton.onClick.AddListener(() =>
        {
            if (settingPanel.gameObject.activeSelf)
            {
                settingPanel.gameObject.SetActive(false);
            }
        });
    }

    public void OnGameReset()
    {

    }
}

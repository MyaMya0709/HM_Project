using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : UI
{
    [SerializeField] private Button startBotton;
    [SerializeField] private Button continueBotton;
    [SerializeField] private Button settingBotton;
    [SerializeField] private Button exitBotton;
    [SerializeField] private RectTransform mainPanel;

    protected override void Awake()
    {
        base.Awake();

        mainPanel = GetComponent<RectTransform>();

        startBotton.onClick.AddListener(() =>
        {
            if (mainPanel.gameObject.activeSelf)
            {
                mainPanel.gameObject.SetActive(false);
                //∞‘¿” æ¿ ∑ŒµÂ & ∆©≈‰∏ÆæÛ UI On
                SceneLoader.Instance.LoadSceneAsync("InGame");
                GameManager.Instance.GameStart();
            }
        });
    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour 
{
    public UI_Menu menuUI;
    public UI_Selection selecUI;
    public Button menuButton;
    public Image expBar;

    // 활성화/비활성화 시 함수 추가/제거
    // [수정 필요] 상단 남은 적 연동하기
    private void OnEnable() => PlayerCondition.OnPlayerLevelUp += EnableSelecUI;
    private void OnDisable() => PlayerCondition.OnPlayerLevelUp -= EnableSelecUI;

    private void Awake()
    {
        // gameUI.menuButton set
        menuButton.onClick.AddListener(() =>
        {
            if (!menuUI.gameObject.activeSelf)
            {
                Time.timeScale = 0f;
                menuUI.gameObject.SetActive(true);
            }
        });
    }

    public void OpenMenuUI()
    {
        Time.timeScale = 0f;
        menuUI.gameObject.SetActive(true);
    }

    public void EnableSelecUI()
    {
        Time.timeScale = 0f;
        selecUI.gameObject.SetActive(true);
    }
}

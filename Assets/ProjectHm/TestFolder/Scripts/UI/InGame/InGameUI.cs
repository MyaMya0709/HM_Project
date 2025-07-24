using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour 
{
    public UI_Menu menuUI;
    public UI_Selection selecUI;
    public Button menuButton;
    public Button testSelecButton;
    public Image expBar;

    // Ȱ��ȭ/��Ȱ��ȭ �� �Լ� �߰�/����

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

        // gameUI.testSelecButton set
        testSelecButton.onClick.AddListener(() =>
        {
            if (!selecUI.gameObject.activeSelf)
            {
                Time.timeScale = 0f;
                selecUI.gameObject.SetActive(true);
            }
        });
    }

}

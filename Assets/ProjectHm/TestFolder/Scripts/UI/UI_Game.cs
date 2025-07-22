using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button testSelecButton;
    [SerializeField] private RectTransform menuUI;
    [SerializeField] private RectTransform selecUI;


    protected override void Awake()
    {
        base.Awake();

        menuButton.onClick.AddListener(() =>
        {
            if (!menuUI.gameObject.activeSelf)
            {
                Time.timeScale = 0f;
                menuUI.gameObject.SetActive(true);
            }
        });

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

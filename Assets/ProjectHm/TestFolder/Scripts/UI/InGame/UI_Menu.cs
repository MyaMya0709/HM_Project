using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        settingButton.onClick.AddListener(() =>
        {
            if (gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                gameObject.SetActive(true);
            }
        });

        continueButton.onClick.AddListener(() =>
        {
            if (gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                gameObject.SetActive(true);
            }
        });

        exitButton.onClick.AddListener(() =>
        {
            if (gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                gameObject.SetActive(true);
            }
        });
    }
}

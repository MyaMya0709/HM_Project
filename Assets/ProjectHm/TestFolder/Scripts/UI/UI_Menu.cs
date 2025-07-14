using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI
{
    [SerializeField] private Button meunBotton;
    [SerializeField] private RectTransform menuPanel;

    protected override void Awake()
    {
        base.Awake();

        meunBotton.onClick.AddListener(() =>
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

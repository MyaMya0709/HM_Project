using UnityEngine;
using UnityEngine.UI;

public class UI_Enhance : MonoBehaviour
{
    public Button enhanceBtn;


    private void Awake()
    {
        enhanceBtn.onClick.AddListener(() =>
        {
            //if (continuePanel.gameObject.activeSelf)
            //{
            //    Clear();
            //    stagePanel.gameObject.SetActive(true);
            //}
        });
    }


}

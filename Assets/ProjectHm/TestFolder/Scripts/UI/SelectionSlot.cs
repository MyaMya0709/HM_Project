using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    [SerializeField] private Button selecButton;
    [SerializeField] private GameObject selectionUI;


    protected void Awake()
    {
        selectionUI = transform.parent.parent.parent.gameObject;

        selecButton.onClick.AddListener(() =>
        {
            if (selectionUI.activeSelf)
            {
                Time.timeScale = 0f;
                selectionUI.SetActive(false);
            }
        });
    }


}

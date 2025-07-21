using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    [SerializeField] private Button selecButton;
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private PlayerCondition player;

    [SerializeField] private string slotName;
    [SerializeField] private Image slotIcon;
    [SerializeField] private string slotDescription;


    protected void Awake()
    {
        selectionUI = transform.parent.parent.parent.gameObject;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCondition>();



        selecButton.onClick.AddListener(() =>
        {
            //플레이어에게 정보 전달
            if (selectionUI.activeSelf)
            {
                Time.timeScale = 1f;
                selectionUI.SetActive(false);
            }
        });
    }
}

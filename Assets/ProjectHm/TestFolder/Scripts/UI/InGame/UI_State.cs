using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_State : MonoBehaviour
{
    [SerializeField] private GameObject statePanel;
    [SerializeField] private Sprite dashIcon;
    [SerializeField] private Sprite downAttackIcon;
    [SerializeField] private Sprite superJumpIcon;

    public void InitStateUI(int id, float cooltime)
    {
        GameObject state = Instantiate(statePanel, transform);
        Transform child0 = state.transform.GetChild(0);
        Transform child1 = state.transform.GetChild(1);
        Debug.Log("첫 번째 자식: " + child0.name);

        // 쿨타임 종류에 따라 아이콘 세팅
        switch (id)
        {
            case 0:
                child0.GetComponent<Image>().sprite = downAttackIcon;
                break;

            case 1:
                child0.GetComponent<Image>().sprite = dashIcon;
                break;

            case 2:
                child0.GetComponent<Image>().sprite = superJumpIcon;
                break;
        }

        // 타이머 시작
        StartCoroutine(DestroyCooltime(child1.GetComponent<Image>(),cooltime));
    }

    private IEnumerator DestroyCooltime(Image timer, float coolTime)
    {
        float elapsed = 0f;
        timer.fillAmount = 1f; // 처음엔 꽉 참

        while (elapsed < coolTime)
        {
            elapsed += Time.deltaTime;
            timer.fillAmount = 1f - (elapsed / coolTime);
            yield return null;
        }

        timer.fillAmount = 0f; // 끝나면 0

        Destroy(timer.transform.parent.gameObject);
        
    }
}

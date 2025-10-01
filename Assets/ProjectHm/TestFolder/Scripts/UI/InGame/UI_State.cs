using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_State : MonoBehaviour
{
    [SerializeField] private GameObject statePanel;
    [SerializeField] private Dictionary<int, GameObject> iconDic = new Dictionary<int, GameObject>(); 
    [SerializeField] private Sprite dashIcon;
    [SerializeField] private Sprite downAttackIcon;
    [SerializeField] private Sprite superJumpIcon;
    [SerializeField] private int[] order = { 0,1,2,3,4,5,6,7,8,9,10 };

    public void InitStateUI(int id, float cooltime)
    {
        GameObject state = Instantiate(statePanel, transform);

        iconDic[id] = state;

        Transform child0 = state.transform.GetChild(0);
        Transform child1 = state.transform.GetChild(1);
        Debug.Log("첫 번째 자식: " + child0.name);

        // 쿨타임 종류에 따라 아이콘 세팅
        switch (id)
        {
            case 0:
                child0.GetComponent<Image>().sprite = dashIcon;
                break;

            case 1:
                child0.GetComponent<Image>().sprite = downAttackIcon;
                break;

            case 2:
                child0.GetComponent<Image>().sprite = superJumpIcon;
                break;
        }

        SortIcon(id);

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

    public void SortIcon(int id)
    {
        foreach (int i in order)
        {
            try
            {
                iconDic[i].transform.SetSiblingIndex(i);
            }

            catch
            {
                Debug.Log($"iconDic[{i}]없음");
                continue;
            }
        }
    }
}

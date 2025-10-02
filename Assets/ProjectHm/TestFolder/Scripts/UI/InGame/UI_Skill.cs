using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    public Image skillIcon;
    public GameObject noneIcon;
    public Image timer;

    public IEnumerator SkillTimer(float cooltime)
    {
        float elapsed = 0f;
        timer.fillAmount = 1f; // 처음엔 꽉 참

        while (elapsed < cooltime)
        {
            elapsed += Time.deltaTime;
            timer.fillAmount = 1f - (elapsed / cooltime);
            yield return null;
        }

        timer.fillAmount = 0f; // 끝나면 0
    }

    public void SkillFinish()
    {
        noneIcon.gameObject.SetActive(true);
    }
}

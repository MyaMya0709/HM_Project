using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ISkill : MonoBehaviour
{
    public PlayerCondition player;
    public SkillData skillData;

    private void Awake()
    {
        player = transform.parent.parent.GetComponent<PlayerCondition>();
        //player = FindFirstObjectByType<PlayerCondition>();
    }
    public abstract void UseSkill();
    public abstract void UseChargeSkill();
    public abstract void SkillRangeCheck();


    //public void SkillInit(GameObject bag)
    //{
    //    GetSkillData(skillID);
    //    Instantiate(skillData.gameObject, bag.transform);
    //}

    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}

using System.Security.Cryptography;
using UnityEngine;

public abstract class ISkill : MonoBehaviour
{
    public PlayerCondition player;
    public SkillData skillData;

    private void Awake()
    {
        player = transform.parent.parent.GetComponent<PlayerCondition>();
    }
    public abstract void UseSkill();
    public abstract void UseChargeSkill();

    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}

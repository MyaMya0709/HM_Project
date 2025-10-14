using System.Collections;
using UnityEngine;

public class Skill_02 : ISkill
{

    public override void SkillRangeCheck()
    {
        throw new System.NotImplementedException();
    }

    public override void UseChargeSkill()
    {
        UseSkill();
    }

    public override void UseSkill()
    {
        Debug.Log("버프 스킬 사용");
    }
}
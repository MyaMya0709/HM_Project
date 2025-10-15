using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill")]
public class SkillData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public int price;
    public Sprite sprite;

    // 데미지 효과
    public EffectTypeData effect;

    public List<int> buffID;

}

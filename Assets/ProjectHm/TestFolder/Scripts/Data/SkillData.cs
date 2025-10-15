using System.Collections.Generic;
using UnityEngine;

// [수정 필요] 
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

    // 버프 ID 리스트
    public List<int> buffID;

}

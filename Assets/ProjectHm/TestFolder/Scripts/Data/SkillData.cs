using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill")]
public class SkillData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public int price;

    // damageMultiple, knockback , airborne , stun , slow , dotDamage;
    public EffectTypeData effect;

    public Sprite sprite;
}

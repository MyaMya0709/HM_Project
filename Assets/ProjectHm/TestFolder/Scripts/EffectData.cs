using UnityEngine;

//[System.Serializable]
//public class EffectData
//{
//    public EffectType effectType;
//    public EffectTarget effectTarget;
//    public float effectAmount;
//    public float effectDuration;
//}

[System.Serializable]
public class BaseEffectData
{
    public int damage;
    public EffectData Knockback;
    public EffectData Airborne;
    public EffectData Stun;
    public EffectData Slow;
    public EffectData DotDamage;
}

[System.Serializable]
public class EffectData
{
    public bool onoff;
    public float valueA;
    public float valueB;
    public float valueC;
}

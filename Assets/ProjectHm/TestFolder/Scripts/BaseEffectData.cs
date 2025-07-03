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
public class WeaponEffectData
{
    public BaseEffectData Knockback;
    public BaseEffectData Airborne;
    public BaseEffectData Stun;
    public BaseEffectData Slow;
    public BaseEffectData DotDamage;
}

[System.Serializable]
public class BaseEffectData
{
    public bool onoff;
    public float valueA;
    public float valueB;
    public float valueC;
}

using System;

[Serializable]
public class WeaponEffectData
{
    public BaseEffectData Knockback;
    public BaseEffectData Airborne;
    public BaseEffectData Stun;
    public BaseEffectData Slow;
    public BaseEffectData DotDamage;
}

[Serializable]
public class BaseEffectData
{
    public bool onoff;
    public float valueA;
    public float valueB;
    public float valueC;
}

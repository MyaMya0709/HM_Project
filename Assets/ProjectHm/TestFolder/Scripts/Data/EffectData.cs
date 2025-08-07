using System;

[Serializable]
public class WeaponEffectData
{
    public EffectTypeData attackEffect;
    public EffectTypeData downEffect;
    public EffectTypeData chargeEffect;
    public EffectTypeData dashEffect;
}

[Serializable]
public class EffectTypeData
{
    public BaseEffectData knockback;
    public BaseEffectData airborne;
    public BaseEffectData stun;
    public BaseEffectData slow;
    public BaseEffectData dotDamage;
}

[Serializable]
public class BaseEffectData
{
    public bool isSum;   //참이면 합연산/거짓이면 곱연산
    public bool onoff;
    public float valueA;
    public float valueB;
    public float valueC;
}

using System;

[Serializable]
public class WeaponEffectData
{
    // 각 공격에 적용되는 효과
    public EffectTypeData attackEffect;
    public EffectTypeData dropEffect;
    public EffectTypeData chargeEffect;
    public EffectTypeData dashEffect;
}

[Serializable]
public class EffectTypeData
{
    // 적용 가능한 효과의 종류 및 데미지 배율
    public float damageMultiple;
    public BaseEffectData knockback;
    public BaseEffectData airborne;
    public BaseEffectData stun;
    public BaseEffectData slow;
    public BaseEffectData dotDamage;
}

[Serializable]
public class BaseEffectData
{
    public bool isSum;      // 참이면 합연산/거짓이면 곱연산
    public bool onoff;      // 효과의 적용 미적용 결정
    public float valueA;    // 아래 벨류의 사용처는 적의 효과 적용 로직에 있음 
    public float valueB;
    public float valueC;
}

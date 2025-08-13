using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enhance : MonoBehaviour
{
    public WeaponData data;

    [SerializeField] private TMP_Text name_TMP;
    [SerializeField] private TMP_Text level_TMP;
    [SerializeField] private TMP_Text discrip_TMP;
    [SerializeField] private Image image;

    [Header("Status")]
    [SerializeField] private RectTransform statusTap;
    [SerializeField] private TMP_Text attackDamage_TMP;
    [SerializeField] private TMP_Text attackSpeed_TMP;
    [SerializeField] private TMP_Text attackRange_TMP;
    [SerializeField] private TMP_Text moveSpeed_TMP;
    [SerializeField] private TMP_Text movePower_TMP;

    [Header("AttackTap")]
    [SerializeField] private RectTransform attackTap;
    [SerializeField] private TMP_Text AT_AttackDamage_TMP;
    [SerializeField] private TMP_Text AT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text AT_AirbornePower_TMP;
    [SerializeField] private TMP_Text AT_StunDuration_TMP;
    [SerializeField] private TMP_Text AT_SlowDuration_TMP;
    [SerializeField] private TMP_Text AT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text AT_DotDamage_TMP;
    [SerializeField] private TMP_Text AT_DotDuration_TMP;

    [Header("DropAttackTap")]
    [SerializeField] private RectTransform dropAttackTap;
    [SerializeField] private TMP_Text DropAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text DropAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text DropAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text DropAT_StunDuration_TMP;
    [SerializeField] private TMP_Text DropAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text DropAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text DropAT_DotDamage_TMP;
    [SerializeField] private TMP_Text DropAT_DotDuration_TMP;

    [Header("ChargeAttackTap")]
    [SerializeField] private RectTransform chargeAttackTap;
    [SerializeField] private TMP_Text ChargeAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text ChargeAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text ChargeAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text ChargeAT_StunDuration_TMP;
    [SerializeField] private TMP_Text ChargeAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text ChargeAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text ChargeAT_DotDamage_TMP;
    [SerializeField] private TMP_Text ChargeAT_DotDuration_TMP;

    [Header("DashAttackTap")]
    [SerializeField] private RectTransform dashAttackTap;
    [SerializeField] private TMP_Text DashAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text DashAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text DashAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text DashAT_StunDuration_TMP;
    [SerializeField] private TMP_Text DashAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text DashAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text DashAT_DotDamage_TMP;
    [SerializeField] private TMP_Text DashAT_DotDuration_TMP;

    [Header("Taps")]
    [SerializeField] private Button statusTapBtn;
    [SerializeField] private Button attackTapBtn;
    [SerializeField] private Button dropAttackTapBtn;
    [SerializeField] private Button chargeAttackTapBtn;
    [SerializeField] private Button dashAttackTapBtn;

    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;

    [SerializeField] private RectTransform enhanceUpPopup;

    [SerializeField] private TMP_Text curGoldTMP;

    private void OnEnable()
    {
        OnStatusTap();
        RightInfoSet();
    }

    public void RightInfoSet()
    {
        //name_TMP.text = 
        level_TMP.text = $"Lv.{GameManager.Instance.weaponData.baseWeaponLevel}";
        //discrip_TMP.text = 
        //image.sprite = 
    }

    public void OnEnhanceUpPopup()
    {
        if(!enhanceUpPopup.gameObject.activeSelf)
        enhanceUpPopup.gameObject.SetActive(true);
    }

    #region InfoSet
    public void StatusInfoSet()
    {
        attackDamage_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        //attackSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        attackRange_TMP.text = $"{GameManager.Instance.weaponData.data.baseRangeList[GameManager.Instance.curMWData.baseLevel]}";
        moveSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseMoveList[GameManager.Instance.curMWData.baseLevel]}";
        //movePower_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
    }
    public void AttackInfoSet()
    {
        UISet(
            AT_AttackDamage_TMP,
            AT_KnockbackPower_TMP,
            AT_AirbornePower_TMP,
            AT_StunDuration_TMP,
            AT_SlowDuration_TMP,
            AT_SlowDecrease_TMP,
            AT_DotDamage_TMP,
            AT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect);
    }
    public void DropAttackInfoSet()
    {
        UISet(
            DropAT_AttackDamage_TMP,
            DropAT_KnockbackPower_TMP,
            DropAT_AirbornePower_TMP,
            DropAT_StunDuration_TMP,
            DropAT_SlowDuration_TMP,
            DropAT_SlowDecrease_TMP,
            DropAT_DotDamage_TMP,
            DropAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect);
    }
    public void ChargeAttackInfoSet()
    {
        UISet(
            ChargeAT_AttackDamage_TMP,
            ChargeAT_KnockbackPower_TMP,
            ChargeAT_AirbornePower_TMP,
            ChargeAT_StunDuration_TMP,
            ChargeAT_SlowDuration_TMP,
            ChargeAT_SlowDecrease_TMP,
            ChargeAT_DotDamage_TMP,
            ChargeAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect);
    }
    public void DashAttackInfoSet()
    {
        UISet(
            DashAT_AttackDamage_TMP,
            DashAT_KnockbackPower_TMP,
            DashAT_AirbornePower_TMP,
            DashAT_StunDuration_TMP,
            DashAT_SlowDuration_TMP,
            DashAT_SlowDecrease_TMP,
            DashAT_DotDamage_TMP,
            DashAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect);
    }
    public void UISet(TMP_Text dmgText, TMP_Text knockbackText, TMP_Text airborneText, TMP_Text stunText, TMP_Text slowDurText, TMP_Text slowDecText, TMP_Text dotDmgText, TMP_Text dotDurText, EffectTypeData data)
    {
        dmgText.text = data.damageMultiple.ToString();
        knockbackText.text = data.knockback.valueA.ToString();
        airborneText.text = data.airborne.valueA.ToString();
        stunText.text = $"{data.stun.valueB}s";
        slowDurText.text = $"{data.slow.valueA}s";
        slowDecText.text = $"-{data.slow.valueB}%";
        dotDmgText.text = data.dotDamage.valueA.ToString();
        dotDurText.text = $"{data.dotDamage.valueB}s";
    }
    #endregion

    #region Taps
    public void OnStatusTap()
    {
        statusTap.gameObject.SetActive(true);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive (false);
        dashAttackTap.gameObject .SetActive (false);
        StatusInfoSet();
    }
    public void OnAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(true);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
        AttackInfoSet();
    }
    public void OnDropAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(true);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
        DropAttackInfoSet();
    }
    public void OnChargeAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(true);
        dashAttackTap.gameObject.SetActive(false);
        ChargeAttackInfoSet();
    }
    public void OnDashAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(true);
        DashAttackInfoSet();
    }
    #endregion
}
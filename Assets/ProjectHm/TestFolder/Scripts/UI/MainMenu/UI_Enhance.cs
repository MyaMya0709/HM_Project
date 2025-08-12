using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enhance : MonoBehaviour
{
    public WeaponData data;

    [SerializeField] private TMP_Text name_TMP;
    [SerializeField] private TMP_Text level_TMP;
    [SerializeField] private TMP_Text discrip_TMP;

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
        UISet();
    }

    public void UISet()
    {
        attackDamage_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        //attackSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        attackRange_TMP.text = $"{GameManager.Instance.weaponData.data.baseRangeList[GameManager.Instance.curMWData.baseLevel]}";
        moveSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseMoveList[GameManager.Instance.curMWData.baseLevel]}";
        //movePower_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";

        AT_AttackDamage_TMP.text = (GameManager.Instance.weaponData.totalDamage * GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.damageMultiple).ToString();
        AT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.knockback.valueA.ToString();
        AT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.airborne.valueA.ToString();
        AT_StunDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.stun.valueB.ToString();
        AT_SlowDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.slow.valueA.ToString();
        AT_SlowDecrease_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.slow.valueB.ToString();
        AT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.dotDamage.valueA.ToString();
        AT_DotDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.dotDamage.valueB.ToString();

        DropAT_AttackDamage_TMP.text = (GameManager.Instance.weaponData.totalDamage * GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.damageMultiple).ToString();
        DropAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.knockback.valueA.ToString();
        DropAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.airborne.valueA.ToString();
        DropAT_StunDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.stun.valueB.ToString();
        DropAT_SlowDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.slow.valueA.ToString();
        DropAT_SlowDecrease_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.slow.valueB.ToString();
        DropAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.dotDamage.valueA.ToString();
        DropAT_DotDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.dotDamage.valueB.ToString();

        ChargeAT_AttackDamage_TMP.text = (GameManager.Instance.weaponData.totalDamage * GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.damageMultiple).ToString();
        ChargeAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.knockback.valueA.ToString();
        ChargeAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.airborne.valueA.ToString();
        ChargeAT_StunDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.stun.valueB.ToString();
        ChargeAT_SlowDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.slow.valueA.ToString();
        ChargeAT_SlowDecrease_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.slow.valueB.ToString();
        ChargeAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.dotDamage.valueA.ToString();
        ChargeAT_DotDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.dotDamage.valueB.ToString();

        DashAT_AttackDamage_TMP.text = (GameManager.Instance.weaponData.totalDamage * GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.damageMultiple).ToString();
        DashAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.knockback.valueA.ToString();
        DashAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.airborne.valueA.ToString();
        DashAT_StunDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.stun.valueB.ToString();
        DashAT_SlowDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.slow.valueA.ToString();
        DashAT_SlowDecrease_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.slow.valueB.ToString();
        DashAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.dotDamage.valueA.ToString();
        DashAT_DotDuration_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.dotDamage.valueB.ToString();
    }

    public void OnEnhanceUpPopup()
    {
        if(!enhanceUpPopup.gameObject.activeSelf)
        enhanceUpPopup.gameObject.SetActive(true);
    }

    #region Taps
    public void OnStatusTap()
    {
        statusTap.gameObject.SetActive(true);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive (false);
        dashAttackTap.gameObject .SetActive (false);
    }
    public void OnAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(true);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
    }
    public void OnDropAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(true);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
    }
    public void OnChargeAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(true);
        dashAttackTap.gameObject.SetActive(false);
    }
    public void OnDashAttackTap()
    {
        statusTap.gameObject.SetActive(false);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(true);
    }
    #endregion
}
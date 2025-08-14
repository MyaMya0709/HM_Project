using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUPrefixSum;

public class UI_EnhancePopup : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private RectTransform statusTap;
    [SerializeField] private TMP_Text before_AttackDamage_TMP;
    [SerializeField] private TMP_Text before_AttackSpeed_TMP;
    [SerializeField] private TMP_Text before_AttackRange_TMP;
    [SerializeField] private TMP_Text before_MoveSpeed_TMP;
    [SerializeField] private TMP_Text before_MovePower_TMP;
    
    [SerializeField] private TMP_Text after_AttackDamage_TMP;
    [SerializeField] private TMP_Text after_AttackSpeed_TMP;
    [SerializeField] private TMP_Text after_AttackRange_TMP;
    [SerializeField] private TMP_Text after_MoveSpeed_TMP;
    [SerializeField] private TMP_Text after_MovePower_TMP;

    [Header("AttackTap")]
    [SerializeField] private RectTransform attackTap;
    [SerializeField] private TMP_Text before_AT_AttackDamage_TMP;
    [SerializeField] private TMP_Text before_AT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text before_AT_AirbornePower_TMP;
    [SerializeField] private TMP_Text before_AT_StunDuration_TMP;
    [SerializeField] private TMP_Text before_AT_SlowDuration_TMP;
    [SerializeField] private TMP_Text before_AT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text before_AT_DotDamage_TMP;
    [SerializeField] private TMP_Text before_AT_DotDuration_TMP;

    [SerializeField] private TMP_Text after_AT_AttackDamage_TMP;
    [SerializeField] private TMP_Text after_AT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text after_AT_AirbornePower_TMP;
    [SerializeField] private TMP_Text after_AT_StunDuration_TMP;
    [SerializeField] private TMP_Text after_AT_SlowDuration_TMP;
    [SerializeField] private TMP_Text after_AT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text after_AT_DotDamage_TMP;
    [SerializeField] private TMP_Text after_AT_DotDuration_TMP;

    [Header("DropAttackTap")]
    [SerializeField] private RectTransform dropAttackTap;
    [SerializeField] private TMP_Text before_DropAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text before_DropAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text before_DropAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text before_DropAT_StunDuration_TMP;
    [SerializeField] private TMP_Text before_DropAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text before_DropAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text before_DropAT_DotDamage_TMP;
    [SerializeField] private TMP_Text before_DropAT_DotDuration_TMP;

    [SerializeField] private TMP_Text after_DropAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text after_DropAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text after_DropAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text after_DropAT_StunDuration_TMP;
    [SerializeField] private TMP_Text after_DropAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text after_DropAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text after_DropAT_DotDamage_TMP;
    [SerializeField] private TMP_Text after_DropAT_DotDuration_TMP;

    [Header("ChargeAttackTap")]
    [SerializeField] private RectTransform chargeAttackTap;
    [SerializeField] private TMP_Text before_ChargeAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_StunDuration_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_DotDamage_TMP;
    [SerializeField] private TMP_Text before_ChargeAT_DotDuration_TMP;

    [SerializeField] private TMP_Text after_ChargeAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_StunDuration_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_DotDamage_TMP;
    [SerializeField] private TMP_Text after_ChargeAT_DotDuration_TMP;

    [Header("DashAttackTap")]
    [SerializeField] private RectTransform dashAttackTap;
    [SerializeField] private TMP_Text before_DashAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text before_DashAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text before_DashAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text before_DashAT_StunDuration_TMP;
    [SerializeField] private TMP_Text before_DashAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text before_DashAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text before_DashAT_DotDamage_TMP;
    [SerializeField] private TMP_Text before_DashAT_DotDuration_TMP;

    [SerializeField] private TMP_Text after_DashAT_AttackDamage_TMP;
    [SerializeField] private TMP_Text after_DashAT_KnockbackPower_TMP;
    [SerializeField] private TMP_Text after_DashAT_AirbornePower_TMP;
    [SerializeField] private TMP_Text after_DashAT_StunDuration_TMP;
    [SerializeField] private TMP_Text after_DashAT_SlowDuration_TMP;
    [SerializeField] private TMP_Text after_DashAT_SlowDecrease_TMP;
    [SerializeField] private TMP_Text after_DashAT_DotDamage_TMP;
    [SerializeField] private TMP_Text after_DashAT_DotDuration_TMP;

    [Header("Taps")]
    [SerializeField] private Button statusTapBtn;
    [SerializeField] private Button attackTapBtn;
    [SerializeField] private Button dropAttackTapBtn;
    [SerializeField] private Button chargeAttackTapBtn;
    [SerializeField] private Button dashAttackTapBtn;

    [SerializeField] private TMP_Text afterLevel_TMP;
    [SerializeField] private TMP_Text spendGold_TMP;
    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Button exitBtn;

    [SerializeField] private RectTransform enhanceUI;

    [SerializeField] private TMP_Text curGoldTMP;

    private void OnEnable()
    {
        OnStatusTap();
        BottomInfoSet();
    }

    // 팝업창 하단부 세팅
    public void BottomInfoSet()
    {
        afterLevel_TMP.text = $"Lv.{GameManager.Instance.weaponData.baseWeaponLevel + 1}";
        spendGold_TMP.text = $"{GameManager.Instance.weaponData.data.enhanceCostList[GameManager.Instance.weaponData.baseWeaponLevel]}";
    }

    // 무기의 스텟, 효과 값 세팅용 함수
    public void UIInfoSet()
    {
        StatusInfoSet();
        AttackInfoSet();
        DropAttackInfoSet();
        ChargeAttackInfoSet();
        DashAttackInfoSet();
    }

    public void OnLevelUp()
    {
        Debug.Log("무기 렙업");
        // 재화 사용 저장 및 json 저장
        GameManager.Instance.SpendGold(GameManager.Instance.weaponData.data.enhanceCostList[GameManager.Instance.weaponData.baseWeaponLevel]);

        // 게임메니저의 무기 레벨업 및 json 저장
        GameManager.Instance.WeaponBaseLevelUp();

        BottomInfoSet();

        // 렙업 할 때 표기되고 있던 텝을 갱신
        if (statusTap.gameObject.activeSelf) StatusInfoSet();
        else if (attackTap.gameObject.activeSelf) AttackInfoSet();
        else if (dropAttackTap.gameObject.activeSelf) DropAttackInfoSet();
        else if (chargeAttackTap.gameObject.activeSelf) ChargeAttackInfoSet();
        else if (dashAttackTap.gameObject.activeSelf) DashAttackInfoSet();
        
        curGoldTMP.text = GameManager.Instance.curGold.ToString();
    }

    public void OnExit()
    {
        enhanceUI.GetComponent<UI_Enhance>().RightInfoSet();
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    #region InfoSet
    public void StatusInfoSet()
    {
        before_AttackDamage_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        //before_AttackSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";
        before_AttackRange_TMP.text = $"{GameManager.Instance.weaponData.data.baseRangeList[GameManager.Instance.curMWData.baseLevel]}";
        before_MoveSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseMoveList[GameManager.Instance.curMWData.baseLevel]}";
        //before_MovePower_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel]}";

        after_AttackDamage_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel + 1]}";
        //after_AttackSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel + 1]}";
        after_AttackRange_TMP.text = $"{GameManager.Instance.weaponData.data.baseRangeList[GameManager.Instance.curMWData.baseLevel + 1]}";
        after_MoveSpeed_TMP.text = $"{GameManager.Instance.weaponData.data.baseMoveList[GameManager.Instance.curMWData.baseLevel + 1]}";
        //after_MovePower_TMP.text = $"{GameManager.Instance.weaponData.data.baseDamageList[GameManager.Instance.curMWData.baseLevel + 1]}";
    }
    public void AttackInfoSet()
    {
        UISet(
            before_AT_AttackDamage_TMP,
            before_AT_KnockbackPower_TMP,
            before_AT_AirbornePower_TMP,
            before_AT_StunDuration_TMP,
            before_AT_SlowDuration_TMP,
            before_AT_SlowDecrease_TMP,
            before_AT_DotDamage_TMP,
            before_AT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect);

        UISet(
            after_AT_AttackDamage_TMP,
            after_AT_KnockbackPower_TMP,
            after_AT_AirbornePower_TMP,
            after_AT_StunDuration_TMP,
            after_AT_SlowDuration_TMP,
            after_AT_SlowDecrease_TMP,
            after_AT_DotDamage_TMP,
            after_AT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect);
    }
    public void DropAttackInfoSet()
    {
        UISet(
            before_DropAT_AttackDamage_TMP,
            before_DropAT_KnockbackPower_TMP,
            before_DropAT_AirbornePower_TMP,
            before_DropAT_StunDuration_TMP,
            before_DropAT_SlowDuration_TMP,
            before_DropAT_SlowDecrease_TMP,
            before_DropAT_DotDamage_TMP,
            before_DropAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect);

        UISet(
            after_DropAT_AttackDamage_TMP,
            after_DropAT_KnockbackPower_TMP,
            after_DropAT_AirbornePower_TMP,
            after_DropAT_StunDuration_TMP,
            after_DropAT_SlowDuration_TMP,
            after_DropAT_SlowDecrease_TMP,
            after_DropAT_DotDamage_TMP,
            after_DropAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect);
    }
    public void ChargeAttackInfoSet()
    {
        UISet(
            before_ChargeAT_AttackDamage_TMP,
            before_ChargeAT_KnockbackPower_TMP,
            before_ChargeAT_AirbornePower_TMP,
            before_ChargeAT_StunDuration_TMP,
            before_ChargeAT_SlowDuration_TMP,
            before_ChargeAT_SlowDecrease_TMP,
            before_ChargeAT_DotDamage_TMP,
            before_ChargeAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect);

        UISet(
            after_ChargeAT_AttackDamage_TMP,
            after_ChargeAT_KnockbackPower_TMP,
            after_ChargeAT_AirbornePower_TMP,
            after_ChargeAT_StunDuration_TMP,
            after_ChargeAT_SlowDuration_TMP,
            after_ChargeAT_SlowDecrease_TMP,
            after_ChargeAT_DotDamage_TMP,
            after_ChargeAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect);
    }
    public void DashAttackInfoSet()
    {
        UISet(
            before_DashAT_AttackDamage_TMP,
            before_DashAT_KnockbackPower_TMP,
            before_DashAT_AirbornePower_TMP,
            before_DashAT_StunDuration_TMP,
            before_DashAT_SlowDuration_TMP,
            before_DashAT_SlowDecrease_TMP,
            before_DashAT_DotDamage_TMP,
            before_DashAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect);

        UISet(
            after_DashAT_AttackDamage_TMP,
            after_DashAT_KnockbackPower_TMP,
            after_DashAT_AirbornePower_TMP,
            after_DashAT_StunDuration_TMP,
            after_DashAT_SlowDuration_TMP,
            after_DashAT_SlowDecrease_TMP,
            after_DashAT_DotDamage_TMP,
            after_DashAT_DotDuration_TMP,
            GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect);
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
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
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

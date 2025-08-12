using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        UIInfoSet();
        LevelUpInfoSet();
    }

    // 팝업창 하단부 세팅
    public void LevelUpInfoSet()
    {
        afterLevel_TMP.text = $"Lv.{GameManager.Instance.weaponData.baseWeaponLevel + 1}";
        spendGold_TMP.text = $"{GameManager.Instance.weaponData.data.enhanceCostList[GameManager.Instance.weaponData.baseWeaponLevel]}";
    }

    // 무기의 스텟, 효과 값 세팅용 함수
    public void UIInfoSet()
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

        before_AT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.damageMultiple.ToString();
        before_AT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.knockback.valueA.ToString();
        before_AT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.airborne.valueA.ToString();
        before_AT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.stun.valueB}s";
        before_AT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.slow.valueA}s";
        before_AT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.slow.valueB}%";
        before_AT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.dotDamage.valueA.ToString();
        before_AT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].attackEffect.dotDamage.valueB}s";

        after_AT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.damageMultiple.ToString();
        after_AT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.knockback.valueA.ToString();
        after_AT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.airborne.valueA.ToString();
        after_AT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.stun.valueB}s";
        after_AT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.slow.valueA}s";
        after_AT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.slow.valueB}%";
        after_AT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.dotDamage.valueA.ToString();
        after_AT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].attackEffect.dotDamage.valueB}s";

        before_DropAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.damageMultiple.ToString();
        before_DropAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.knockback.valueA.ToString();
        before_DropAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.airborne.valueA.ToString();
        before_DropAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.stun.valueB}s";
        before_DropAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.slow.valueA}s";
        before_DropAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.slow.valueB}%";
        before_DropAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.dotDamage.valueA.ToString();
        before_DropAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dropEffect.dotDamage.valueB}s";

        after_DropAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.damageMultiple.ToString();
        after_DropAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.knockback.valueA.ToString();
        after_DropAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.airborne.valueA.ToString();
        after_DropAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.stun.valueB}s";
        after_DropAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.slow.valueA}s";
        after_DropAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.slow.valueB}%";
        after_DropAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.dotDamage.valueA.ToString();
        after_DropAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dropEffect.dotDamage.valueB}s";

        before_ChargeAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.damageMultiple.ToString();
        before_ChargeAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.knockback.valueA.ToString();
        before_ChargeAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.airborne.valueA.ToString();
        before_ChargeAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.stun.valueB}s";
        before_ChargeAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.slow.valueA}s";
        before_ChargeAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.slow.valueB}%";
        before_ChargeAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.dotDamage.valueA.ToString();
        before_ChargeAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].chargeEffect.dotDamage.valueB}s";

        after_ChargeAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.damageMultiple.ToString();
        after_ChargeAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.knockback.valueA.ToString();
        after_ChargeAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.airborne.valueA.ToString();
        after_ChargeAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.stun.valueB}s";
        after_ChargeAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.slow.valueA}s";
        after_ChargeAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.slow.valueB}%";
        after_ChargeAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.dotDamage.valueA.ToString();
        after_ChargeAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].chargeEffect.dotDamage.valueB}s";

        before_DashAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.damageMultiple.ToString();
        before_DashAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.knockback.valueA.ToString();
        before_DashAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.airborne.valueA.ToString();
        before_DashAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.stun.valueB}s";
        before_DashAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.slow.valueA}s";
        before_DashAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.slow.valueB}%";
        before_DashAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.dotDamage.valueA.ToString();
        before_DashAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel].dashEffect.dotDamage.valueB}s";

        after_DashAT_AttackDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.damageMultiple.ToString();
        after_DashAT_KnockbackPower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.knockback.valueA.ToString();
        after_DashAT_AirbornePower_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.airborne.valueA.ToString();
        after_DashAT_StunDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.stun.valueB}s";
        after_DashAT_SlowDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.slow.valueA}s";
        after_DashAT_SlowDecrease_TMP.text = $"-{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.slow.valueB}%";
        after_DashAT_DotDamage_TMP.text = GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.dotDamage.valueA.ToString();
        after_DashAT_DotDuration_TMP.text = $"{GameManager.Instance.weaponData.data.baseWeaponEffectList[GameManager.Instance.weaponData.baseWeaponLevel + 1].dashEffect.dotDamage.valueB}s";
    }

    public void OnLevelUp()
    {
        Debug.Log("무기 렙업");
        // 재화 사용 저장 및 json 저장
        GameManager.Instance.SpendGold(GameManager.Instance.weaponData.data.enhanceCostList[GameManager.Instance.weaponData.baseWeaponLevel]);

        // 게임메니저의 무기 레벨업 및 json 저장
        GameManager.Instance.WeaponLevelUp();

        LevelUpInfoSet();
        UIInfoSet();
        curGoldTMP.text = GameManager.Instance.curGold.ToString();
    }

    public void OnExit()
    {
        enhanceUI.GetComponent<UI_Enhance>().UISet();
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    #region Taps
    public void OnStatusTap()
    {
        statusTap.gameObject.SetActive(true);
        attackTap.gameObject.SetActive(false);
        dropAttackTap.gameObject.SetActive(false);
        chargeAttackTap.gameObject.SetActive(false);
        dashAttackTap.gameObject.SetActive(false);
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

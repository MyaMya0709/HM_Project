using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnhancePopup : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enhance : MonoBehaviour
{
    //public WeaponData data;

    [Header("WeaponInfo")]
    public int weaponID;
    public WeaponData curMWData;
    public GameObject curWeapon;
    public IManualWeapon weaponData;

    [SerializeField] private TMP_Text name_TMP;
    [SerializeField] private TMP_Text level_TMP;
    [SerializeField] private TMP_Text discrip_TMP;
    [SerializeField] private Image image;


    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Button selecBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private GameObject selectedCheck;
    [SerializeField] private GameObject unlockCheck;

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

    [SerializeField] private RectTransform enhanceUpPopup;
    [SerializeField] private RectTransform purchasePopup;

    [SerializeField] private TMP_Text curGoldTMP;

    public bool isPurchase = false;
    public bool isUnlock = false;

    private void OnEnable()
    {
        weaponID = GameManager.Instance.weaponID;
        curMWData = GameManager.Instance.weaponDatas[weaponID];
        curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
        weaponData = curWeapon.GetComponent<IManualWeapon>();
        weaponData.baseWeaponLevel = curMWData.baseLevel;

        OnStatusTap();
        RightInfoSet();
    }


    public void WeaponBaseLevelUp()
    {
        curMWData.baseLevel++;
        weaponData.BaseLevelUp();

        // 보유 리스트의 
        for (int i = 0; i < GameManager.Instance.purchaseWeaponList.datas.Count; i++)
        {
            if (GameManager.Instance.purchaseWeaponList.datas[i].weaponID == curMWData.weaponID)
                Debug.Log($"{GameManager.Instance.purchaseWeaponList.datas[i].baseLevel}");
        }

        Debug.Log($"{curMWData.baseLevel}");
        Debug.Log($"{weaponData.baseWeaponLevel}");
        Debug.Log($"{GameManager.Instance.weaponDatas[weaponID].baseLevel}");

        GameManager.Instance.SaveWeaponData();
    }


    public void UnlockCheck()
    {
        // 현재 보여지는 캐릭터가 해금 되었는지 확인
        for (int i = 0; i < GameManager.Instance.unlockWeaponList.Count; i++)
        {
            if (GameManager.Instance.unlockWeaponList[i] == weaponID)
            {
                isUnlock = true;
                break;
            }
            else isUnlock = false;
        }
    }
    public void PurchaseChack()
    {
        // 현재 보여지는 캐릭터를 구입하였는지 확인
        for (int i = 0; i < GameManager.Instance.purchaseWeaponList.datas.Count; i++)
        {
            if (GameManager.Instance.purchaseWeaponList.datas[i].weaponID == weaponID)
            {
                isPurchase = true;
                break;
            }
            else isPurchase = false;
        }
    }


    public void OnChangeWeapon(Button btn)
    {
        // 메인무기 데이터 전체 순회
        for (int i = 0; i < DataManager.Instance.manualPrefabList.Count; i++)
        {
            // 데이터 리스트에서 현재 위치 확인
            if (DataManager.Instance.manualPrefabList[i].GetComponent<IManualWeapon>().data.weaponID == weaponID)
            {
                if (btn == leftBtn)
                {
                    // 이전 무기의 ID, Data 세팅
                    Debug.Log("좌측버튼 클릭");

                    // 현 위치가 0일 때
                    if (i - 1 < 0)
                    {
                        weaponID = DataManager.Instance.manualPrefabList[DataManager.Instance.manualPrefabList.Count - 1].GetComponent<IManualWeapon>().data.weaponID;
                    }
                    // 현 위치가 0이 아닐때
                    else
                    {
                        weaponID = DataManager.Instance.manualPrefabList[i - 1].GetComponent<IManualWeapon>().data.weaponID;
                    }

                    Debug.Log("이전 무기 ID");
                    break;
                }
                else if (btn == rightBtn)
                {
                    //다음 무기의 ID, Data 세팅
                    Debug.Log("우측버튼 클릭");

                    // 현 위치가 리스트의 마지막일 때
                    if (i + 1 > DataManager.Instance.manualPrefabList.Count - 1)
                    {
                        weaponID = DataManager.Instance.manualPrefabList[0].GetComponent<IManualWeapon>().data.weaponID;
                    }
                    // 현 위치가 리스트의 마지막이 아닐 때
                    else
                    {
                        weaponID = DataManager.Instance.manualPrefabList[i + 1].GetComponent<IManualWeapon>().data.weaponID;
                    }

                    Debug.Log("다음 무기 ID");
                    break;
                }
            }
        }
        RightInfoSet();

        if (statusTap.gameObject.activeSelf) StatusInfoSet();
        else if (attackTap.gameObject.activeSelf) AttackInfoSet();
        else if (dropAttackTap.gameObject.activeSelf) DropAttackInfoSet();
        else if (chargeAttackTap.gameObject.activeSelf) ChargeAttackInfoSet();
        else if (dashAttackTap.gameObject.activeSelf) DashAttackInfoSet();


    }
    public void OnEnhanceUpPopup()
    {
        if (!enhanceUpPopup.gameObject.activeSelf) enhanceUpPopup.gameObject.SetActive(true);
    }
    public void OnPurchasePopup()
    {
        purchasePopup.GetComponent<UI_PurchasePopup>().dataID = weaponID;
        if (!purchasePopup.gameObject.activeSelf) purchasePopup.gameObject.SetActive(true);
    }
    public void OnSelecWeapon()
    {
        GameManager.Instance.GetWeaponData(weaponID);
        selecBtn.gameObject.SetActive(false);
        selectedCheck.SetActive(true);
    }

    #region InfoSet
    public void RightInfoSet()
    {
        UnlockCheck();                         // 잠금해제 체크
        PurchaseChack();                       // 구입여부 체크

        // 무기 정보, 선택/구매 버튼 세팅
        if (isUnlock)
        {
            if (isPurchase)
            {
                curMWData = GameManager.Instance.weaponDatas[weaponID];
                curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
                weaponData = curWeapon.GetComponent<IManualWeapon>();
                weaponData.baseWeaponLevel = curMWData.baseLevel;

                if (weaponID == GameManager.Instance.weaponID)
                {
                    unlockCheck.SetActive(false);
                    levelUpBtn.gameObject.SetActive(true);
                    selectedCheck.SetActive(true);
                    selecBtn.gameObject.SetActive(false);
                    buyBtn.gameObject.SetActive(false);
                }
                else
                {
                    unlockCheck.SetActive(false);
                    levelUpBtn.gameObject.SetActive(true);
                    selectedCheck.SetActive(false);
                    selecBtn.gameObject.SetActive(true);
                    buyBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                curMWData = null;
                curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
                weaponData = curWeapon.GetComponent<IManualWeapon>();
                weaponData.baseWeaponLevel = 0;

                unlockCheck.SetActive(false);
                levelUpBtn.gameObject.SetActive(false);
                selectedCheck.SetActive(false);
                selecBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            curMWData = null;
            curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
            weaponData = curWeapon.GetComponent<IManualWeapon>();
            weaponData.baseWeaponLevel = 0;

            unlockCheck.SetActive(true);
            levelUpBtn.gameObject.SetActive(false);
            selectedCheck.SetActive(false);
            selecBtn.gameObject.SetActive(false);
            buyBtn.gameObject.SetActive(false);
        }

        //name_TMP.text = 
        level_TMP.text = $"Lv.{weaponData.baseWeaponLevel}";
        //discrip_TMP.text = 
        //image.sprite = 
    }
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
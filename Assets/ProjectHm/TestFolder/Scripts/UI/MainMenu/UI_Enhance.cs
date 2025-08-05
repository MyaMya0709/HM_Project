using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enhance : MonoBehaviour
{
    public WeaponData data;

    public Button enhanceBtn;

    [SerializeField] private TMP_Text weaponLevelTMP;

    [Header("Stat")]
    [SerializeField] private TMP_Text damageTMP;
    [SerializeField] private TMP_Text attackRangeTMP;
    [SerializeField] private TMP_Text attackSpeedTMP;
    [SerializeField] private TMP_Text moveSpeedTMP;
    [SerializeField] private TMP_Text jumpPowerTMP;

    [Header("attackType")]
    [SerializeField] private TMP_Text attackTypeTMP;

    private void Awake()
    {

    }


}

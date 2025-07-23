using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    #region PlayerBsaeStat
    public PlayerData playerData;
    public StatUpTableData statUpTableData;
    #endregion

    #region SelecStatTable
    public Dictionary<int, float> selecMoveSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };

    public Dictionary<int, float> selecAttackPowerDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };

    public Dictionary<int, float> selecAttckSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };
    #endregion

    #region WeaponData
    public Dictionary<int, GameObject> manualPrefabDic = new();
    public Dictionary<int, GameObject> autoPrefabDic = new();
    public Dictionary<int, ManualWeaponData> manualDataDic = new();
    public Dictionary<int, AutoWeaponData> autoDataDic = new();
    #endregion

    #region SelecWPUp
    #endregion

    #region SlotData
    public List<SlotData> allSlotDatas = new List<SlotData>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerData = playerData.LoadData();
        statUpTableData = statUpTableData.LoadData();

        // 슬롯데이터 로드
        foreach (SlotData data in Resources.LoadAll("ScriptableObject/Slots"))
        {
            allSlotDatas.Add(data);
        }
        Debug.Log($"모든 슬롯 데이터 : {allSlotDatas.Count}");

        // 무기 프리펩 로드
        foreach (GameObject obj in Resources.LoadAll("Prefabs/Weapons"))
        {
            if (obj.TryGetComponent<IAutoWeapon>(out IAutoWeapon AWP))
            {
                autoPrefabDic.Add(AWP.data.weaponID, obj);
            }
            else if (obj.TryGetComponent<IManualWeapon>(out IManualWeapon MWP))
            {
                manualPrefabDic.Add(MWP.data.weaponID, obj);
            }
            else
            {
                Debug.Log($"프리펩이 아님");
            }
        }
        Debug.Log($"자동 무기 프리펩 : {autoPrefabDic.Count}");
        Debug.Log($"수동 무기 프리펩 : {manualPrefabDic.Count}");

        //무기 데이터 로드
        foreach (var obj in Resources.LoadAll("ScriptableObject/Weapon"))
        {
            if (obj is AutoWeaponData autoData)
            {
                autoDataDic.Add(autoData.weaponID, autoData);
            }
            else if (obj is ManualWeaponData manualData)
            {
                manualDataDic.Add(manualData.weaponID, manualData);
            }
            else
            {
                Debug.Log($"무기데이터가 아님");
            }
        }
        Debug.Log($"자동 무기 데이터 : {autoDataDic.Count}");
        Debug.Log($"수동 무기 데이터 : {manualDataDic.Count}");
    }

    private void Start()
    {
        
    }
}

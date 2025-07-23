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
    // weaponID로 Prefab,Data 찾기
    public List<GameObject> manualPrefabList = new();
    public List<ManualWeaponData> manualDataList = new();

    // weaponID - 100으로 Prefab,Data 찾기
    public List<GameObject> autoPrefabList = new();
    public List<AutoWeaponData> autoDataList = new();
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
    }

    private void Start()
    {

    }
}
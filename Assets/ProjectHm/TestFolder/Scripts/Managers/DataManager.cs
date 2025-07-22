using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

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

    #region SelecWeapon
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

        foreach (SlotData data in Resources.LoadAll("ScriptableObject/Slots"))
        {
            allSlotDatas.Add(data);
        }
    }
    private void Start()
    {
        
    }
}

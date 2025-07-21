using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DataManager : Singleton<DataManager>
{
    #region PlayerBsaeStat
    public PlayerData playerData;
    public StatUpTableData statUpTableData;
    #endregion

    #region SelecStat
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

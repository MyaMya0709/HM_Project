using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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
    [SerializeField] public Dictionary<int, WeaponData> weaponDataDic = new();
    // weaponID로 Prefab,Data 찾기
    public List<GameObject> manualPrefabList = new();
    public List<ManualWeaponData> manualDataList = new();

    // weaponID - 100으로 Prefab,Data 찾기
    public List<GameObject> autoPrefabList = new();
    public List<AutoWeaponData> autoDataList = new();
    #endregion

    #region SlotData
    public List<SlotData> allSlotDatas = new List<SlotData>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerData = playerData.LoadData();
        statUpTableData = statUpTableData.LoadData();
        WeaponDataLoad();
    }

    public void WeaponDataLoad(string fileName = default)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.IsNullOrEmpty(fileName) ? $"Data/{typeof(WeaponData)}" : $"Data/{fileName}");
        if (textAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        weaponDataDic = JsonConvert.DeserializeObject<WeaponDataContainer>(textAsset.text).data;
        Debug.Log($"데이터 로드 성공 : {weaponDataDic.Count}");
    }
}
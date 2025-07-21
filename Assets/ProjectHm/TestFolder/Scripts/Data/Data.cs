using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;

    public void Clear()
    {
        moveSpeedLevel = 1;
        attackPowerLevel = 1;
        attackSpeedLevel = 1;
    }
    public PlayerData LoadData()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("Data/PlayerData"); // 경로에서 확장자 제외
        if (jsonAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonAsset.text);
        if (data == null)
            Debug.Log("PlayerDataLoad 실패");

        return data;
    }
}
#endregion

#region StatUpTableData
[Serializable]
public class StatUpTableData
{
    // key:playerLevel / value:MaxExp
    public Dictionary<int, float> maxExpList;
    // key:statLevel / value:movaSpeed
    public Dictionary<int, float> moveSpeedDic;
    // key:statLevel / value:attackPower
    public Dictionary<int, float> attackPowerDic;
    // key:statLevel / value:attackSpeed
    public Dictionary<int, float> attackSpeedDic;

    public StatUpTableData LoadData()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("Data/StatUpTableData"); // 경로에서 확장자 제외
        if (jsonAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        StatUpTableData data = JsonConvert.DeserializeObject<StatUpTableData>(jsonAsset.text);
        if (data == null)
            Debug.Log("PlayerDataLoad 실패");

        return data;
    }
}
#endregion

//public class SlotData
//{
//    public SlotType type;
//    public string name;
//    public Image icon;
//    public string description;
//    public StatType stat;               // 스탯 업그레이드
//    public IWeapon weapon;              // 자동무기 장착
//    public WeaponUpData weaponUpData;   // 자동무기 업그레이드
//} 

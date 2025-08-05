using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int playerLevel;
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;

    public int characterID;
    public int weaponID;
    public int haveGold;
    public int statUpPoint;

    public List<int> openCharacterIDList;

    public void Clear()
    {
        playerLevel = 0;
        moveSpeedLevel = 0;
        attackPowerLevel = 0;
        attackSpeedLevel = 0;
        characterID = 0;
        weaponID = 0;
        haveGold = 0;
        statUpPoint = 0;
        openCharacterIDList.Clear();
        openCharacterIDList.Add(0);
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

#region WeaponData
[Serializable]
public class WeaponData
{
    public int weaponID;
    public int baseLevel;
}

[Serializable]
public class WeaponDataDic
{
    public Dictionary<int, WeaponData> data;

    public WeaponDataDic LoadData(string fileName = default)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.IsNullOrEmpty(fileName) ? $"Data/{typeof(WeaponData)}" : $"Data/{fileName}");
        if (textAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        WeaponDataDic dic = JsonConvert.DeserializeObject<WeaponDataDic>(textAsset.text);
        if (dic.data == null)
            Debug.Log("PlayerDataLoad 실패");

        return dic;
    }
}
#endregion
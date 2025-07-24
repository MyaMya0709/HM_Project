using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;

    public void Clear()
    {
        moveSpeedLevel = 0;
        attackPowerLevel = 0;
        attackSpeedLevel = 0;
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
public class WeaponDataContainer
{
    public Dictionary<int, WeaponData> data = new();
}
#endregion
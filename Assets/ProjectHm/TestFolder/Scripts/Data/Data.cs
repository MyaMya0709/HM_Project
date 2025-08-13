using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int playerLevel;
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int statUpPoint;
    public int haveGold;

    public int characterID;
    public List<int> openCharacterIDList;

    public int weaponID;

    public void Clear()
    {
        playerLevel = 0;
        moveSpeedLevel = 0;
        attackPowerLevel = 0;
        attackSpeedLevel = 0;
        statUpPoint = 0;
        haveGold = 999999999;

        characterID = 0;
        openCharacterIDList.Clear();
        openCharacterIDList.Add(0);

        weaponID = 0;
    }
}
#endregion

#region WeaponData
[Serializable]
public class WeaponData
{
    public int weaponID;
    public int baseLevel;
    public bool isBuy = false;      // 구입여부
}

[Serializable]
public class WeaponDataList
{
    public List<WeaponData> datas;

    public void Clear()
    {
        datas = new List<WeaponData>()
            {
                new WeaponData { weaponID = 0, baseLevel = 0, isBuy = true },
                new WeaponData { weaponID = 100, baseLevel = 0, isBuy = true }
            };
    }
}
#endregion
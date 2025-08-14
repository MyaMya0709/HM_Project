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
}

[Serializable]
public class WeaponDataList
{
    public List<WeaponData> datas;

    public void Clear()
    {
        datas = new List<WeaponData>()
            {
                new WeaponData { weaponID = 0, baseLevel = 0 }
            };
    }
}
#endregion

#region UnlockData
[Serializable]
public class UnlockData
{
    public List<int> characterIDs;
    public List<int> skillIDs;
    public List<int> waeaponIDs;
    public List<int> autoWeaponIDs;

    public void Clear()
    {
        characterIDs = new List<int>() { 0 };
        skillIDs = new List<int> { 0 };
        waeaponIDs = new List<int> { 0 };
        autoWeaponIDs = new List<int> { 0 };
    }
}
#endregion
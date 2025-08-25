using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.TextCore.Text;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int playerLevel;

    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int movePowerLevel;
    public int actPowerLevel;
    public int masteryLevel;

    public int statUpPoint;
    public int haveGold;

    public int characterID;
    public int weaponID;

    public void Clear()
    {
        playerLevel = 0;

        attackPowerLevel = 0;
        attackSpeedLevel = 0;
        movePowerLevel = 0;
        actPowerLevel = 0;
        masteryLevel = 0;

        statUpPoint = 0;
        haveGold = 999999999;

        characterID = 0;
        weaponID = 100;
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
        datas.Clear();
        datas.Add(new WeaponData { weaponID = 100, baseLevel = 0 });
    }
}
#endregion

#region PurchaseCharacterData
[Serializable]
public class PurchaseCharacterList
{
    public List<int> characterIDs;

    public void Clear()
    {
        characterIDs = new List<int>() { 0, 1, 2 };
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
        characterIDs = new List<int>() { 0,1,2 };
        skillIDs = new List<int> { 0 };
        waeaponIDs = new List<int> { 100 };
        autoWeaponIDs = new List<int> { 200 };
    }
}
#endregion
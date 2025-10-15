using System;
using System.Collections.Generic;

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

    public int skillID;

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

        skillID = 0;
    }
}
#endregion

#region WeaponData
// 저장해야하는 무기 데이터
[Serializable]
public class WeaponData
{
    public int weaponID;
    public int baseLevel;
    public int clearStage;
}

// 구매한 무기 데이터
[Serializable]
public class PurchaseWeaponList
{
    public List<WeaponData> datas;

    public void Clear()
    {
        datas.Clear();
        datas.Add(new WeaponData { weaponID = 100, baseLevel = 0, clearStage = 0 });
    }
}
#endregion

// 구매한 케릭터 데이터
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

// 잠금 해제 데이터
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
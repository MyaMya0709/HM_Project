using System;
using System.Collections.Generic;
using UnityEngine;

#region PlayerData
[Serializable]
public class PlayerData
{
    public int playerLevel;
    // key:playerLevel / value:MaxExp
    public Dictionary<int, float> maxExpList;
    public float curExp;

    //Key:statName /Value:statLevel
    public Dictionary<string, int> statLevelDic;
    public float moveSpeed;
    public float attackPower;
    public float attackSpeed;

    // key:statLevel / value:movaSpeed
    public Dictionary<int, float> moveSpeedDic;
    // key:statLevel / value:attackPower
    public Dictionary<int, float> attackPowerDic;
    // key:statLevel / value:attackSpeed
    public Dictionary<int, float> attackSpeedDic;

    public float GetValue(string statName)
    {
        if (!statLevelDic.TryGetValue(statName, out int statLevel))
            throw new ArgumentException($"{statName}에 해당하는 레벨 값이 없음");

        Dictionary<int, float> statDic = new();

        switch (statName)
        {
            case "moveSpeed":
                statDic = moveSpeedDic;
                break;
            case "attackPower":
                statDic = attackPowerDic;
                break;
            case "attackSpeed":
                statDic = attackSpeedDic;
                break;
            default:
                throw new ArgumentException($"{statName}에 해당하는 딕셔너리가 없음");
        }

        if (!statDic.TryGetValue(statLevel, out float result))
            throw new ArgumentException($"{statName}의 {statLevel}레벨에 해당하는 값이 없음");

        return result;
    }

    public void Clear(PlayerData playerData)
    {
        playerLevel = 1;
        curExp = 0;
        statLevelDic = new Dictionary<string, int>
        {
            { "moveSpeed", 1 },
            { "attackPower", 1 },
            { "attackSpeed", 1 }
        };
    }
}
#endregion

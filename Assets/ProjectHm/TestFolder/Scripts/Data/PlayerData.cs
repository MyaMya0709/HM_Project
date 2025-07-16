using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    public int maxLevel;
    public float maxHealth;
    public float moveSpeed;
    public float attackPower;
    public float attackSpeed;
    public float dashPower = 80f;
    public float jumpForce = 50f;
    // key:Level / value:MaxExp
    public Dictionary<int, float> maxExpDic;
    // key:Level / value:healthRate
    public Dictionary<int, float> healthRateDic;
    // key:Level / value:speedRate
    public Dictionary<int, float> speedRateDic;
    // key:Level / value:attackPowerRate
    public Dictionary<int, float> attackPowerRateDic;
    // key:Level / value:attackSpeedRate
    public Dictionary<int, float> attackSpeedRateDic;
    // key:Level / value:dashPowerRate
    public Dictionary<int, float> dashPowerRateDic;
    // key:Level / value:jumpForceRate
    public Dictionary<int, float> jumpForceRateDic;
}

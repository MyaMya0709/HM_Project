using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerClearData : ScriptableObject
{
    public int maxLevel;
    public float moveSpeed;
    public float attackPower;
    // key:Level / value:MaxExp
    public List<float> maxExpList;
    // key:Level / value:movaSpeed
    public List<float> moveSpeedList;
    // key:Level / value:attackPower
    public List<float> attackPowerList;
}

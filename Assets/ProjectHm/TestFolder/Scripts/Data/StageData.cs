using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stage")]
public class StageData : ScriptableObject
{
    public int maxWave;
    public float waveDelay;
    public int stageCost;
    public int[] poolID;
}

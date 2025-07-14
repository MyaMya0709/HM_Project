using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Group")]
public class GroupData : ScriptableObject
{
    // List 순서 : 소환 순서
    public List<SpawnData> spawnList;
    // waveSpawnCost를 계산할 단위
    public int groupCost;
}

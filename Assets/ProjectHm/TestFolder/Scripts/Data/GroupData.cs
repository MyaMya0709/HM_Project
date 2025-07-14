using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Group")]
public class GroupData : ScriptableObject
{
    // List ���� : ��ȯ ����
    public List<SpawnData> spawnList;
    // waveSpawnCost�� ����� ����
    public int groupCost;
}

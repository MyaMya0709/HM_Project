using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave2")]
public class StageData
{
    // � ��, Round ������ ���� ������, 

    public List<WaveData2> spawnSequence;

}

public class WaveData2
{
    public List<GroupData> spawnSequence;
    public float spawnDelay;
    public float roundSpawnCost = 25f;


}

[CreateAssetMenu(menuName = "Data/Group")]
public class GroupData
{
    // Key : ��ȯ ���� / Value : �� ������
    //public Dictionary<int, EnemyData> spawnSequence;

    // List ���� : ��ȯ ����
    public List<EnemyData> spawnSequence;
    // RoundSpawnCost�� ����� ����
    public int groupLevel;

    //List<EEEEEE>...
    int value = 0;
}

public class EEEEEE
{
    EnemyData enemyData;
    int aaaa = 0;
    int bbb = 0;
    float eee = 0f;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave2")]
public class StageData
{
    // 어떤 적, Round 사이의 스폰 딜레이, 

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
    // Key : 소환 순서 / Value : 적 데이터
    //public Dictionary<int, EnemyData> spawnSequence;

    // List 순서 : 소환 순서
    public List<EnemyData> spawnSequence;
    // RoundSpawnCost를 계산할 단위
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

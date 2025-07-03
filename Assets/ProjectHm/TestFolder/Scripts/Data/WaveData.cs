using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave")]
public class WaveData : ScriptableObject
{
    public GameObject[] enemyPrefabs;    // 적의 정보가 들어갈 오브젝트
    public EnemyData[] spawnEnemyList;   // 적의 종류
    public int enemyCount = 9999;
    public float spawnInterval = 1f;
}

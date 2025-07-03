using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave")]
public class WaveData : ScriptableObject
{
    public EnemyData[] spawnEnemyList;   // 적의 종류
    public int enemyCount = 9999;
    public float spawnInterval = 1f;
}

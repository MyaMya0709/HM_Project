using UnityEngine;

[CreateAssetMenu(menuName = "Data/Spawn")]
public class SpawnData : ScriptableObject
{
    public EnemyData enemyData;
    public int spawnPointNum;
    public float spawnDelay;
}
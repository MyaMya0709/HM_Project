using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave")]
public class WaveData : ScriptableObject
{
    public GameObject enemyPrefab;
    public EnemyAI[] spawnEnemyList;
    public int enemyCount = 9999;
    public float spawnInterval = 1f;
}

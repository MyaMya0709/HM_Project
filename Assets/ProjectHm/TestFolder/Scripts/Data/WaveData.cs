using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave")]
public class WaveData : ScriptableObject
{
    public GameObject[] enemyPrefabs;    // ���� ������ �� ������Ʈ
    public EnemyData[] spawnEnemyList;   // ���� ����
    public int enemyCount = 9999;
    public float spawnInterval = 1f;
}

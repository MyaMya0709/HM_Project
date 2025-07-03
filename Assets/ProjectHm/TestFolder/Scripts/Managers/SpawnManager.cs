using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<WaveData> waves = new();
    public List<Transform> spawnPoints;

    //private int maxWave = 0;
    private int currentWaveIndex = 0;
    private int aliveEnemies = 0;
    private bool isSpawning = false;

    public System.Action<int> OnWaveStarted;
    public System.Action OnAllWavesCleared;

    public void StartWaves()
    {
        ////Spawner���� ������� ������Ʈ�� Tranform Ž��
        //Transform parentsPoint = GameObject.Find("EnemySpawner").transform;
        //if (parentsPoint != null)
        //{
        //    // �θ� �Ʒ��� �ڽ� Transform���� ��ȸ
        //    foreach (Transform child in parentsPoint)
        //    {
        //        spawnPoints.Add(child);
        //    }
        //}

        //LoadData();
        currentWaveIndex = 0;
        StartCoroutine(RunWave(currentWaveIndex));
    }

    //public void LoadData()
    //{
    //    for (int i = 0; i <= maxWave; i++)
    //    {
    //        WaveData curWave = Resources.Load<WaveData>($"ScriptableObject/Wave_{currentWaveIndex}");
    //        if (curWave == null)
    //        {
    //            Debug.LogWarning("Resources ������ 'WaveData.asset'�� �����ϴ�.");
    //            return;
    //        }
    //        waves.Add(curWave);
    //    }
    //}

    private IEnumerator RunWave(int waveIndex)
    {
        isSpawning = true;

        if (waveIndex > waves.Count)
        {
            //��� ���̺� ����� ȣ��
            OnAllWavesCleared?.Invoke();
            yield break;
        }

        WaveData wave = waves[waveIndex];
        OnWaveStarted?.Invoke(waveIndex + 1);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        aliveEnemies++;

        //������ enemy�� ������ HandleEnemyDeath() ����
        enemy.GetComponent<EnemyAI>().OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        aliveEnemies--;
        if (!isSpawning && aliveEnemies <= 0)
        {
            currentWaveIndex++;
            StartCoroutine(RunWave(currentWaveIndex));
        }
    }
}

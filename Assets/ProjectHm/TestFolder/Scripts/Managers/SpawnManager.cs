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
        ////Spawner들을 묶어놓은 오브젝트의 Tranform 탐색
        //Transform parentsPoint = GameObject.Find("EnemySpawner").transform;
        //if (parentsPoint != null)
        //{
        //    // 부모 아래의 자식 Transform들을 순회
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
    //            Debug.LogWarning("Resources 폴더에 'WaveData.asset'이 없습니다.");
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
            //모든 웨이브 종료시 호출
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

        //생성된 enemy가 죽으면 HandleEnemyDeath() 실행
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<WaveData> waves = new();             // WaveData�� ����Ʈ�� ��ü ���̺� ����
    public List<Transform> spawnGruondPoints;        // ���� ���� ����Ʈ
    public List<Transform> spawnSkyPoints;           // ���� ���� ����Ʈ

    public Transform attackPoint;

    private int maxWave;
    private int currentWaveIndex = 0;
    private int aliveEnemies = 0;
    private bool isSpawning = false;

    private Coroutine spawnCoroutine;

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
        maxWave = waves.Count;
        currentWaveIndex = 0;
        spawnCoroutine = StartCoroutine(RunWave(currentWaveIndex));
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

        if (waveIndex > maxWave)
        {
            //��� ���̺� ����� ȣ�� - Invoke(�̰��� �Լ� �Է� ����)
            OnAllWavesCleared?.Invoke();
            yield break;
        }

        // ���� ���̺꿡 �ش��ϴ� ���̺� ������ ȣ��
        WaveData wave = waves[waveIndex];

        // ���̺��� ��ü �� ���� ����
        aliveEnemies = wave.enemyCount;

        OnWaveStarted?.Invoke(waveIndex + 1);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.spawnEnemyList[Random.Range(0, wave.spawnEnemyList.Length)]);
            yield return new WaitForSeconds(wave.spawnInterval);

            // ���� �ı��� StopCoroutine() ����
            if (GameManager.Instance.isGameOver == true)
            {
                StopCoroutine();
            }
        }

        isSpawning = false;
    }

    private void SpawnEnemy(EnemyData enemyData)
    {
        Transform spawnPoint;
        if (enemyData.MoveType == EnemyMoveType.Ground)
        {
            spawnPoint = spawnGruondPoints[Random.Range(0, spawnGruondPoints.Count)];
        }
        else
        {
            spawnPoint = spawnSkyPoints[Random.Range(0, spawnSkyPoints.Count)];
        }

        GameObject enemy = Instantiate(enemyData.enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<BaseEnemy>().enemyData = enemyData;
        enemy.GetComponent<BaseEnemy>().target = attackPoint;
        //aliveEnemies++;

        //������ enemy�� ������ HandleEnemyDeath() ����
        enemy.GetComponent<BaseEnemy>().OnDeath += HandleEnemyDeath;
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

    public void StopCoroutine()
    {
        Debug.Log("���� ���� �ߴ�");
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}

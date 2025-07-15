using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<WaveData> waves;
    public List<SpawnData> spawnDataList = new();    // ������ ���� ������ ����Ʈ

    public List<Transform> spawnGruondPoints;        // ���� ���� ����Ʈ
    public List<Transform> spawnSkyPoints;           // ���� ���� ����Ʈ

    public Transform attackPoint;

    private int maxWave;
    public float waveDelay = 10f;
    private int currentWaveIndex = 0;
    private int aliveEnemies = 0;
    private bool isSpawning = false;

    private Coroutine spawnCoroutine;

    public System.Action<int> OnWaveStarted;
    public System.Action OnAllWavesCleared;

    public void StartWaves()
    {
        waves = StageManager.Instance.waveList;

        //spawnGruondPoints = 
        //spawnSkyPoints = 

        maxWave = waves.Count;
        currentWaveIndex = 0;
        spawnCoroutine = StartCoroutine(RunWave(currentWaveIndex));
    }

    // wave ����
    private IEnumerator RunWave(int waveIndex)
    {
        Debug.Log($"Wave {waveIndex+1}");
        isSpawning = true;

        if (waveIndex == 0)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(waveDelay);

        if (waveIndex > maxWave)
        {
            //��� ���̺� ����� ȣ�� - Invoke(�̰��� �Լ� �Է� ����)
            OnAllWavesCleared?.Invoke();
            yield break;
        }

        // ���� ���̺꿡 �ش��ϴ� ���̺� ������ ȣ��
        WaveData wave = new WaveData()
        {
            groupList = new List<GroupData>()
        };
        wave = waves[waveIndex];

        // ���̺� ���۽� ȣ�� ex) ���̺� ���� UI
        OnWaveStarted?.Invoke(waveIndex + 1);

        foreach (GroupData waveGroup in wave.groupList)
        {
            if (waveGroup != null)
            {
                for (int i = 0; i < waveGroup.spawnList.Count; i++)
                {
                    spawnDataList.Add(waveGroup.spawnList[i]);
                }
                Debug.Log($"spawnDataList Add Data, listCount : {spawnDataList.Count}");
            }
            else
            {
                Debug.Log("waveGroup is null");
            }
        }

        // ���̺��� ��ü �� ���� ����
        aliveEnemies = spawnDataList.Count;
        Debug.Log(aliveEnemies);

        for (int i = 0; i < spawnDataList.Count; i++)
        {
            SpawnEnemy(spawnDataList[i].enemyData);
            yield return new WaitForSeconds(spawnDataList[i].spawnDelay);

            // ���� �ı��� StopCoroutine() ����
            if (GameManager.Instance.isGameOver == true)
            {
                StopCoroutine();
            }
        }

        isSpawning = false;
    }

    // �� ��üȭ �� ���� ���� ���
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

    // �����ִ� ���� �� == 0 / ���� ���̺� ����
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

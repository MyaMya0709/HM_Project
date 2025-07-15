using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<WaveData> waves;
    public List<SpawnData> spawnDataList = new();    // 생성할 적의 데이터 리스트

    public List<Transform> spawnGruondPoints;        // 지상 생성 포인트
    public List<Transform> spawnSkyPoints;           // 공중 생성 포인트

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

    // wave 시작
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
            //모든 웨이브 종료시 호출 - Invoke(이곳에 함수 입력 예정)
            OnAllWavesCleared?.Invoke();
            yield break;
        }

        // 현재 웨이브에 해당하는 웨이브 데이터 호출
        WaveData wave = new WaveData()
        {
            groupList = new List<GroupData>()
        };
        wave = waves[waveIndex];

        // 웨이브 시작시 호출 ex) 웨이브 시작 UI
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

        // 웨이브의 전체 적 수량 저장
        aliveEnemies = spawnDataList.Count;
        Debug.Log(aliveEnemies);

        for (int i = 0; i < spawnDataList.Count; i++)
        {
            SpawnEnemy(spawnDataList[i].enemyData);
            yield return new WaitForSeconds(spawnDataList[i].spawnDelay);

            // 기지 파괴시 StopCoroutine() 실행
            if (GameManager.Instance.isGameOver == true)
            {
                StopCoroutine();
            }
        }

        isSpawning = false;
    }

    // 적 실체화 및 적의 숫자 계산
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

        //생성된 enemy가 죽으면 HandleEnemyDeath() 실행
        enemy.GetComponent<BaseEnemy>().OnDeath += HandleEnemyDeath;
    }

    // 남아있는 적의 수 == 0 / 다음 웨이브 시작
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
        Debug.Log("몬스터 스폰 중단");
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}

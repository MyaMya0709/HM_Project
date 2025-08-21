using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public UIManager uiManager;
    public StageManager stageManager;

    public List<WaveData> waves;
    public List<SpawnData> spawnDataList = new();            // 생성할 적의 데이터 리스트

    public List<Transform> spawnGruondPoints = new();        // 지상 생성 포인트
    public List<Transform> spawnSkyPoints = new();           // 공중 생성 포인트

    public Transform attackPoint;

    public int maxWave;
    public float waveDelay = 10f;
    public int currentWaveIndex = 0;
    public int killEnemies = 0;
    public int aliveEnemies = 0;
    public bool isSpawning = false;
    public bool isGameFinish = false;

    public Coroutine spawnCoroutine;

    public System.Action<int> OnWaveStarted;
    public event Action<bool> OnFinishGame;


    // 웨이브 시작
    public void StartWaves()
    {
        waves = stageManager.waveList;

        maxWave = stageManager.data.maxWave;
        currentWaveIndex = 0;
        spawnCoroutine = StartCoroutine(RunWave(currentWaveIndex));
    }

    // wave 생성 로직
    private IEnumerator RunWave(int waveIndex)
    {
        Debug.Log($"Wave {waveIndex+1}");

        if (waveIndex >= maxWave)
        {
            Debug.Log($"Wave Finish");
            yield break;
        }

        isSpawning = true;

        if (waveIndex == 0)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(waveDelay);

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
                //Debug.Log($"spawnDataList Add Data, listCount : {spawnDataList.Count}");
            }
            else
            {
                Debug.Log("waveGroup is null");
            }
        }

        // 웨이브의 전체 적 수량 저장
        aliveEnemies += spawnDataList.Count;
        Debug.Log($"적의 수: {aliveEnemies}");

        // 웨이브 스폰
        for (int i = 0; i < spawnDataList.Count; i++)
        {
            SpawnEnemy(spawnDataList[i].enemyData);
            yield return new WaitForSeconds(spawnDataList[i].spawnDelay);

            // 기지 파괴시 StopCoroutine() 실행
            if (isGameFinish == true)
            {
                StopCoroutine();
            }
        }

        isSpawning = false;

        //웨이브 스폰 끝나면 바로 다음 웨이브 시작
        currentWaveIndex++;
        spawnCoroutine = StartCoroutine(RunWave(currentWaveIndex));
    }

    // 적 실체화 및 적의 숫자 계산
    private void SpawnEnemy(EnemyData enemyData)
    {
        Transform spawnPoint;
        if (enemyData.MoveType == EnemyMoveType.Ground)
        {
            spawnPoint = spawnGruondPoints[UnityEngine.Random.Range(0, spawnGruondPoints.Count)];
        }
        else
        {
            spawnPoint = spawnSkyPoints[UnityEngine.Random.Range(0, spawnSkyPoints.Count)];
        }

        GameObject enemy = Instantiate(enemyData.enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<BaseEnemy>().enemyData = enemyData;
        enemy.GetComponent<BaseEnemy>().target = attackPoint;
        //aliveEnemies++;

        //생성된 enemy가 죽으면 HandleEnemyDeath() 실행
        enemy.GetComponent<BaseEnemy>().OnDeath += HandleEnemyDeath;
    }

    // 남아있는 적의 수 == 0 / 
    private void HandleEnemyDeath()
    {
        aliveEnemies--;
        killEnemies++;
        // 게임오버 로직 실행
          if (!isSpawning && aliveEnemies <= 0)
        {
            //모든 웨이브 종료시 호출
            isGameFinish = true;
            //게임 클리어 UI 호출
            uiManager.finishUI.OnEnableFinshUI(!isSpawning);
        }
    }

    public void StopCoroutine()
    {
        Debug.Log("몬스터 스폰 중단");

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;

    }
}

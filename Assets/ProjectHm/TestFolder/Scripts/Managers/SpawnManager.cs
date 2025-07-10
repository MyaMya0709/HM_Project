using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<WaveData2> waves2;
    public List<SpawnData> spawnDataList;

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

        waves2 = StageManager.Instance.waveList;


        maxWave = waves2.Count;
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
    //            Debug.LogWarning("Resources 폴더에 'WaveData.asset'이 없습니다.");
    //            return;
    //        }
    //        waves.Add(curWave);
    //    }
    //}

    private IEnumerator RunWave(int waveIndex)
    {
        isSpawning = true;

        yield return new WaitForSeconds(waveDelay);

        if (waveIndex > maxWave)
        {
            //모든 웨이브 종료시 호출 - Invoke(이곳에 함수 입력 예정)
            OnAllWavesCleared?.Invoke();
            yield break;
        }

        // 현재 웨이브에 해당하는 웨이브 데이터 호출
        WaveData2 wave = new WaveData2()
        {
            groupList = new List<GroupData>()
        };

        wave = waves2[waveIndex];

        // 웨이브의 전체 적 수량 저장
        for (int i = 0; i < wave.groupList.Count; i++)
        {
            Debug.Log(wave.groupList.Count);
            aliveEnemies += wave.groupList[i].spawnList.Count;
        }

        OnWaveStarted?.Invoke(waveIndex + 1);

        foreach (GroupData groupList in wave.groupList)
        {
            if (groupList != null)
            {
                for (int i = 0; i < groupList.spawnList.Count; i++)
                {
                    spawnDataList.Add(groupList.spawnList[i]);
                }
            }
        }

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

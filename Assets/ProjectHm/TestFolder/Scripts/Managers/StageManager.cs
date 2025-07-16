using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public StageData data;

    // Stage의 GroupData의 배열
    public List<GroupData> GroupList;

    public List<WaveData> waveList = new();
    public List<int> waveCostList = new();

    private void Start()
    {
        // 현재씬 파악 후 로직 수행
        string current = SceneManager.GetActiveScene().name;

        if (current == "InGame")
            GameManager.Instance.GameStart();
    }

    // StageData Setting
    public void StageSet()
    {
        data = Resources.Load<StageData>($"ScriptableObject/Stage/TestStage");
        // Stage의 GroupData 불러오기
        GroupList = PoolManager.Instance.LoadGroupPool(data);
        Debug.Log($"ScriptableObjectList : {GroupList.Count}");

        // 분배한 코스트 재분배
        waveCostList.Clear();
        WaveCostSet();

        // 웨이브 세팅
        for (int i= 0; i < data.maxWave; i++)
        {
            waveList.Add(MakeWave(waveCostList[i]));
        }
    }

    // stageSpawnCost를 maxWave의 수만큼 분배
    public void WaveCostSet()
    {
        Debug.Log("Stagecost -> WaveCost");
        float[] temp = new float[data.maxWave];
        int[] result = new int[data.maxWave];

        // 각 배열에 총 cost의 비율 분배
        float sum = 0f;
        for (int i = 0; i < data.maxWave; i++)
        {
            temp[i] = Random.value;
            sum += temp[i];
        }

        // 정수 부분 채우기
        int sumInt = 0;
        for (int i = 0; i < data.maxWave; i++)
        {
            result[i] = Mathf.FloorToInt(temp[i] * data.stageCost / sum);
            sumInt += result[i];
        }

        // stageSpawnCost - sumInt 만큼 무작위로 +1 배분
        int deficit = data.stageCost - sumInt;
        for (int i = 0; i < deficit; i++)
        {
            int idx = Random.Range(0, data.maxWave);
            result[idx]++;
        }

        // waveCostList에 저장
        for (int i = 0;i < data.maxWave; i++)
        waveCostList.Add(result[i]);
    }

    // 저장된 waveCost를 바탕으로 WaveData 생성
    public WaveData MakeWave(int maxCost)
    {
        if (GroupList == null || GroupList.Count == 0)
        {
            Debug.LogError("stageGroupList가 null이거나 비어있습니다.");
            return null;
        }

        Debug.Log($"MakeWave, {maxCost}");
        WaveData makeWave = new WaveData()
        {
            groupList = new List<GroupData>()
        };
        int curCost = 0;
        int groupCount = 0;
        
        // wave에 무작위 group 추가
        while (curCost < maxCost && GroupList.Count > 0)
        {
            GroupData randomGroupData = GroupList[Random.Range(0, GroupList.Count - 1)];
            
            makeWave.groupList.Add(randomGroupData);
            curCost += randomGroupData.groupCost;

            groupCount++;
        }

        if (makeWave.groupList.Count > 0)
        {
            // 마지막 추가된 그룹 제거
            GroupData last = makeWave.groupList.Last();
            curCost -= last.groupCost;
            makeWave.groupList.Remove(last);
        }

        // wave의 마지막에 들어갈 그룹 후보군 검색
        List<GroupData> list = new List<GroupData>();
        int remainCost = maxCost - curCost;
        foreach (GroupData data in GroupList)
        {
            if (remainCost - 1 <= data.groupCost && data.groupCost <= remainCost + 1)
            {
                list.Add(data);
            }
        }

        // wave의 마지막에 들어갈 그룹 렌덤 추가
        if (list.Count > 0)
        {
            makeWave.groupList.Add(list[Random.Range(0, list.Count - 1)]);
        }
        else
        {
            Debug.LogWarning($"remainCost({remainCost})에 맞는 그룹이 없어서 마지막 그룹을 추가하지 않았습니다.");
        }

        return makeWave;
    }

}

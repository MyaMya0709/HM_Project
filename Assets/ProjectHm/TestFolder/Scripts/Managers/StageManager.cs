using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public StageData data;

    // 전체 GroupData의 배열
    public GroupData[] stageGroupList;

    public List<WaveData2> waveList = new();
    public List<int> waveCostList;



    protected override void Awake()
    {
        base.Awake();

        LoadGroupList();

        StageSet();
    }

    // StageData Setting
    public void StageSet()
    {
        waveCostList.Clear();

        WaveCostSet();

        for (int i= 0; i < data.maxWave; i++)
        {
            waveList.Add(MakeWave(waveCostList[i]));
        }
    }

    // 전체 GroupData 불러오기
    public void LoadGroupList()
    {
        stageGroupList = Resources.LoadAll<GroupData>($"ScriptableObject");
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
    public WaveData2 MakeWave(int maxCost)
    {
        if (stageGroupList == null || stageGroupList.Length == 0)
        {
            Debug.LogError("stageGroupList가 null이거나 비어있습니다.");
            return null;
        }

        Debug.Log($"MakeWave, {maxCost}");
        WaveData2 makeWave = new WaveData2()
        {
            groupList = new List<GroupData>()
        };
        int curCost = 0;
        int groupCount = 0;
        
        // wave에 무작위 group 추가
        while (curCost < maxCost && stageGroupList.Length > 0)
        {
            GroupData randomGroupData = stageGroupList[Random.Range(0, stageGroupList.Length)];
            
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
        foreach (GroupData data in stageGroupList)
        {
            if (remainCost-1 <= data.groupCost && data.groupCost <= remainCost + 1)
            {
                list.Add(data);
            }
        }

        // wave의 마지막에 들어갈 그룹 렌덤 추가
        if (list.Count > 0)
        {
            makeWave.groupList.Add(list[Random.Range(0, list.Count)]);
        }
        else
        {
            Debug.LogWarning($"remainCost({remainCost})에 맞는 그룹이 없어서 마지막 그룹을 추가하지 않았습니다.");
        }

        return makeWave;
    }

}

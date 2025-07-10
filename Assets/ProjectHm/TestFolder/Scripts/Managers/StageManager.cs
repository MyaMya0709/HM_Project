using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public StageData data;

    // ��ü GroupData�� �迭
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

    // ��ü GroupData �ҷ�����
    public void LoadGroupList()
    {
        stageGroupList = Resources.LoadAll<GroupData>($"ScriptableObject");
    }

    // stageSpawnCost�� maxWave�� ����ŭ �й�
    public void WaveCostSet()
    {
        Debug.Log("Stagecost -> WaveCost");
        float[] temp = new float[data.maxWave];
        int[] result = new int[data.maxWave];

        // �� �迭�� �� cost�� ���� �й�
        float sum = 0f;
        for (int i = 0; i < data.maxWave; i++)
        {
            temp[i] = Random.value;
            sum += temp[i];
        }

        // ���� �κ� ä���
        int sumInt = 0;
        for (int i = 0; i < data.maxWave; i++)
        {
            result[i] = Mathf.FloorToInt(temp[i] * data.stageCost / sum);
            sumInt += result[i];
        }

        // stageSpawnCost - sumInt ��ŭ �������� +1 ���
        int deficit = data.stageCost - sumInt;
        for (int i = 0; i < deficit; i++)
        {
            int idx = Random.Range(0, data.maxWave);
            result[idx]++;
        }

        // waveCostList�� ����
        for (int i = 0;i < data.maxWave; i++)
        waveCostList.Add(result[i]);
    }

    // ����� waveCost�� �������� WaveData ����
    public WaveData2 MakeWave(int maxCost)
    {
        if (stageGroupList == null || stageGroupList.Length == 0)
        {
            Debug.LogError("stageGroupList�� null�̰ų� ����ֽ��ϴ�.");
            return null;
        }

        Debug.Log($"MakeWave, {maxCost}");
        WaveData2 makeWave = new WaveData2()
        {
            groupList = new List<GroupData>()
        };
        int curCost = 0;
        int groupCount = 0;
        
        // wave�� ������ group �߰�
        while (curCost < maxCost && stageGroupList.Length > 0)
        {
            GroupData randomGroupData = stageGroupList[Random.Range(0, stageGroupList.Length)];
            
            makeWave.groupList.Add(randomGroupData);
            curCost += randomGroupData.groupCost;

            groupCount++;
        }

        if (makeWave.groupList.Count > 0)
        {
            // ������ �߰��� �׷� ����
            GroupData last = makeWave.groupList.Last();
            curCost -= last.groupCost;
            makeWave.groupList.Remove(last);
        }

        // wave�� �������� �� �׷� �ĺ��� �˻�
        List<GroupData> list = new List<GroupData>();
        int remainCost = maxCost - curCost;
        foreach (GroupData data in stageGroupList)
        {
            if (remainCost-1 <= data.groupCost && data.groupCost <= remainCost + 1)
            {
                list.Add(data);
            }
        }

        // wave�� �������� �� �׷� ���� �߰�
        if (list.Count > 0)
        {
            makeWave.groupList.Add(list[Random.Range(0, list.Count)]);
        }
        else
        {
            Debug.LogWarning($"remainCost({remainCost})�� �´� �׷��� ��� ������ �׷��� �߰����� �ʾҽ��ϴ�.");
        }

        return makeWave;
    }

}

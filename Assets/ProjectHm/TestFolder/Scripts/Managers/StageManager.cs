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

    // Stage�� GroupData�� �迭
    public List<GroupData> GroupList;

    public List<WaveData> waveList = new();
    public List<int> waveCostList = new();

    private void Start()
    {
        // ����� �ľ� �� ���� ����
        string current = SceneManager.GetActiveScene().name;

        if (current == "InGame")
            GameManager.Instance.GameStart();
    }

    // StageData Setting
    public void StageSet()
    {
        data = Resources.Load<StageData>($"ScriptableObject/Stage/TestStage");
        // Stage�� GroupData �ҷ�����
        GroupList = PoolManager.Instance.LoadGroupPool(data);
        Debug.Log($"ScriptableObjectList : {GroupList.Count}");

        // �й��� �ڽ�Ʈ ��й�
        waveCostList.Clear();
        WaveCostSet();

        // ���̺� ����
        for (int i= 0; i < data.maxWave; i++)
        {
            waveList.Add(MakeWave(waveCostList[i]));
        }
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
    public WaveData MakeWave(int maxCost)
    {
        if (GroupList == null || GroupList.Count == 0)
        {
            Debug.LogError("stageGroupList�� null�̰ų� ����ֽ��ϴ�.");
            return null;
        }

        Debug.Log($"MakeWave, {maxCost}");
        WaveData makeWave = new WaveData()
        {
            groupList = new List<GroupData>()
        };
        int curCost = 0;
        int groupCount = 0;
        
        // wave�� ������ group �߰�
        while (curCost < maxCost && GroupList.Count > 0)
        {
            GroupData randomGroupData = GroupList[Random.Range(0, GroupList.Count - 1)];
            
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
        foreach (GroupData data in GroupList)
        {
            if (remainCost - 1 <= data.groupCost && data.groupCost <= remainCost + 1)
            {
                list.Add(data);
            }
        }

        // wave�� �������� �� �׷� ���� �߰�
        if (list.Count > 0)
        {
            makeWave.groupList.Add(list[Random.Range(0, list.Count - 1)]);
        }
        else
        {
            Debug.LogWarning($"remainCost({remainCost})�� �´� �׷��� ��� ������ �׷��� �߰����� �ʾҽ��ϴ�.");
        }

        return makeWave;
    }

}

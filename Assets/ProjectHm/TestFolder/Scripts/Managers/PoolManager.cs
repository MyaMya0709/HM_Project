using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public StageGroupPool[] allStageGroupPool;
    protected override void Awake()
    {
        base.Awake();
        allStageGroupPool = Resources.LoadAll<StageGroupPool>($"ScriptableObject/StageGroupPool");

        if (allStageGroupPool != null || allStageGroupPool.Length == 0)
            Debug.Log("전체 GroupPool 로드 오류");
    }

    // Stage에 사용할 GroupPool을 Load
    public List<GroupData> LoadGroupPool(StageData data)
    {
        List<GroupData> groupPool = new();

        foreach (StageGroupPool pool in allStageGroupPool)
        {
            if (pool != null) Debug.Log("AllStageGroupPool 순회 불가");

            for (int i = 0; i < data.poolID.Length; i++)
            {
                if (pool.ID == data.poolID[i])
                {
                    groupPool.AddRange(pool.groupPool);
                }
                else
                {
                    Debug.Log("StageGroupPool 추가 불가");
                }
            }
        }
        return groupPool;
    }
}

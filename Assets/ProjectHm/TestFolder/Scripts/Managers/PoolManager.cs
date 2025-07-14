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
            Debug.Log("��ü GroupPool �ε� ����");
    }

    // Stage�� ����� GroupPool�� Load
    public List<GroupData> LoadGroupPool(StageData data)
    {
        List<GroupData> groupPool = new();

        foreach (StageGroupPool pool in allStageGroupPool)
        {
            if (pool != null) Debug.Log("AllStageGroupPool ��ȸ �Ұ�");

            for (int i = 0; i < data.poolID.Length; i++)
            {
                if (pool.ID == data.poolID[i])
                {
                    groupPool.AddRange(pool.groupPool);
                }
                else
                {
                    Debug.Log("StageGroupPool �߰� �Ұ�");
                }
            }
        }
        return groupPool;
    }
}

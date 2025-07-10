using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroupList")]
public class StageEnemyList : ScriptableObject
{
    public List<GroupData> enemyPool;
}
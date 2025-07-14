using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroupList")]
public class StageGroupPool : ScriptableObject
{
    public int ID;
    public List<GroupData> groupPool;
}
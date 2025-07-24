using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/groupList")]
public class StageGroupPool : ScriptableObject
{
    public int ID;
    public List<GroupData> groupPool;
}
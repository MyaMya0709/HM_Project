using Newtonsoft.Json;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        playerData = playerData.LoadData();
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerData playerData = new();
    public Dictionary<int, WeaponData> weaponDataDic = new();

    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        playerData = playerData.LoadData();
        WeaponDataLoad();
    }

    public void GameStart()
    {
        StageManager.Instance.StageSet();
        SpawnManager.Instance.StartWaves();
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void WeaponDataLoad(string fileName = default)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.IsNullOrEmpty(fileName) ? $"Data/{typeof(WeaponData)}" : $"Data/{fileName}");
        if (textAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        weaponDataDic = JsonConvert.DeserializeObject<WeaponDataContainer>(textAsset.text).data;
        Debug.Log($"데이터 로드 성공 : {weaponDataDic.Count}");
    }
}


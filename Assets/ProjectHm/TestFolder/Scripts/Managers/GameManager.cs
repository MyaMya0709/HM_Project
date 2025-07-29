using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData = new();
    [SerializeField] private CharacterData characterData;
    public Dictionary<int, WeaponData> weaponDataDic = new();

    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        playerData = playerData.LoadData();
        characterData = SetCharacterData();
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

    public PlayerData SetPlayerData()
    {
        return playerData;
    }

    public void GetPlayerData(PlayerData data)
    {
        playerData = data;
    }

    public CharacterData SetCharacterData()
    {
        CharacterData data = new();
        foreach (CharacterData CData in DataManager.Instance.characterDataList)
        {
            // playerData의 characterID와 같은 아이디의 CharacterData 찾아서 참조
            if (CData.ID == playerData.characterID)
            {
                data = CData;
                if (data.name != null) Debug.Log($"CharacterData: {data.ID} Load");

                return data;
            }
        }

        if(data.name == null) Debug.Log($"CharacterData Load 실패");
        return null;
    }

    public void WeaponDataLoad(string fileName = default)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.IsNullOrEmpty(fileName) ? $"Data/{typeof(WeaponData)}" : $"Data/{fileName}");
        if (textAsset == null)
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");

        weaponDataDic = JsonConvert.DeserializeObject<WeaponDataContainer>(textAsset.text).data;
        Debug.Log($"데이터 로드 성공 : {weaponDataDic.Count}");
    }

    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerData.json"), json);
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        string json;
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            if (playerData != null) Debug.Log($"playerDataLoad");
        }
    }
}


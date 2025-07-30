using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData = new();
    [SerializeField] private WeaponData curMWData = new();
    [SerializeField] private CharacterData curCharacterData;

    // 해금된 캐릭터만 가진 Dic으로 변경 예정
    [SerializeField] private CharacterData characterDataDic;
    // 해금된 무기만 가진 Dic
    public WeaponDataDic weaponDataDic;

    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        LoadPlayerData();
        LoadCharacterData();

        weaponDataDic = weaponDataDic.LoadData();
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
        return characterDataDic;
    }


    public void LoadCharacterData()
    {
        foreach (CharacterData CData in DataManager.Instance.characterDataList)
        {
            // playerData의 characterID와 같은 아이디의 CharacterData 찾아서 참조
            if (CData.ID == playerData.characterID)
            {
                characterDataDic = CData;
                if (characterDataDic.name != null) Debug.Log($"CharacterData: {characterDataDic.ID} Load");
            }
        }
        if (characterDataDic.name == null) Debug.Log($"CharacterData Load 실패");
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerData.json"), json);
        Debug.Log(Application.persistentDataPath);
    }
    public void LoadPlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        string json;
        if (File.Exists(path))
        {
            // 파일 있으면 로드
            json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            if (playerData != null) Debug.Log($"playerDataLoad");
        }
        else
        {
            // 파일 없으면 클리어 파일 세이브 후, 데이터 클리어
            playerData.Clear();
            json = JsonUtility.ToJson(playerData);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerData.json"), json);

            if (playerData != null) Debug.Log($"New PlayerData Save");

        }
    }

    public void GameReset()
    {
        string json;
        playerData.Clear();
        json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerData.json"), json);

        if (playerData != null) Debug.Log($"New PlayerData Save");

        // TODO : 무기, 캐릭터 정보 리셋로직 작성
    }
}


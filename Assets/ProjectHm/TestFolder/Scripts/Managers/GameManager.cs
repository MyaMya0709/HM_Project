using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData = new();
    public int playerLevel;
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;

    public int characterID;
    [SerializeField] private CharacterData characterData;
    public List<int> openCharacterIDList;

    public int weaponID;
    public int curGold;
    public int statUpPoint;

    [SerializeField] private WeaponData curMWData = new();


    // 해금된 무기만 가진 Dic
    public WeaponDataDic weaponDataDic;

    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        LoadPlayerData();
        GetCharacterData();

        weaponDataDic = weaponDataDic.LoadData();

        playerLevel = playerData.playerLevel;
        moveSpeedLevel = playerData.moveSpeedLevel;
        attackPowerLevel = playerData.attackPowerLevel;
        attackSpeedLevel = playerData.attackSpeedLevel;

        characterID = playerData.characterID;
        weaponID = playerData.weaponID;
        curGold = playerData.haveGold;
        statUpPoint = playerData.statUpPoint;

        openCharacterIDList = playerData.openCharacterIDList;
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
    public void GetPlayerData()
    {
        playerData.playerLevel = playerLevel;
        playerData.moveSpeedLevel = moveSpeedLevel;
        playerData.attackPowerLevel = attackPowerLevel;
        playerData.attackSpeedLevel = attackSpeedLevel;

        playerData.characterID = characterID;
        playerData.weaponID = weaponID;
        playerData.haveGold = curGold;
        playerData.statUpPoint = statUpPoint;

        playerData.openCharacterIDList = openCharacterIDList;
    }


    public CharacterData SetCharacterData()
    {
        return characterData;
    }

    public void GetCharacterData()
    {
        foreach (CharacterData CData in DataManager.Instance.characterDataList)
        {
            if(CData.ID == characterID)
            {
                characterData = CData;
            }
        }
        if (characterData == null) Debug.Log($"CharacterData Load 실패");

        playerData.characterID = characterData.ID;
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


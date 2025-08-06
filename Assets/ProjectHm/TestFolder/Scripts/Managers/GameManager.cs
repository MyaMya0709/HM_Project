using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData = new();
    public int playerLevel;
    public int statUpPoint;

    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;

    public int curGold;

    public int characterID;
    [SerializeField] private CharacterData characterData;
    public List<int> openCharacterIDList;

    public int weaponID;
    public WeaponData curMWData;
    public IManualWeapon weaponData;

    // 해금된 무기
    public WeaponDataList openWeaponList;
    public Dictionary<int, WeaponData> weaponDatas = new();


    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        LoadPlayerData();
        GetCharacterData();

        LoadWeaponData();

        playerLevel = playerData.playerLevel;
        moveSpeedLevel = playerData.moveSpeedLevel;
        attackPowerLevel = playerData.attackPowerLevel;
        attackSpeedLevel = playerData.attackSpeedLevel;
        statUpPoint = playerData.statUpPoint;
        curGold = playerData.haveGold;

        characterID = playerData.characterID;
        openCharacterIDList = playerData.openCharacterIDList;

        weaponID = playerData.weaponID;
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
        playerData.statUpPoint = statUpPoint;
        playerData.haveGold = curGold;

        playerData.characterID = characterID;
        openCharacterIDList = playerData.openCharacterIDList;

        playerData.weaponID = weaponID;
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

    public void SaveWeaponData()
    {
        string json = JsonUtility.ToJson(openWeaponList);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "WeaponData.json"), json);
        Debug.Log(Application.persistentDataPath);
    }
    public void LoadWeaponData()
    {
        string path = Path.Combine(Application.persistentDataPath, "WeaponData.json");

        string json;
        if (File.Exists(path))
        {
            // 파일 있으면 로드
            json = File.ReadAllText(path);

            //리스트에 역직렬화
            openWeaponList = JsonUtility.FromJson<WeaponDataList>(json);
            if (openWeaponList.datas.Count == 0) Debug.Log($"openWeaponList LoadFail");

            //Dictionary으로 전환
            foreach (WeaponData waepon in openWeaponList.datas)
            {
                weaponDatas.Add(waepon.weaponID, waepon);
            }
            if (weaponDatas.Count != 0) Debug.Log($"weaponDataLoad");
        }
        else
        {
            // 파일 없으면 클리어 파일 세이브 후, 데이터 클리어
            openWeaponList.Clear();

            //Dictionary으로 전환
            foreach (WeaponData waepon in openWeaponList.datas)
            {
                weaponDatas.Add(waepon.weaponID, waepon);
            }

            json = JsonUtility.ToJson(openWeaponList);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "WeaponData.json"), json);

            if (weaponDatas.Count != 0) Debug.Log($"New WeaponData Save");
        }
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


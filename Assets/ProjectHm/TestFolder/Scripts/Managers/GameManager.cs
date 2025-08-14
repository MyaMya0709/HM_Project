using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private PlayerData playerData = new();
    public int playerLevel;
    public int statUpPoint;
    public int moveSpeedLevel;
    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int weaponID;
    public int characterID;
    public int curGold;

    [Header("Character")]
    public CharacterData curCharacterData;
    public PurchaseCharacterList purchaseCharacterList;                  // 구매한 캐릭터 리스트
    public Dictionary<int, CharacterData> characterDataDic = new();      // 구매한 캐릭터 Dic

    [Header("Weapon")]
    public WeaponData curMWData;                                         // 현재 장착한 무기의 ID,Level
    public GameObject curWeapon;                                         // 현재 장착한 무기 프리펩
    public IManualWeapon weaponData;                                     // 현재 장착한 무기 데이터
    public WeaponDataList openWeaponList;                                // 구입한 무기 리스트
    public Dictionary<int, WeaponData> weaponDatas = new();              // 구입한 무기 Dic

    [Header("UnLockDatas")]
    public UnlockData unlockData;                                        // 해금된 요소의 Data
    public List<int> unlockCharacterList;                                // 해금된 캐릭터 ID 리스트
    public List<int> unlockSkillList;                                    // 해금된 스킬 ID 리스트
    public List<int> unlockWeaponList;                                   // 해금된 무기 ID 리스트
    public List<int> unlockAutoWeaponList;                               // 해금된 자동무기 ID 리스트

    public int selecStageID = 0;

    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        LoadPlayerData();
        LoadUnLockData();
        LoadWeaponData();

        playerLevel = playerData.playerLevel;
        moveSpeedLevel = playerData.moveSpeedLevel;
        attackPowerLevel = playerData.attackPowerLevel;
        attackSpeedLevel = playerData.attackSpeedLevel;
        statUpPoint = playerData.statUpPoint;
        curGold = playerData.haveGold;

        characterID = playerData.characterID;
        //openCharacterIDList = playerData.openCharacterIDList;

        weaponID = playerData.weaponID;

        GetCharacterData();
        GetWeaponData();
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
    public void GameReset()
    {
        string json;
        playerData.Clear();
        json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerData.json"), json);

        if (playerData != null) Debug.Log($"New PlayerData Save");

        // TODO : 무기, 캐릭터 정보 리셋로직 작성
    }


    public void WeaponInit(GameObject holder)
    {
        Instantiate(weaponData.gameObject, holder.transform);
    }

    public void WeaponBaseLevelUp()
    {
        curMWData.baseLevel++;
        weaponData.BaseLevelUp();

        for (int i = 0; i < openWeaponList.datas.Count; i++)
        {
            if(openWeaponList.datas[i].weaponID == curMWData.weaponID)
            Debug.Log($"{openWeaponList.datas[i].baseLevel}");
        }

        Debug.Log($"{curMWData.baseLevel}");
        Debug.Log($"{weaponData.baseWeaponLevel}");
        Debug.Log($"{weaponDatas[curMWData.weaponID].baseLevel}");

        SaveWeaponData();
    }

    public void SpendGold(int gold)
    {
        curGold -= gold;
        GetPlayerData();
        SavePlayerData();
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

        playerData.weaponID = weaponID;
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


    public void GetWeaponData()
    {
        curMWData = weaponDatas[weaponID];
        curWeapon = DataManager.Instance.manualPrefabList[weaponID];
        weaponData = curWeapon.GetComponent<IManualWeapon>();
        weaponData.baseWeaponLevel = curMWData.baseLevel;
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
            if (openWeaponList.datas.Count != 0) Debug.Log($"openWeaponList Load");

            //Dictionary으로 전환
            foreach (WeaponData waepon in openWeaponList.datas)
            {
                weaponDatas.Add(waepon.weaponID, waepon);
            }
            if (weaponDatas.Count != 0) Debug.Log($"weaponData Load");
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


    public CharacterData SetCharacterData()
    {
        return curCharacterData;
    }
    public void GetCharacterData()
    {
        foreach (CharacterData CData in DataManager.Instance.characterDataList)
        {
            if(CData.ID == characterID)
            {
                curCharacterData = CData;
            }
        }
        if (curCharacterData == null) Debug.Log($"CharacterData Load 실패");

        playerData.characterID = curCharacterData.ID;
    }
    public void SaveCharacterData()
    {
        string json = JsonUtility.ToJson(purchaseCharacterList);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "CharacterData.json"), json);
        Debug.Log(Application.persistentDataPath);
    }
    public void LoadCharacterData()
    {
        string path = Path.Combine(Application.persistentDataPath, "CharacterData.json");

        string json;
        if (File.Exists(path))
        {
            // 파일 있으면 로드
            json = File.ReadAllText(path);

            //리스트에 역직렬화
            purchaseCharacterList = JsonUtility.FromJson<PurchaseCharacterList>(json);
            if (purchaseCharacterList.characterIDs.Count != 0) Debug.Log($"purchaseCharacterList Load");

            //Dictionary으로 전환
            foreach (int waepon in purchaseCharacterList.characterIDs)
            {
                characterDataDic.Add(waepon, DataManager.Instance.characterDataList[waepon]);
            }
            if (characterDataDic.Count != 0) Debug.Log($"characterDataDic Load");
        }
        else
        {
            // 파일 없으면 클리어 파일 세이브 후, 데이터 클리어
            purchaseCharacterList.Clear();

            //Dictionary으로 전환
            foreach (int waepon in purchaseCharacterList.characterIDs)
            {
                characterDataDic.Add(waepon, DataManager.Instance.characterDataList[waepon]);
            }

            json = JsonUtility.ToJson(purchaseCharacterList);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "CharacterData.json"), json);

            if (characterDataDic.Count != 0) Debug.Log($"new characterDataDic Save");
        }
    }


    public void SaveUnlockData()
    {
        string json = JsonUtility.ToJson(unlockData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "unlockData.json"), json);
        Debug.Log(Application.persistentDataPath);
    }
    public void LoadUnLockData()
    {
        string path = Path.Combine(Application.persistentDataPath, "UnlockData.json");

        string json;
        if (File.Exists(path))
        {
            // 파일 있으면 로드
            json = File.ReadAllText(path);

            // 역직렬화
            unlockData = JsonUtility.FromJson<UnlockData>(json);

            // 각각의 리스트에 할당
            unlockCharacterList = unlockData.characterIDs;
            unlockSkillList = unlockData.skillIDs;
            unlockWeaponList = unlockData.waeaponIDs;
            unlockAutoWeaponList = unlockData.autoWeaponIDs;
            if (unlockCharacterList.Count != 0) Debug.Log($"unlockCharacterList Load");
            if (unlockSkillList.Count != 0) Debug.Log($"unlockSkillList Load");
            if (unlockWeaponList.Count != 0) Debug.Log($"unlockWeaponList Load");
            if (unlockAutoWeaponList.Count != 0) Debug.Log($"unlockAutoWeaponList Load");
        }
        else
        {
            // 파일 없으면 클리어 파일 세이브 후, 데이터 클리어
            unlockData.Clear();

            // 각각의 리스트에 할당
            unlockCharacterList = unlockData.characterIDs;
            unlockSkillList = unlockData.skillIDs;
            unlockWeaponList = unlockData.waeaponIDs;
            unlockAutoWeaponList = unlockData.autoWeaponIDs;
            if (unlockCharacterList.Count != 0) Debug.Log($"unlockCharacterList Load");
            if (unlockSkillList.Count != 0) Debug.Log($"unlockSkillList Load");
            if (unlockWeaponList.Count != 0) Debug.Log($"unlockWeaponList Load");
            if (unlockAutoWeaponList.Count != 0) Debug.Log($"unlockAutoWeaponList Load");

            // 해금데이터 Json 저장 
            json = JsonUtility.ToJson(unlockData);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "UnlockData.json"), json);
            if (unlockData.characterIDs.Count != 0) Debug.Log($"New unlockData Save");
        }
    }
}


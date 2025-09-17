using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TMP_Text curGoldTMP;

    [Header("Player")]
    [SerializeField] private PlayerData playerData = new();
    public int playerLevel;
    public int statUpPoint;

    public int attackPowerLevel;
    public int attackSpeedLevel;
    public int movePowerLevel;
    public int actPowerLevel;
    public int masteryLevel;

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
    public IManualWeapon weaponData;                                     // 현재 장착한 무기 정보 데이터
    public WeaponDataList purchaseWeaponList;                            // 구입한 무기 리스트
    public Dictionary<int, WeaponData> weaponDatas = new();              // 구입한 무기 Dic

    [Header("UnLockDatas")]
    public UnlockData unlockData;                                        // 해금된 요소의 Data
    public List<int> unlockCharacterList;                                // 해금된 캐릭터 ID 리스트
    public List<int> unlockSkillList;                                    // 해금된 스킬 ID 리스트
    public List<int> unlockWeaponList;                                   // 해금된 무기 ID 리스트
    public List<int> unlockAutoWeaponList;                               // 해금된 자동무기 ID 리스트

    public int selecStageID = 0;

    public bool isGameFinish = false;

    protected override void Awake()
    {
        base.Awake();

        LoadPlayerData();

        playerLevel = playerData.playerLevel;

        attackPowerLevel = playerData.attackPowerLevel;
        attackSpeedLevel = playerData.attackSpeedLevel;
        movePowerLevel = playerData.movePowerLevel;
        actPowerLevel = playerData.actPowerLevel;
        masteryLevel = playerData.masteryLevel;

        statUpPoint = playerData.statUpPoint;
        curGold = playerData.haveGold;

        characterID = playerData.characterID;
        weaponID = playerData.weaponID;

        LoadUnLockData();
        LoadWeaponData();
        LoadCharacterData();

        GetCharacterData(characterID);
        GetWeaponData(weaponID);
    }

    
    public void GameFinish()
    {
        isGameFinish = true;
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
        GetWeaponData(weaponID);
        Instantiate(weaponData.gameObject, holder.transform);
    }


    public void SetGold()
    {
        curGoldTMP.text = curGold.ToString();
    }
    public void GetGold(int gold)
    {
        curGold += gold;
        SaveData();
    }
    public void SpendGold(int gold)
    {
        curGold -= gold;
        SaveData();
    }
    public void SaveData()
    {
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

        playerData.attackPowerLevel = attackPowerLevel;
        playerData.attackSpeedLevel = attackSpeedLevel;
        playerData.movePowerLevel = movePowerLevel;
        playerData.actPowerLevel = actPowerLevel;
        playerData.masteryLevel = masteryLevel;

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


    public IManualWeapon SetWeaponData()
    {
        curMWData = weaponDatas[weaponID];
        curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
        weaponData = curWeapon.GetComponent<IManualWeapon>();
        weaponData.baseWeaponLevel = curMWData.baseLevel;
        SaveData();
        return weaponData;
    }
    public void GetWeaponData(int id)
    {
        Debug.Log("장착 무기 정보 세팅");
        weaponID = id;
        curMWData = weaponDatas[weaponID];
        curWeapon = DataManager.Instance.manualPrefabList[weaponID - 100];
        weaponData = curWeapon.GetComponent<IManualWeapon>();
        weaponData.baseWeaponLevel = curMWData.baseLevel;
        SaveData();
    }
    public void SaveWeaponData()
    {
        string json = JsonUtility.ToJson(purchaseWeaponList);
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
            purchaseWeaponList = JsonUtility.FromJson<WeaponDataList>(json);
            if (purchaseWeaponList.datas.Count != 0) Debug.Log($"purchaseWeaponList Load");

            //Dictionary으로 전환
            foreach (WeaponData waepon in purchaseWeaponList.datas)
            {
                weaponDatas.Add(waepon.weaponID, waepon);
            }
            if (weaponDatas.Count != 0) Debug.Log($"weaponData Load");
        }
        else
        {
            // 파일 없으면 클리어 파일 세이브 후, 데이터 클리어
            purchaseWeaponList.Clear();

            //Dictionary으로 전환
            foreach (WeaponData waepon in purchaseWeaponList.datas)
            {
                weaponDatas.Add(waepon.weaponID, waepon);
            }

            json = JsonUtility.ToJson(purchaseWeaponList);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "WeaponData.json"), json);

            if (weaponDatas.Count != 0) Debug.Log($"New WeaponData Save");
        }
    }
    public void BuyWeapon(int id)
    {
        // 중복 여부 확인
        bool isOverlap = false;
        foreach (WeaponData data in purchaseWeaponList.datas)
        {
            if (data.weaponID != id)
            {
                isOverlap = false;
            }
            else
            {
                isOverlap = true;
                Debug.Log("purchaseWeaponList에 중복 요소 있음");
                break;
            }
        }

        // 중복이 없다면 실행
        if (!isOverlap)
        {
            //재화 사용
            SpendGold(DataManager.Instance.manualDataList[id - 100].price);

            // openWeaponList 리스트에 추가
            purchaseWeaponList.datas.Add(
                new WeaponData()
                {
                    weaponID = id,
                    baseLevel = 0
                });

            // 리스트 오름차순 정렬
            purchaseWeaponList.datas.Sort((x, y) => x.weaponID.CompareTo(y.weaponID));

            foreach (WeaponData data in purchaseWeaponList.datas)
            {
                if (data.weaponID == id)
                {
                    // 딕셔너리에 추가
                    weaponDatas.Add(id, data);
                }
            }

            //무기 데이터 저장
            SaveWeaponData();
        }
    }
    public void UnLockWeaponData(int id)
    {
        // 중복확인
        if(!unlockWeaponList.Contains(id))
        {
            // 무기 추가
            unlockWeaponList.Add(id);
            unlockWeaponList.Sort();

            unlockData.waeaponIDs.Add(id);
            unlockData.waeaponIDs.Sort();

            //해금데이터 저장
            SaveUnlockData();
        }
        else
        {
            Debug.Log("unlockWeaponList 중복");
        }
    }


    public CharacterData SetCharacterData()
    {
        if (curCharacterData == null) curCharacterData = DataManager.Instance.characterDataList[0];
        return curCharacterData;
    }
    public void GetCharacterData(int characterID)
    {
        if(DataManager.Instance != null)
        {
            foreach (CharacterData CData in DataManager.Instance.characterDataList)
            {
                if (CData.ID == characterID)
                {
                    curCharacterData = CData;
                }
            }
        }
        if (curCharacterData == null) Debug.Log($"CharacterData Load 실패");

        playerData.characterID = characterID;

        SaveData();
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
            foreach (int characterID in purchaseCharacterList.characterIDs)
            {
                if (DataManager.Instance != null)
                {
                    characterDataDic.TryAdd(characterID, DataManager.Instance.characterDataList[characterID]);
                }
                else Debug.Log($"characterDataDic Load Fail");
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
    public void BuyCharacter(int id)
    {
        //중복 확인
        if (!purchaseCharacterList.characterIDs.Contains(id))
        {
            //재화 사용
            SpendGold(DataManager.Instance.characterDataList[id].price);

            // characterIDs 리스트에 추가
            purchaseCharacterList.characterIDs.Add(id);
            // 리스트 오름차순 정렬
            purchaseCharacterList.characterIDs.Sort();

            // 딕셔너리에 추가
            if (!characterDataDic.ContainsKey(id))
            {
                characterDataDic.Add(id, DataManager.Instance.characterDataList[id]);
            }

            //캐릭터 데이터 저장
            SaveCharacterData();
        }
        else
        {
            Debug.Log("purchaseCharacterList 중복");
        }
    }
    public void UnLockCharacterData(int id)
    {
        //중복 확인
        if (!unlockCharacterList.Contains(id))
        {
            // 캐릭터 추가
            unlockCharacterList.Add(id);
            unlockCharacterList.Sort();

            unlockData.characterIDs.Add(id);
            unlockData.characterIDs.Sort();

            // 해금 데이터 저장
            SaveUnlockData();
        }
        else
        {
            Debug.Log("unlockCharacterList 중복");
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


using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<CharacterData> characterDataList = new();

    // weaponID로 Prefab,Data 찾기
    public List<GameObject> manualPrefabList = new();
    public List<ManualWeaponData> manualDataList = new();

    // weaponID - 100으로 Prefab,Data 찾기
    public List<GameObject> autoPrefabList = new();
    public List<AutoWeaponData> autoDataList = new();

    public List<SlotData> allSelecSlotDatas = new List<SlotData>();

    #region PlayerStatTable
    // key:playerLevel / value:LevelUpCost
    public Dictionary<int, int> levelUpCostDic
    = new Dictionary<int, int>()
    {
        {0, 50},
        {1, 100},
        {2, 200},
        {3, 300},
        {4, 400},
        {5, 500},
        {6, 600},
        {7, 700},
        {8, 800},
        {9, 900},
        {10, 1000},
        {11, 1100},
        {12, 1200},
        {13, 1300},
        {14, 1400},
        {15, 1500},
        {16, 1600},
        {17, 1700},
        {18, 1800},
        {19, 1900},
        {20, 2000},
        {21, 2100},
        {22, 2200},
        {23, 2300},
        {24, 2400},
        {25, 2500},
        {26, 2600},
        {27, 2700},
        {28, 2800},
        {29, 2900},
        {30, 3000},
        {31, 3100},
        {32, 3200},
        {33, 3300},
        {34, 3400},
        {35, 3500},
        {36, 3600},
        {37, 3700},
        {38, 3800},
        {39, 3900},
        {40, 4000},
        {41, 4100},
        {42, 4200},
        {43, 4300},
        {44, 4400},
        {45, 4500},
        {46, 4600},
        {47, 4700},
        {48, 4800},
        {49, 4900}
    };
    // key:statLevel / value:movaSpeed
    public Dictionary<int, float> moveSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 6f},
        {1, 6.1f},
        {2, 6.2f},
        {3, 6.3f},
        {4, 6.4f},
        {5, 6.5f},
        {6, 6.6f},
        {7, 6.7f},
        {8, 6.8f},
        {9, 6.9f},
        {10, 7f},
        {11, 7.1f},
        {12, 7.2f},
        {13, 7.3f},
        {14, 7.4f},
        {15, 7.5f}
    };
    // key:statLevel / value:attackPower
    public Dictionary<int, float> attackPowerDic
    = new Dictionary<int, float>()
    {
        {0, 10f},
        {1, 10f},
        {2, 20f},
        {3, 30f},
        {4, 40f},
        {5, 50f},
        {6, 60f},
        {7, 70f},
        {8, 80f},
        {9, 90f},
        {10, 100f},
        {11, 110f},
        {12, 120f},
        {13, 130f},
        {14, 140f},
        {15, 150f}
    };
    // key:statLevel / value:attackSpeed
    public Dictionary<int, float> attackSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 10f},
        {1, 10.1f},
        {2, 10.2f},
        {3, 10.3f},
        {4, 10.4f},
        {5, 10.5f},
        {6, 10.6f},
        {7, 10.7f},
        {8, 10.8f},
        {9, 10.9f},
        {10, 11f},
        {11, 11.1f},
        {12, 11.2f},
        {13, 11.3f},
        {14, 11.4f},
        {15, 11.5f}
    };

    // key:playerLevel / value:MaxExp
    public Dictionary<int, float> maxExpDic
    = new Dictionary<int, float>()
    {
        {0, 50},
        {1, 100},
        {2, 200},
        {3, 300},
        {4, 400},
        {5, 500},
        {6, 600},
        {7, 700},
        {8, 800},
        {9, 900},
        {10, 1000},
        {11, 1100},
        {12, 1200},
        {13, 1300},
        {14, 1400},
        {15, 1500},
        {16, 1600},
        {17, 1700},
        {18, 1800},
        {19, 1900},
        {20, 2000},
        {21, 2100},
        {22, 2200},
        {23, 2300},
        {24, 2400},
        {25, 2500},
        {26, 2600},
        {27, 2700},
        {28, 2800},
        {29, 2900},
        {30, 3000},
        {31, 3100},
        {32, 3200},
        {33, 3300},
        {34, 3400},
        {35, 3500},
        {36, 3600},
        {37, 3700},
        {38, 3800},
        {39, 3900},
        {40, 4000},
        {41, 4100},
        {42, 4200},
        {43, 4300},
        {44, 4400},
        {45, 4500},
        {46, 4600},
        {47, 4700},
        {48, 4800},
        {49, 4900},
        {50, 5000}
    };
    #endregion

    #region SelecStatTable
    public Dictionary<int, float> selecMoveSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };

    public Dictionary<int, float> selecAttackPowerDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };

    public Dictionary<int, float> selecAttckSpeedDic
    = new Dictionary<int, float>()
    {
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.2f},
        {4, 1.4f},
        {5, 1.5f},
    };
    #endregion

    private void Awake()
    {
        Instance = this;
    }
}
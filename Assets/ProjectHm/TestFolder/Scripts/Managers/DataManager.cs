using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<CharacterData> characterDataList = new();

    // weaponID -100으로 Prefab,Data 찾기
    public List<GameObject> manualPrefabList = new();
    public List<ManualWeaponData> manualDataList = new();

    // weaponID -200으로 Prefab,Data 찾기
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

    // key:statLevel / value:attackPower
    public Dictionary<int, float> attackPowerDic
    = new Dictionary<int, float>()
    {
        {0, 5f},
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
        {0, 1f},
        {1, 1.1f},
        {2, 1.2f},
        {3, 1.3f},
        {4, 1.4f},
        {5, 1.5f},
        {6, 1.6f},
        {7, 1.7f},
        {8, 1.8f},
        {9, 1.9f},
        {10, 2f},
        {11, 2.1f},
        {12, 2.2f},
        {13, 2.3f},
        {14, 2.4f},
        {15, 2.5f}
    };
    // key:statLevel / value:[moveSpeed, jumpPower]
    public Dictionary<int, float[]> movePowerDic
    = new Dictionary<int, float[]>()
    {
        {0, new float[] { 6f, 50f } },
        {1, new float[] { 6.1f, 50.1f }},
        {2, new float[] { 6.2f, 50.2f }},
        {3, new float[] { 6.3f, 50.3f }},
        {4, new float[] { 6.4f, 50.4f }},
        {5, new float[] { 6.5f, 50.5f }},
        {6, new float[] { 6.6f, 50.6f }},
        {7, new float[] { 6.7f, 50.7f }},
        {8, new float[] { 6.8f, 50.8f }},
        {9, new float[] { 6.9f, 50.9f }},
        {10, new float[] { 7.0f, 51.0f }},
        {11, new float[] { 7.1f, 51.1f }},
        {12, new float[] { 7.2f, 51.2f }},
        {13, new float[] { 7.3f, 51.3f }},
        {14, new float[] { 7.4f, 51.4f }},
        {15, new float[] { 7.5f, 51.5f }}
    };
    // key:statLevel / value:[dashPower, superJumpPower]  dashPower는 dashDistance의미
    public Dictionary<int, float[]> actPowerDic
    = new Dictionary<int, float[]>()
    {
        {0, new float[] { 4f, 100f } },
        {1, new float[] { 4.1f, 100.1f }},
        {2, new float[] { 4.2f, 100.2f }},
        {3, new float[] { 4.3f, 100.3f }},
        {4, new float[] { 4.4f, 100.4f }},
        {5, new float[] { 4.5f, 100.5f }},
        {6, new float[] { 4.6f, 100.6f }},
        {7, new float[] { 4.7f, 100.7f }},
        {8, new float[] { 4.8f, 100.8f }},
        {9, new float[] { 4.9f, 100.9f }},
        {10, new float[] { 5.0f, 101.0f }},
        {11, new float[] { 5.1f, 101.1f }},
        {12, new float[] { 5.2f, 101.2f }},
        {13, new float[] { 5.3f, 101.3f }},
        {14, new float[] { 5.4f, 101.4f }},
        {15, new float[] { 5.5f, 101.5f }}
    };
    // key:statLevel / value:masteryStat
    public Dictionary<int, int> masteryStatDic
    = new Dictionary<int, int>()
    {   { 0, 1},
        { 1, 2},
        { 2, 3},
        { 3, 4},
        { 4, 5},
        { 5, 6},
        { 6, 7},
        { 7, 8},
        { 8, 9},
        { 9, 10},
        { 10, 11},
        { 11, 12},
        { 12, 13},
        { 13, 14},
        { 14, 15},
        { 15, 16}
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

    // key:statLevel / value:[moveSpeed, jumpPower]
    public Dictionary<int, float[]> selecMovePowerDic
    = new Dictionary<int, float[]>()
    {
        {0, new float[] { 1f, 1f } },
        {1, new float[] { 1.1f, 1.1f }},
        {2, new float[] { 1.2f, 1.2f }},
        {3, new float[] { 1.3f, 1.3f }},
        {4, new float[] { 1.4f, 1.4f }},
        {5, new float[] { 1.5f, 1.5f }}
    };

    // key:statLevel / value:[dashPower, superJumpPower]  dashPower는 dashDistance의미
    public Dictionary<int, float[]> selecActPowerDic
    = new Dictionary<int, float[]>()
    {
        {0, new float[] { 1f, 1f } },
        {1, new float[] { 1.1f, 1.1f }},
        {2, new float[] { 1.2f, 1.2f }},
        {3, new float[] { 1.3f, 1.3f }},
        {4, new float[] { 1.4f, 1.4f }},
        {5, new float[] { 1.5f, 1.5f }}
    };

    public Dictionary<int, int> selecMasteryStatDic
    = new Dictionary<int, int>()
    {
        {0, 1},
        {1, 2},
        {2, 3},
        {3, 4},
        {4, 5},
        {5, 6},
    };
    #endregion

    private void Awake()
    {
        Instance = this;
    }
}
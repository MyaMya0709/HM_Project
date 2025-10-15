using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Monster")]
public class EnemyData : ScriptableObject
{
    public int enemyID;
    public string enemyName;
    public EnemyMoveType MoveType;           // 이동 방식에 따른 분류
    public GameObject enemyPrefab;           // 분류에 따라 해당 프리펩을 붙혀줄 것
    public float maxHealth;                  // 체력
    public float moveSpeed;                  // 이동 속도
    public float attackPower;                // 데미지
    public float dropExpAmount;              // 경험지
    public float dropGoldChance;             // 골드 드롭 확률
    public float dropGoldAmount;             // 골드
    public float dropSkillChance;            // 스킬 드롭 확률
    public List<int> dropSkillIDList;        // 스킬 리스트
    public List<ItemData> dropItemList;      // 아이템 리스트
}
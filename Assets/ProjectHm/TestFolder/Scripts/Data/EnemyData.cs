using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Monster")]
public class EnemyData : ScriptableObject
{
    public int enemyID;
    public string enemyName;
    public EnemyMoveType MoveType;      // ���� �̵� ���
    public GameObject enemyPrefab;      // ���� �̵� ��Ŀ� ���� �ش� �������� ������ ��
    public float maxHealth;
    public float moveSpeed;
    public float attackPower;
    public float dropExpAmount;
    public float dropGoldChance;
    public float dropGoldAmount;
    public float dropSkillChance;
    public List<int> dropSkillIDList;
    public List<ItemData> dropItemList;
    // �ʿ� �� �ִϸ��̼� � �߰�
}
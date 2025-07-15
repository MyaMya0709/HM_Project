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
    public List<ItemData> dropItemList;
    // �ʿ� �� �ִϸ��̼� � �߰�
}
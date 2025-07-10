using UnityEngine;

[CreateAssetMenu(menuName = "Data/Monster")]
public class EnemyData : ScriptableObject
{
    public int enmeyID;
    public string enemyName;
    public EnemyMoveType MoveType;      // ���� �̵� ���
    public GameObject enemyPrefab;      // ���� �̵� ��Ŀ� ���� �ش� �������� ������ ��
    public float maxHealth;
    public float moveSpeed;
    public float attackPower;
    // �ʿ� �� �ִϸ��̼� � �߰�
}

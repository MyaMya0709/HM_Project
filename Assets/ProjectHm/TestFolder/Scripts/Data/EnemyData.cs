using UnityEngine;

[CreateAssetMenu(menuName = "Data/Monster")]
public class EnemyData : ScriptableObject
{
    public int enmeyID;
    public string enemyName;
    public EnemyMoveType MoveType;      // 적의 이동 방식
    public GameObject enemyPrefab;      // 적의 이동 방식에 따라 해당 프리펩을 붙혀줄 것
    public float maxHealth;
    public float moveSpeed;
    public float attackPower;
    // 필요 시 애니메이션 등도 추가
}

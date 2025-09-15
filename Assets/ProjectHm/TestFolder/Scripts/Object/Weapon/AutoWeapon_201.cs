using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoWeapon_201 : IAutoWeapon
{
    public Transform initPoint;
    public GameObject projectilePrefab;

    private float nextFireTime;
    public float damage;          // 공격 데미지
    public float fireRate;        // 공격 쿨타임
    public float speed;           // 이동 속도
    public float attackRange;     // 최대 거리

    private float timer;
    [SerializeField] private List<Transform> targetsInRange = new List<Transform>();
    private int currentProjectileCount = 1; // 시작 발사체 수
    private int fireIndex = 0; // 순환용 인덱스

    public int projectileUnit;
    public int projectileUnitMax;

    private void OnEnable()
    {
        OnWeaponLevelUp += StatSet;
    }
    private void OnDisable()
    {
        OnWeaponLevelUp -= StatSet;
    }

    protected override void Start()
    {
        base.Start();
        projectileUnit = 0;
        timer = 0;
        StatSet();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && targetsInRange.Count > 0)
        {
            Debug.Log("aaa");
            Attack();
            timer = fireRate;
        }
    }

    public void StatSet()
    {
        damage = selecFloatStat0 * data.masteryStatFloatList0[playerCondition.totalMasteryStat];
        fireRate = selecFloatStat1 * data.masteryStatFloatList1[playerCondition.totalMasteryStat];
        speed = selecFloatStat2 * data.masteryStatFloatList2[playerCondition.totalMasteryStat];
        attackRange = selecFloatStat3 * data.masteryStatFloatList3[playerCondition.totalMasteryStat];
        
        projectileUnitMax = selecIntStat0 * data.masteryStatIntList0[playerCondition.totalMasteryStat];
    }

    public override void Attack()
    {
        for (int i = 0; i < currentProjectileCount; i++)
        {
            if (targetsInRange.Count == 0) break;

            Transform target = targetsInRange[fireIndex % targetsInRange.Count];
            fireIndex++;

            GameObject proj = Instantiate(projectilePrefab, initPoint.position, Quaternion.identity);
            proj.GetComponent<Projectile_201>().SetTarget(target);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !targetsInRange.Contains(other.transform))
        {
            targetsInRange.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
        }
    }

    public void IncreaseProjectileCount()
    {
        currentProjectileCount++;
    }
}

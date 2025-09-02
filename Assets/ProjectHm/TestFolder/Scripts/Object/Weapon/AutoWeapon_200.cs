using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoWeapon_200 : IAutoWeapon
{
    public Transform initPoint;
    public GameObject boomerangPrefab;

    private float nextFireTime;
    public float damage;          // 공격 데미지
    public float fireRate;        // 공격 쿨타임
    public float speed;           // 이동 속도
    public float maxDistance;     // 최대 거리
    public float returnSpeed;     // 복귀 속도

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
        StatSet();
    }

    void Update()
    {
        // 타이머 + 투사체 회수 후 공격
        if (Time.time >= nextFireTime && projectileUnit == 0)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void StatSet()
    {
        damage = selecFloatStat0 * data.masteryStatFloatList0[playerCondition.totalMasteryStat];
        fireRate = selecFloatStat1 * data.masteryStatFloatList1[playerCondition.totalMasteryStat];
        speed = selecFloatStat2 * data.masteryStatFloatList2[playerCondition.totalMasteryStat];
        maxDistance = selecFloatStat3 * data.masteryStatFloatList3[playerCondition.totalMasteryStat];
        returnSpeed = selecFloatStat4 * data.masteryStatFloatList4[playerCondition.totalMasteryStat];
        
        projectileUnitMax = selecIntStat0 * data.masteryStatIntList0[playerCondition.totalMasteryStat];
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine(projectileUnitMax));
    }

    public IEnumerator AttackCoroutine(int projectileUnitMax)
    {
        while (projectileUnit < projectileUnitMax)
        {
            float dirX = playerController.lastLookDirection.x; // 플레이어 바라보는 방향 (1 또는 -1)
            Vector2 dir = new Vector2(dirX, 0f);

            //투사체 생성 및 세팅
            var obj = Instantiate(boomerangPrefab, initPoint.position, Quaternion.identity);
            obj.GetComponent<BoomerangProjectile>().Init(
                dir,
                initPoint,
                data.selecWeaponEffectList[selecWeaponLevel],
                damage,
                speed,
                maxDistance,
                returnSpeed
                );

            //투사체 갯수 체크
            projectileUnit++;

            yield return new WaitForSeconds(0.1f);
        }
    }
}

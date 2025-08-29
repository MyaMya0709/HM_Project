using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon_200 : IAutoWeapon
{
    public int weaponID = 200;

    public Transform attackPoint;
    public GameObject boomerangPrefab;
    public float fireRate = 2f;
    private float nextFireTime;

    protected override void Start()
    {
        base.Start();

    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }
    }

    public override void Attack()
    {
        float dirX = playerController.lastLookDirection.x; // 플레이어 바라보는 방향 (1 또는 -1)
        Vector2 dir = new Vector2(dirX, 0f);

        var obj = Instantiate(boomerangPrefab, attackPoint.position, Quaternion.identity);
        obj.GetComponent<BoomerangProjectile>().Init(dir, playerCondition.transform, data.selecWeaponEffectList[selecWeaponLevel]);
    }
}

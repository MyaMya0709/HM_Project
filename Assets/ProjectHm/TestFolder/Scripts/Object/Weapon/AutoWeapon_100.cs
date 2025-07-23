using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon_100 : IAutoWeapon
{
    public int weaponID = 100;

    public Transform attackPoint;

    protected override void Start()
    {
        base.Start();
        SetStat();
    }
}

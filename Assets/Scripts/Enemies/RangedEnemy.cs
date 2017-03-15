using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase {

    protected override void Start()
    {
        base.Start();
        turnRate = 4;
    }

    public override void Attack()
    {
        Debug.Log("Ranged enemy attack");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATowerAttack 
{
    protected BaseTower tower;
    public ATowerAttack(BaseTower tower)
    {
        this.tower = tower;
    }

    public abstract void Attack(Transform target);
    
}

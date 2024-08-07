using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearyLock : AAttackPriority
{
    public override BaseEnemy GetTarget(BaseTower tower)
    {
        if (tower.targetEnemy != null) return tower.targetEnemy;
        float nearestDistance = Mathf.Infinity;
        BaseEnemy enemyToAttack = null;
        foreach (var enemy in tower.enemiesInRange)
        {
            float distance = Vector3.Distance(enemy.transform.position, tower.transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                enemyToAttack = enemy;
            }
        }
        return enemyToAttack;
    }
}

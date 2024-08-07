using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttackPriority
{
    public abstract BaseEnemy GetTarget(BaseTower tower);
}

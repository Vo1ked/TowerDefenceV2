using UnityEngine;

/// <summary>
/// all attack types
/// </summary>
public enum TowerAttackType //add to this enum all class successor ATowerAttack
{
    StraightShoot,
    AimShoot
}

public abstract class ATowerAttack 
{
    protected BaseTower tower;

    public ATowerAttack() { }

    public ATowerAttack(BaseTower tower)
    {
        this.tower = tower;
    }

    public abstract void Attack(Transform target, Bullet bullet);

    public abstract void OnColliderHit(Collider col);

    public virtual float GetDamage()
    {
        float dice = Random.Range(0, 1f);
        float damage = Random.Range(tower.towerStats.Damage.min, tower.towerStats.Damage.max);
        if (dice <= tower.towerStats.Damage.critMultiplier)
        {
            if (damage == 0 || tower.towerStats.Damage.critMultiplier == 0) return 0;
            return damage * tower.towerStats.Damage.critMultiplier;
        }
        else return damage;
    }


}

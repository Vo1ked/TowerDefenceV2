using System.Collections;
using UnityEngine;

public class AimShoot : ATowerAttack
{
    Transform _target;
    Bullet _bullet;
    bool hit = false;

    public AimShoot(BaseTower tower) : base(tower) { }

    public override void Attack(Transform target, Bullet bullet)
    {
        _bullet = bullet;
        _target = target;
        bullet.StartCoroutine(AimMove());
    }

    IEnumerator AimMove()
    {
        while (!hit)
        {
            Vector3 directionNrl = (_target.position - _bullet.transform.position).normalized;
            _bullet.transform.Translate((directionNrl * tower.towerStats.bulletSpeed) * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    public override void OnColliderHit(Collider col)
    {
        BaseEnemy enemy = col.GetComponent<BaseEnemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(GetDamage());
            _bullet.Disable();
            hit = true;
        }
    }
}

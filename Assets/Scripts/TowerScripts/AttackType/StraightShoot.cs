using System.Collections;
using UnityEngine;

public class StraightShoot : ATowerAttack
{

    Vector3 _direction;
    Bullet _bullet;

    bool _hit = false;
    Coroutine _moveCorutine;
    public StraightShoot() { }

    public StraightShoot(BaseTower tower) : base(tower) { }

    public override void Attack(Transform target, Bullet bullet)
    {
        _direction = (target.position - bullet.transform.position).normalized;
        _bullet = bullet;
       _moveCorutine = _bullet.StartCoroutine(StraughtMove());
    }

    IEnumerator StraughtMove()
    {
        while (!_hit)
        {
            _bullet.transform.Translate((_direction * tower.towerStats.bulletSpeed) * Time.deltaTime);
            yield return null;
        }
    }

    public override void OnColliderHit(Collider col)
    {
        BaseEnemy enemy = col.GetComponentInParent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GetDamage());
            _hit = true;
            _bullet.Disable();
        }

    }
}

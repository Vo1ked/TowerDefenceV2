using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseTower : MonoBehaviour
{
    [SerializeField] Transform _rotateObject;
    [SerializeField] Transform _shootSpawnPoint;
    [SerializeField] Transform _bulletContainer;
    public Vector3 offset;
    public TowerStats towerStats;
    [HideInInspector] public BaseEnemy targetEnemy;
    [HideInInspector] public List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
    float _timeToShoot = 0;
    float _enemyCheckDelay = 0.25f;
    float _turnSpeed = 15;
    Coroutine _searchCorutine;
    Coroutine _rotateCorutine;
    Coroutine _shootCorutine;
    AAttackPriority _priority = new NearyLock();
    int counter = 0;
    System.Type _attackTypeContainer;
    System.Type attackType
    {
        get
        {
            if (_attackTypeContainer == null)
            {
                string towerAttackType = towerStats.AttackType.ToString();
                _attackTypeContainer = System.Type.GetType(towerAttackType);
            }
            return _attackTypeContainer;
        }
    }
    private ATowerAttack _towerAttack
    {
        get
        {
            object[] tower = new object[] { this };
            return (ATowerAttack)System.Activator.CreateInstance(attackType, tower);
        }
    }

    Queue<Bullet> _pool = new Queue<Bullet>();
    int _bulletCounter;

    // Start is called before the first frame update
    void Start()
    {
        _searchCorutine = StartCoroutine(FindEnemiesInRange(_enemyCheckDelay));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FindEnemy()
    {

    }

    IEnumerator FindEnemiesInRange(float checkDelay)
    {
        if (targetEnemy != null && Vector3.Distance(targetEnemy.transform.position, transform.position) > towerStats.attackRange)
        {
            targetEnemy = null;
        }
        if (WaveSpawner.enemyList.Count > 0)
        {
            enemiesInRange = WaveSpawner.enemyList.FindAll(x => Vector3.Distance(x.transform.position, transform.position) <= towerStats.attackRange);
        }
        targetEnemy = _priority.GetTarget(this);
        if (_rotateCorutine != null)
        {
            StopCoroutine(_rotateCorutine);
            _rotateCorutine = null;
        }
        _rotateCorutine = StartCoroutine(RotateObject());
        if (_shootCorutine != null)
        {
            StopCoroutine(_shootCorutine);
            _shootCorutine = null;
        }
        _shootCorutine = StartCoroutine(TryShoot());
        yield return new WaitForSeconds(checkDelay);
        _searchCorutine = null;
        _searchCorutine = StartCoroutine(FindEnemiesInRange(checkDelay));
    }

    IEnumerator TryShoot()
    {
        yield return new WaitWhile(() => _timeToShoot > 0);
        yield return new WaitWhile(() => targetEnemy == null);


        Bullet bullet = GetBullet();
        bullet.Init(_towerAttack, targetEnemy.transform, _shootSpawnPoint.transform.position, _pool);
        _timeToShoot = towerStats.attackSpeed;
        StartCoroutine(ShootDelayTimer());

    }

    Bullet GetBullet()
    {
        if (_pool.Count < 1)
        {
            IncreesePool();
        }
        return _pool.Dequeue();
    }

    void IncreesePool()
    {
        int startBulletCount = 5;
        int bulletToAdd = 0;
        if (_bulletCounter == 0)
        {
            bulletToAdd = startBulletCount;
        }
        else
        {
            bulletToAdd = Mathf.FloorToInt(_bulletCounter * 0.2f);
        }
        _bulletCounter += bulletToAdd;
        for (int i = 0; i < bulletToAdd; i++)
        {
            GameObject bulletObj = Instantiate(towerStats.bulletPrefab, _shootSpawnPoint.transform.position, towerStats.bulletPrefab.transform.rotation, _bulletContainer);
            bulletObj.name = "bullet " + counter;
            bulletObj.SetActive(false);
            counter++;
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            _pool.Enqueue(bullet);
        }
    }

    IEnumerator ShootDelayTimer()
    {
        if (_timeToShoot < 0.000001f)
        {
            _timeToShoot = 0;
            yield break;
        }
        yield return null;
        _timeToShoot -= Time.deltaTime;
        StartCoroutine(ShootDelayTimer());
    }


    IEnumerator RotateObject(System.Action OnComlete = null)
    {
        if (targetEnemy == null) yield break;
        Vector3 dir = targetEnemy.transform.position - _rotateObject.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        while (Quaternion.Angle(rotation, _rotateObject.rotation) > 5)
        {
            Vector3 eulerAngle = Quaternion.Lerp(_rotateObject.rotation, rotation, Time.deltaTime * _turnSpeed).eulerAngles;
            _rotateObject.eulerAngles = new Vector3(0, eulerAngle.y, 0);
            yield return null;
        }
        OnComlete?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerStats.attackRange);
    }
}

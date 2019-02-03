using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseTower : MonoBehaviour
{
    [SerializeField] Transform _rotateObject;
    [SerializeField] Transform _shootSpawnPoint;
    public Vector3 offset;
    [SerializeField] TowerStats _towerStats;
    [HideInInspector] public BaseEnemy targetEnemy;
    [HideInInspector] public List<BaseEnemy> enemiesInRange;
    float _timeToShoot = 0;
    float _enemyCheckDelay = 0.25f;
    float _turnSpeed = 5;
    Coroutine _searchCorutine;
    Coroutine _rotateCorutine;
    Coroutine _shootCorutine;
    AAttackPriority _priority = new NearyLock();
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
        if (targetEnemy != null && Vector3.Distance(targetEnemy.transform.position, transform.position) > _towerStats.attackRange)
        {
            targetEnemy = null;
        }
        if (WaveSpawner.enemyList.Count > 0)
        {
            enemiesInRange = WaveSpawner.enemyList.FindAll(x => Vector3.Distance(x.transform.position, transform.position) <= _towerStats.attackRange);
        }
        targetEnemy = _priority.GetTarget(this);
        if (_rotateCorutine != null)
        {
            StopCoroutine(_rotateCorutine);
            _rotateCorutine = null;
        }
        _rotateCorutine = StartCoroutine(RotateObject());
        _shootCorutine =  StartCoroutine(TryShoot());
        yield return new WaitForSeconds(checkDelay);
        _searchCorutine = null;
        _searchCorutine = StartCoroutine(FindEnemiesInRange(checkDelay));
    }

    IEnumerator TryShoot()
    {
        yield return new WaitWhile(() => _timeToShoot > 0);
        yield return new WaitWhile(() => targetEnemy == null);


        GameObject bullet = Instantiate(_towerStats.bulletPrefab, this.transform);
        _timeToShoot = _towerStats.attackSpeed;
        StartCoroutine(ShootDelay());

    }

    IEnumerator ShootDelay()
    {
        if (_timeToShoot < 0.000001f)
        {
            _timeToShoot = 0;
            yield return new WaitUntil(() => _timeToShoot > 0);
        }
        yield return null;
        _timeToShoot -= Time.deltaTime;
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
        Gizmos.DrawWireSphere(transform.position, _towerStats.attackRange);
    }
}

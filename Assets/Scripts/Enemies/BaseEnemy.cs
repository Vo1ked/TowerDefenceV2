using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{

    [Inject] SignalBus _signalBus;
    [Inject] CoroutineController _coroutineController;

    [SerializeField] EnemyStats stats;
    public float Heals { get; private set; }
    public int id { get; set; }
    Coroutine _moveCorutine;


    protected NavMeshAgent meshAgent;
    protected int _waypointCounter;

    public void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        _signalBus.Subscribe<PauseSignal>(IsPaused);
    }

    private void IsPaused(PauseSignal pause)
    {
        meshAgent.isStopped = pause.pause;
    }

    public virtual void Init()
    {
        Heals = stats.heals;
        meshAgent.speed = stats.speed;
        meshAgent.enabled = true;
        meshAgent.Warp(EnemyPath.Waypoints[_waypointCounter].position);
        _moveCorutine = _coroutineController.StartManagedCoroutine(SetWaypoint());
    }

    public virtual void Move()
    {
        meshAgent.SetDestination(EnemyPath.Waypoints[_waypointCounter].position);
    }

    public virtual IEnumerator SetWaypoint()
    {
        _waypointCounter++;
        if (_waypointCounter >= EnemyPath.Waypoints.Count)
        {
            UnitFinishPath();
            yield break;
        }
        Move();
        yield return new WaitUntil(DistanceCheck);
        _moveCorutine = null;
        _moveCorutine = _coroutineController.StartManagedCoroutine(SetWaypoint());
    }
    
    public void TakeDamage(float damage)
    {
        Heals -= damage;
        if (Heals < 0) UnitDie();
    }

    public float GiveGold()
    {
        return stats.gold;
    }

    public float Damage()
    {
        return stats.damage;
    }

    bool DistanceCheck()
    {
        return Vector3.Distance(transform.position, EnemyPath.Waypoints[_waypointCounter].position) < 2f;
    }



    void UnitDie()
    {
        _signalBus.Fire(new EnemyDieSignal(this));
        DestroyUnit();
    }

    void UnitFinishPath()
    {
        _signalBus.Fire(new EnemyFinishPathSignal(this));
        DestroyUnit();
    }

    private void DestroyUnit()
    {
        if (_moveCorutine != null)
        {
            _coroutineController.StopManagedCoroutine(_moveCorutine);
        }
        _signalBus.TryUnsubscribe<PauseSignal>(IsPaused);
        Destroy(gameObject);
    }
}

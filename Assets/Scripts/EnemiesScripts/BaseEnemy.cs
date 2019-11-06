using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{

    [Inject] SignalBus _signalBus;
    [SerializeField] EnemyStats stats;
    public float Heals { get; private set; }
    public int id { get; set; }
    Coroutine _moveCorutine;


    protected NavMeshAgent meshAgent;
    protected int _waypointCounter;

    public void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Init()
    {
        Heals = stats.heals;
        meshAgent.speed = stats.speed;
        meshAgent.enabled = true;
        meshAgent.Warp(EnemyPath.Waypoints[_waypointCounter].position);
        _moveCorutine = StartCoroutine(SetWaypoint());
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
        _moveCorutine = StartCoroutine(SetWaypoint());
    }

    bool DistanceCheck()
    {
        return Vector3.Distance(transform.position, EnemyPath.Waypoints[_waypointCounter].position) < 2f;
    }

    public void TakeDamage(float damage)
    {
        Heals = Heals - damage;
        if (Heals < 0) UnitDie();
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
            StopCoroutine(_moveCorutine);
        }
        Destroy(gameObject);
    }
}

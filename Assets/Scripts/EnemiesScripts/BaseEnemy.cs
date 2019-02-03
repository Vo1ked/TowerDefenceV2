using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{

    [Inject] SignalBus _signalBus;
    [SerializeField] EnemyStats stats;
    int _id;
    public int id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    protected NavMeshAgent meshAgent;
    protected int _waypointCounter;

    public void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Init()
    {
        meshAgent.speed = stats.speed;
        meshAgent.enabled = true;
        meshAgent.Warp(EnemyPath.Waypoints[_waypointCounter].position);
        StartCoroutine(SetWaypoint());
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
            Destroy(gameObject);
            _signalBus.Fire(new EnemyDieSignal(this));
            yield break;
        }
        Move();
        yield return new WaitUntil(DistanceCheck);
        StartCoroutine(SetWaypoint());
    }

    bool DistanceCheck()
    {
        return Vector3.Distance(transform.position, EnemyPath.Waypoints[_waypointCounter].position) < 2f;
    }

}

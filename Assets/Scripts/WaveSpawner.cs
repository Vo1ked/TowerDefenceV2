using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Zenject;

public class WaveSpawner : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [Inject] SignalBus _signalBus;

    [SerializeField] List<Wave> waves;
    private int _currentWaveIndex = 0;
    private int CurrentWaveIndex
    {
        get => _currentWaveIndex;
        set
        {
            _currentWaveIndex = value;
            _signalBus.Fire(new WaveChangedSignal( _currentWaveIndex));
        }
    }
    [SerializeField] Transform _enemiesContainer;
    public static List<BaseEnemy> enemyList { get; private set; } = new List<BaseEnemy>();
    [ReadOnly] Wave _currentWave;
    int _unitsToSpawn;
    int enemyId = 0;
    System.Action OnWaveComplete;
    
    void Start()
    {
        _signalBus.Subscribe<EnemyDieSignal>(x => RemoveEnemyFromList(x.enemy));
        _signalBus.Subscribe<EnemyFinishPathSignal>(x => RemoveEnemyFromList(x.enemy));
        Init();
    }

    void Init()
    {
        OnWaveComplete += SpawnNextWave;
        CurrentWaveIndex = 0;
        StartWave(waves[CurrentWaveIndex]);
    }

    void RemoveEnemyFromList(BaseEnemy enemy)
    {
        BaseEnemy curEnemy = enemyList.Find(x => x.id == enemy.id);
        enemyList.Remove(curEnemy);
    }

    void StartWave(Wave currentWave)
    {
        _unitsToSpawn = 0;
        foreach (var item in currentWave.UnitsInWave)
        {
            _unitsToSpawn += item.EnemiesCount;
            item.EnemiesLeft = item.EnemiesCount;
        }
        _currentWave = currentWave;
        StartCoroutine(SpawnWave(_currentWave, OnWaveComplete));
    }

    void SpawnNextWave()
    {
        StartCoroutine(BetweenWaveDelay(NextWave));
    }
    void NextWave()
    {
        CurrentWaveIndex++;
        if (CurrentWaveIndex >= waves.Count)
        {
            return;
        }
        StartWave(waves[CurrentWaveIndex]);
    }

    IEnumerator BetweenWaveDelay(System.Action OnComplete = null)
    {
        CustomYieldInstruction waitWhile = new WaitWhile(() => enemyList.Count > 0);
        yield return waitWhile;
        OnComplete?.Invoke();
    }

    IEnumerator SpawnWave(Wave currentWawe, System.Action OnComplete = null)
    {
        YieldInstruction wait = new WaitForSeconds(currentWawe.BetweenUnitsDelay);
        while (_unitsToSpawn > 0)
        {
            Units unit = currentWawe.UnitsInWave[Random.Range(0, currentWawe.UnitsInWave.Count)];
            if (unit.EnemiesLeft < 1) continue;

            BaseEnemy enemy = _diContainer.InstantiatePrefab(unit.EnemyType.prefab, _enemiesContainer).GetComponent<BaseEnemy>();
            yield return null;
            if (enemy)
            {
                enemy.id = enemyId;
                enemyId++;
                _unitsToSpawn--;
                unit.EnemiesLeft--;
                enemyList.Add(enemy);
            }
            yield return wait;
        }
        OnComplete?.Invoke();
    }
    
}

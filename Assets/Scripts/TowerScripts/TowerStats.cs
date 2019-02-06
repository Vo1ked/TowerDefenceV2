using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Volk/Tower")]
public class TowerStats : ScriptableObject
{

    public string id;
    public GameObject prefab;
    public GameObject bulletPrefab;
    /// <summary>
    /// damage between min and max where x-minimum ,y-maximum
    /// </summary>
    public Damage Damage;
    public float attackRange;
    /// <summary>
    /// attacks per seconds;
    /// </summary>
    public float attackSpeed;
    public float bulletSpeed;
    /// <summary>
    /// cost to upgrade too this tower or build;
    /// </summary>
    public float cost;
    public float sellCost;
    public List<TowerStats> upgradeTo;
    public TowerAttackType AttackType;

}


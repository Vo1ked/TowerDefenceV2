using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Volk/Enemy")]
public class EnemyStats : ScriptableObject
{
    public string id;
    public GameObject prefab;
    public float heals;
    public float speed;
    public float damage;
    public float gold;

}

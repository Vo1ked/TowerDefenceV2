using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Volk/Wave")]
public class Wave : ScriptableObject
{
  public List<Units> UnitsInWave;
  public float BetweenUnitsDelay = 1; 

}


[System.Serializable]
public class Units
{
    public EnemyStats EnemyType;
    /// <summary>
    /// Set -1 To infinity
    /// </summary>
    public int EnemiesCount;

   [HideInInspector] public int EnemiesLeft;

    
}

using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<Transform> _waypoints = new List<Transform>();
    public static List<Transform> Waypoints
    {
        get
        {
            if (_waypoints.Count < 1)
            {
                Debug.LogError("waypoints is empty");
            }
            return _waypoints;
        }
    }

    public void Awake()
    {
        foreach (Transform item in transform)
        {
            _waypoints.Add(item);
        }

    }
}

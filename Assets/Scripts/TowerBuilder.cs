using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerBuilder : MonoBehaviour
{
    [Inject] SignalBus _signalBus;
    List<TowerStats> _fistTowersToBuild;
    // Start is called before the first frame update
    void Start()
    {
        
    }


}

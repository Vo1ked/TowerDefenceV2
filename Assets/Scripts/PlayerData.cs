using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerData : MonoBehaviour
{
    static PlayerData _playerdata;
    public static PlayerData Instance
    {
        get
        {
            if (_playerdata == null)
            {
                _playerdata = new PlayerData();                
            }
            return _playerdata;
        }
    }
    #region TempData
    public ReactiveProperty<int> currentWave = new ReactiveProperty<int>(1);
    public ReactiveProperty<float> currenGold = new ReactiveProperty<float>(0);
    public ReactiveProperty<int> currentHeals = new ReactiveProperty<int>(0);
    #endregion





}

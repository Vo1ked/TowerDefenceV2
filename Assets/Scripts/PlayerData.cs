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
    public ReactiveProperty<int> currentWave = new ReactiveProperty<int>(1);
    public ReactiveProperty<float> currenGold = new ReactiveProperty<float>(0);
    public ReactiveProperty<int> currentHeals = new ReactiveProperty<int>(0);
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUIController : MonoBehaviour,IUIWindow
{
    [Inject] private SignalBus _signalBus;
    [SerializeField] Button _pauseButton;
    [SerializeField] TextMeshProUGUI _waveCounter;
    [SerializeField] TextMeshProUGUI _healsConter;
    [SerializeField] TextMeshProUGUI _goldConter;
    private float _gold;
    private float _heals;
    public string Name => "GUI";
    

    public void SetWindow(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

    void Start()
    {
        _signalBus.Subscribe<WaveChangedSignal>(SetCurrentWave);
        _signalBus.Subscribe<EnemyFinishPathSignal>(RemoveHeals);
        _signalBus.Subscribe<EnemyDieSignal>(AddGold);
    }
    
    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<WaveChangedSignal>(SetCurrentWave);
        _signalBus.TryUnsubscribe<EnemyFinishPathSignal>(RemoveHeals);
        _signalBus.TryUnsubscribe<EnemyDieSignal>(AddGold);
    }

    private void AddGold(EnemyDieSignal enemy)
    {
        _gold += enemy.enemy.GiveGold();
        _goldConter.text = _gold.ToString("0");
    }

    private void RemoveHeals(EnemyFinishPathSignal enemy)
    {
        _heals -= enemy.enemy.Damage();
        _healsConter.text = _heals.ToString("0");
    }
    
    public void SetCurrentWave(WaveChangedSignal wave)
    {
        _waveCounter.text = wave.wave.ToString();
    }
}

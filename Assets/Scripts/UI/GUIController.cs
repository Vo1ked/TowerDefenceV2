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
        _signalBus.Subscribe<WaveChangedSignal>(x => SetCurrentWave(x.wave));
        _signalBus.Subscribe<EnemyFinishPathSignal>(x => RemoveHeals(x.enemy));
        _signalBus.Subscribe<EnemyDieSignal>(x => AddGold(x.enemy));
    }

    private void AddGold(BaseEnemy enemy)
    {
        _gold += enemy.GiveGold();
        _goldConter.text = _gold.ToString("0");
    }

    private void RemoveHeals(BaseEnemy enemy)
    {
        _heals -= enemy.Damage();
        _healsConter.text = _heals.ToString("0");
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<WaveChangedSignal>(x => SetCurrentWave(x.wave));
        _signalBus.TryUnsubscribe<EnemyFinishPathSignal>(x => RemoveHeals(x.enemy));
        _signalBus.TryUnsubscribe<EnemyDieSignal>(x => AddGold(x.enemy));
    }

    public void SetCurrentWave(int wave)
    {
        _waveCounter.text = wave.ToString();
    }
}

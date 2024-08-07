using System.Collections;
using UnityEngine;
using Zenject;

public class CoroutineController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    private bool _isPause;
    
    public Coroutine StartManagedCoroutine(IEnumerator routine)
    {
        return StartCoroutine(RunManagedCoroutine(routine));
    }

    private void Awake()
    {
        _signalBus.Subscribe<PauseSignal>(SetPause);
    }

    private void SetPause(PauseSignal signal)
    {
        _isPause = signal.pause;
    }

    private IEnumerator RunManagedCoroutine(IEnumerator routine)
    {
        while (routine.MoveNext())
        {
            yield return routine.Current;

            while (_isPause)
            {
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        _signalBus.TryUnsubscribe<PauseSignal>(SetPause);
    }
}


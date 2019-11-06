using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour,IUIWindow
{
    [SerializeField] Button _pauseButton;
    [SerializeField] TextMeshProUGUI _waveCounter;
    [SerializeField] TextMeshProUGUI _healsConter;
    [SerializeField] TextMeshProUGUI _goldConter;

    public string Name => "GUI";

    public static GUIController Instance { get; private set; }

    public void SetWindow(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

    void Awake()
    {
        Instance = this;
    }

    public void SetCurrentWave(int wave)
    {
        _waveCounter.text = wave.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


}

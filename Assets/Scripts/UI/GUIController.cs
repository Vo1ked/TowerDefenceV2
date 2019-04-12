﻿using System.Collections;
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

    public void SetWindow(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


}

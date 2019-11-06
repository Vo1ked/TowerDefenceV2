using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class UIController : MonoBehaviour
{
    [Inject] SignalBus _signalBus;

    List<IUIWindow> _uIWindows;
    public static UIController Instance { get; private set; }

    private void Awake()
    {
        _uIWindows = new List<IUIWindow>();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IUIWindow window;
        DontDestroyOnLoad(this.gameObject);
        foreach (Transform child in transform)
        {

            window = child.GetComponent<IUIWindow>();
            if (window != null)
            {
                _uIWindows.Add(window);
            }
            ShowMainMenu();
        }

    }

    public void HideAllWindow()
    {
        _uIWindows.ForEach(x => x.SetWindow(false));
    }

    public void ShowMainMenu()
    {
        HideAllWindow();
        _uIWindows.Find(x => x.Name == "MainMenu").SetWindow(true);
    }

    public void ShowGui()
    {
        HideAllWindow();
        _uIWindows.Find(x => x.Name == "GUI").SetWindow(true);

    }

}

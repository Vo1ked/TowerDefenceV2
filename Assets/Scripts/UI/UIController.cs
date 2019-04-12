using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{

    List<IUIWindow> _uIWindows;
    private void Awake()
    {
        _uIWindows = new List<IUIWindow>();
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

    void HideAllWindow()
    {
        _uIWindows.ForEach(x => x.SetWindow(false));
    }

    void ShowMainMenu()
    {
        HideAllWindow();
        _uIWindows.Find(x => x.Name == "MainMenu").SetWindow(true);
    }

    void ShowGui()
    {
        HideAllWindow();
        _uIWindows.Find(x => x.Name == "GUI").SetWindow(true);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour, IUIWindow
{

    [SerializeField] Button _start;
    [SerializeField] Button _settings;
    [SerializeField] Button _quit;

    public string Name => "MainMenu";

    private void Start()
    {
        _start.onClick.AddListener(StartGame);
        _settings.onClick.AddListener(OpenSettings);
        _quit.onClick.AddListener(QuitGame);
        
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync("Maze").completed += x => UIController.Instance.ShowGui();
    }

    void OpenSettings()
    {

    }

    void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public void SetWindow(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }
}

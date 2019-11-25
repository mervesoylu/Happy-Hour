using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _controlsButton;
    [SerializeField] Button _creditsButton;

    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _controlsPanel;
    [SerializeField] GameObject _creditsPanel;

    void OnEnable()
    {
        _playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        _exitButton.onClick.AddListener(() => Application.Quit());
        _controlsButton.onClick.AddListener(() =>
        {
            _mainMenuPanel.SetActive(false);
            _controlsPanel.SetActive(true);
        });
        _creditsButton.onClick.AddListener(() =>
        {
            _mainMenuPanel.SetActive(false);
            _creditsPanel.SetActive(true);
        });
    }

    void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public void OnBackButton()
    {
        _mainMenuPanel.SetActive(true);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _controlsButton;
    [SerializeField] Button _creditsButton;

    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _controlsPanel;
    [SerializeField] GameObject _creditsPanel;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_playButton.gameObject);
    }

    void OnEnable()
    {
        _playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        _exitButton.onClick.AddListener(() => Application.Quit());
        _controlsButton.onClick.AddListener(() => {
            _mainMenuPanel.SetActive(false);
            _controlsPanel.SetActive(true);
        });
        _creditsButton.onClick.AddListener(() => {
            _mainMenuPanel.SetActive(false);
            _creditsPanel.SetActive(true);
        });
    }

    void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            if (XCI.GetButtonDown(XboxButton.B, (XboxController)i))
            {
                _mainMenuPanel.SetActive(true);
                _controlsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
            }
        }
    }
}

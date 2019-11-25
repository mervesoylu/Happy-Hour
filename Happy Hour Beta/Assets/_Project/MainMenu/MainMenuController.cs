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

    [SerializeField] LinkedList<Button> _navigationPath = new LinkedList<Button>();
    LinkedListNode<Button> _current;

    void Start()
    {
        // Initialize the navigation path.
        _navigationPath.AddLast(_exitButton);
        _navigationPath.AddLast(_controlsButton);
        _navigationPath.AddLast(_playButton);
        _navigationPath.AddLast(_creditsButton);

        _current = _navigationPath.Find(_playButton);
    }

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

    void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            XboxController current = (XboxController)i;

            if (XCI.GetButtonDown(XboxButton.A, current))
            {
                _current.Value.onClick.Invoke();
            }

            if (XCI.GetButtonDown(XboxButton.B, current))
            {
                _mainMenuPanel.SetActive(true);
                _controlsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                UpdateSelectedButton();
            }

            // DPad.
            if (XCI.GetButtonDown(XboxButton.DPadRight, current))
                Next();

            if (XCI.GetButtonDown(XboxButton.DPadLeft, current))
                Previous();

            // Joystick.
            if (XCI.GetAxisRaw(XboxAxis.LeftStickX, current) > 0f)
            {
                if (Time.time >= _nextTime)
                {
                    Next();
                    _nextTime = Time.time + _interval;
                }
            }

            if (XCI.GetAxisRaw(XboxAxis.LeftStickX, current) < 0f)
            {
                if (Time.time >= _nextTime)
                {
                    Previous();
                    _nextTime = Time.time + _interval;
                }
            }
        }
    }
    private static float _interval = 0.25f;
    private float _nextTime;

    private void Next()
    {
        if (_current.Next != null)
        {
            _current = _current.Next;
            UpdateSelectedButton();
        }
    }

    private void Previous()
    {
        if (_current.Previous != null)
        {
            _current = _current.Previous;
            UpdateSelectedButton();
        }
    }

    private void UpdateSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(_current.Value.gameObject);
    }
}

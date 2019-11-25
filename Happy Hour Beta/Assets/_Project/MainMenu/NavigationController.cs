using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XboxCtrlrInput;

public class NavigationController : MonoBehaviour
{
    [SerializeField] List<Button> _navigationList;
    [SerializeField] int _defaultButton;

    [SerializeField] UnityEvent _onBackButton;

    LinkedList<Button> _navigationLinkedList = new LinkedList<Button>();
    LinkedListNode<Button> _current;

    void Start()
    {
        // Initialize the navigation path.
        _navigationLinkedList = new LinkedList<Button>(_navigationList);
        _current = _navigationLinkedList.Find(_navigationList[_defaultButton]);
        UpdateSelectedButton();
    }

    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.A, XboxController.First))
        {
            _current.Value.onClick.Invoke();
        }

        if (XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            _onBackButton.Invoke();
            UpdateSelectedButton();
        }

        // DPad.
        if (XCI.GetButtonDown(XboxButton.DPadRight, XboxController.First))
            Next();

        if (XCI.GetButtonDown(XboxButton.DPadLeft, XboxController.First))
            Previous();

        // Joystick.
        if (XCI.GetAxisRaw(XboxAxis.LeftStickX, XboxController.First) > 0f)
        {
            if (Time.unscaledTime >= _nextTime)
            {
                Next();
                _nextTime = Time.unscaledTime + _interval;
            }
        }

        if (XCI.GetAxisRaw(XboxAxis.LeftStickX, XboxController.First) < 0f)
        {
            if (Time.unscaledTime >= _nextTime)
            {
                Previous();
                _nextTime = Time.unscaledTime + _interval;
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
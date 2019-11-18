using UnityEngine;
using UnityEngine.UI;

public class ReadyMenuController : MonoBehaviour
{
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _highlightedColor;
    [SerializeField] Image[] _characterFrames;
    [SerializeField] Image[] _characterNames;

    public void Ready(int controllerID)
    {
        if (controllerID < 1 || controllerID > 4)
        {
            Debug.LogError("Invalid controller ID!");
            return;
        }

        _characterFrames[controllerID - 1].color = _highlightedColor;
        _characterNames[controllerID - 1].color = _highlightedColor;
    }

    public void ResetUI()
    {
        for (int i = 0; i < 4; i++)
        {
            _characterFrames[i].color = _defaultColor;
            _characterNames[i].color = _defaultColor;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
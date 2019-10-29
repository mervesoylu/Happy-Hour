using UnityEngine;
using UnityEngine.UI;

public class ReadyMenuController : MonoBehaviour
{
    [SerializeField] Image[] _characterIcons;

    public void Ready(int controllerID)
    {
        if (controllerID < 1 || controllerID > 4)
        {
            Debug.LogError("Invalid controller ID!");
            return;
        }

        _characterIcons[controllerID - 1].color = Color.green;
    }

    public void ResetUI()
    {
        for (int i = 0; i < 4; i++)
        {
            _characterIcons[i].color = Color.white;
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
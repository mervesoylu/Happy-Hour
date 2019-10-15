using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_playButton.gameObject);
    }

    void OnEnable()
    {
        _playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}

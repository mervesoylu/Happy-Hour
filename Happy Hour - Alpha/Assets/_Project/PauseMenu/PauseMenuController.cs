using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Canvas _pauseMenuCanvas;
    
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.Start))
            pauseGame();
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        _pauseMenuCanvas.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
    }

    void resumeGame()
    {
        Time.timeScale = 1;
        _pauseMenuCanvas.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _resumeButton.onClick.AddListener(resumeGame);
        _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        _pauseMenuCanvas.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        _resumeButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
    }
}

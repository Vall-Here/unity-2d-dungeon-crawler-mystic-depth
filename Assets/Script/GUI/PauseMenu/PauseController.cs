using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private UIControl uIControl;
    public Button pauseButton;
    
    public bool isPaused = false;

    void Start()
    {
        uIControl = FindObjectOfType<UIControl>();
        pauseButton.onClick.AddListener(Pause);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !uIControl.IsSettingMenuOpen() )
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseButton.gameObject.SetActive(false);
        uIControl.PauseGame();
    }

    public void Resume()
    {
        pauseButton.gameObject.SetActive(true);
        uIControl.ResumeGame();
    }

    public void OpenSettings()
    {
        uIControl.OpenSettings();
    }

    public void CloseSettings()
    {
        uIControl.CloseSettings();
    }

    public void Restart()
    {
        uIControl.Restart();
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("selectLevelScene");
    }
}

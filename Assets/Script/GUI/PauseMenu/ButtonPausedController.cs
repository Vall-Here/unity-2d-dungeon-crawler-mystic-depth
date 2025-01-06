using UnityEngine;
using UnityEngine.UI;

public class ButtonPausedController : MonoBehaviour
{
    public Button resume;
    public Button restartButton;
    public Button settingButton;
    public Button quitButton;
    [SerializeField] 
    private PauseController pauseController;

    void Start()
    {
        pauseController = FindObjectOfType<PauseController>();

        resume.onClick.AddListener(ToggleResume);
        restartButton.onClick.AddListener(RestartGame);
        settingButton.onClick.AddListener(ToggleSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseController.isPaused)
        {
            ToggleResume();
        }   
    }

    void ToggleResume()
    {
        if (pauseController.isPaused == true)
        {
            pauseController.Resume();
            pauseController.isPaused = true;
        }
        else
        {
            pauseController.Pause();
            pauseController.isPaused = false;
        }
    }

    void ToggleSettings()
    {
        pauseController.OpenSettings();
    }

    void RestartGame()
    {
        pauseController.Restart();
    }

    void QuitGame()
    {
        pauseController.Quit();
    }
}

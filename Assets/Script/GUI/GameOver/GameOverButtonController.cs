using UnityEngine;
using UnityEngine.UI;

public class GameOverButtonController : MonoBehaviour
{
    public Button retryButton;
    public Button quitButton;
    [SerializeField] 
    private GameOverController gameOverController;

    void Start()
    {
        gameOverController = FindObjectOfType<GameOverController>();

        retryButton.onClick.AddListener(ToggleRetry);
        quitButton.onClick.AddListener(QuitGame);
    }

    void ToggleRetry()
    {
        gameOverController.Retry();
    }

    void QuitGame()
    {
        Time.timeScale = 1f;
        gameOverController.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    private UIControl uIControl;

    void Start()
    {
        uIControl = FindObjectOfType<UIControl>();
    }

    public void Retry()
    {
        uIControl.Restart();
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("selectLevelScene");
    }
}

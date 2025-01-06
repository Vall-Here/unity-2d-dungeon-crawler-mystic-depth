using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    private UIControl uIControl;

    void Start()
    {
        uIControl = FindObjectOfType<UIControl>();
    }

    public void Next()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        int nextSceneIndex = currentSceneIndex + 1;

        // Periksa apakah scene berikutnya ada dalam build settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Memuat scene dengan index: " + nextSceneIndex);
        }
        else
        {
            Debug.Log("Tidak ada scene berikutnya di build settings.");
        }
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("selectLevelScene");
    }
}

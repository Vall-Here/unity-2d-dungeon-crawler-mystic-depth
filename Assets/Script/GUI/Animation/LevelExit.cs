using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTrigger : MonoBehaviour
{
    private UIControl uIControl; //willy
    public int levelUnlocked;

    void Start()
    {
        uIControl = FindObjectOfType<UIControl>(); //willy
        levelUnlocked = SceneManager.GetActiveScene().buildIndex ;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("sudah"); 

        if (other.CompareTag("Player")) 
        {
            EndGame();
        }
    }

    private void EndGame()
    {
    
        if (levelUnlocked > PlayerPrefs.GetInt("LevelAt", 0))
        {
            PlayerPrefs.SetInt("LevelAt", levelUnlocked);
            int levelReached = PlayerPrefs.GetInt("LevelAt", 1);
            Debug.Log("Level Reached: " + levelReached);    
        }
        
        uIControl.OpenVictory(); //willy

        // Time.timeScale = 1; 
        // Debug.Log("done");
        // SceneManager.LoadScene("LevelScene");
    }

}

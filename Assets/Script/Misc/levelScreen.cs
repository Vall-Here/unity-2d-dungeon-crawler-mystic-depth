using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject panel; // Referensi ke Panel yang berisi semua level button

    private Button[] levelButton;
    private Image[] levelImage;

    void Start()
    {
        // ResetPlayerPrefs(); 
        // Ambil semua button yang berada di dalam panel
        levelButton = panel.GetComponentsInChildren<Button>();
        levelImage = panel.GetComponentsInChildren<Image>();
        
        int levelReached = PlayerPrefs.GetInt("LevelAt", 1);
        for (int i = 0; i < levelButton.Length; i++) 
        {
            int levelIndex = i + 1; 
            if (levelIndex > levelReached && levelIndex < 16)
            {
                levelButton[i].interactable = false;
                levelImage[i].gameObject.SetActive(true); 
            }
            
            if( levelIndex < 16){
            // listener ke button secara otomatis
            levelButton[i].onClick.AddListener(() => SelectLevel(levelIndex)) ;}
        }
    }

    public void SelectLevel(int levelIndex)
    {
        if (levelIndex >= 1 && levelIndex <= levelButton.Length)
        {
            SceneManager.LoadScene("Level" + levelIndex);
        }
        else
        {
            Debug.LogError("Level index out of range");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("PlayerPrefs has been reset.");
    }
}

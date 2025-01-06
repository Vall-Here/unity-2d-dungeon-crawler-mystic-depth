using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button[] menuButtons;  // Array to hold menu buttons
    private int selectedIndex = 0; // Index of the currently selected button

    void Start()
    {
        UpdateButtonSelection();
    }

    void Update()
    {
        // Navigate down with DownArrow or 'S' key
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            UpdateButtonSelection();
        }
        // Navigate up with UpArrow or 'W' key
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            UpdateButtonSelection();
        }
        // Select option with Enter or 'Space' key
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.Instance.playSFX("button"); // Play SFX for button click
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    void UpdateButtonSelection()
    {
        // Highlight the selected button with light gray color
        for (int i = 0; i < menuButtons.Length; i++)
        {
            var colors = menuButtons[i].colors;
            colors.normalColor = i == selectedIndex ? new Color(0.8f, 0.8f, 0.8f) : Color.white;
            menuButtons[i].colors = colors;
        }
    }

    // Function to load the next scene
    public void Play()
    {
        // audioManager.Instance.playSFX("button"); // Play SFX for play button
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Function to quit the game
    public void Quit()
    {
        // audioManager.Instance.playSFX("button"); // Play SFX for quit button
        Application.Quit();
        Debug.Log("Berhasil");
    }
}

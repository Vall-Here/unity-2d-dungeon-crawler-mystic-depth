using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public List<Button> levelButtons; // Assign your buttons here in the Inspector
    public int columns = 5;           // Number of columns in your grid
    private int currentIndex = 0;

    void Start()
    {
        // Initialize the first button as selected
        UpdateButtonSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Up
        {
            MoveSelection(-columns);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Down
        {
            MoveSelection(columns);
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Left
        {
            if (currentIndex % columns != 0)
                MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            if ((currentIndex + 1) % columns != 0)
                MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // Enter to select
        {
            levelButtons[currentIndex].onClick.Invoke();
        }
    }

    void MoveSelection(int step)
    {
        // Deselect current button
        levelButtons[currentIndex].GetComponent<Outline>().enabled = false;

        // Calculate new index and clamp it within bounds
        currentIndex = Mathf.Clamp(currentIndex + step, 0, levelButtons.Count - 1);

        // Select new button
        UpdateButtonSelection();
    }

    void UpdateButtonSelection()
    {
        // Enable outline or effect to highlight selected button
        levelButtons[currentIndex].GetComponent<Outline>().enabled = true;
    }
}

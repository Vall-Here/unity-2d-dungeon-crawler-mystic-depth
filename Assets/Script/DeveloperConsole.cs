using TMPro;
using UnityEngine;
using System.Collections;

public class DeveloperConsole : MonoBehaviour
{
  
    public GameObject consolePanel; 
    public TMP_InputField inputField; 
    public static string CurrentCommand = ""; 

    public PlayerHealth playerHealth;

    private bool isConsoleVisible = false;

    public GameObject masterKey;
    public GameObject normalKey;

    void Start()
    {
        inputField.onSubmit.AddListener(ProcessCommand);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            ToggleConsole();
        }
    }

    void ToggleConsole()
    {
        isConsoleVisible = !isConsoleVisible;
        consolePanel.SetActive(isConsoleVisible);

        if (isConsoleVisible)
        {
            inputField.ActivateInputField();
        }
    }

    void ProcessCommand(string command)
    {
        CurrentCommand = command;
     
        inputField.text = ""; 
        ExecuteCommand(command);
        ToggleConsole(); 
    }

    void ExecuteCommand(string command)
    {
        if (command == "/gamerule disablehealthdown kjfkawkwasafwakjsdkwakbsdnwa")
        {
            Debug.Log("Player cannot take damage.");
            playerHealth.isNgecit = true;
        }
        else if (command == "/gamerule enablehealthdown")
        {
            Debug.Log("Player can take damage again.");
            playerHealth.isNgecit = false;
        }else if (command == "/gamerule give masterkey")
        {
            Instantiate(masterKey, playerHealth.transform.position, Quaternion.identity);
            Debug.Log("Player has been given the master key.");
        }
        else if (command == "/gamerule give normalkey")
        {
            Instantiate(masterKey, playerHealth.transform.position, Quaternion.identity);
        }else if (command.StartsWith("/gamerule addlvl"))
        {
            string[] commandParts = command.Split(' '); 
            
            if (commandParts.Length == 2 && int.TryParse(commandParts[1], out int levelUnlocked))
            {
                PlayerPrefs.SetInt("LevelAt", levelUnlocked); // Set the level in PlayerPrefs
                Debug.Log("Level set to " + levelUnlocked);  // Log the level that was set
            }
            else
            {
                Debug.LogWarning("Invalid level specified. Usage: /gamerule addlvl <level_number>");
            }
        }

        else 
        {
            Debug.LogWarning("Unknown command: " + command);
        }
    }

    public static string GetCommand()
    {
        return CurrentCommand;
    }
}

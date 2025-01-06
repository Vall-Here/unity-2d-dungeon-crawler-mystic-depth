using UnityEngine.UI;
using UnityEngine;

public class VictoryButtonController : MonoBehaviour
{
    public Button next;
    public Button quitButton;
    [SerializeField] 
    private VictoryController victoryController;

    void Start()
    {
        victoryController = FindObjectOfType<VictoryController>();

        next.onClick.AddListener(ToggleNext);
        quitButton.onClick.AddListener(QuitGame);
    }

    void ToggleNext()
    {
        victoryController.Next();
    }

    void QuitGame()
    {
        victoryController.Quit();
    }
}

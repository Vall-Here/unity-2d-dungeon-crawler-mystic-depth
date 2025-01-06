using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public Transform Canvas;

    [SerializeField] 
    private PauseController pauseController;


    // Tambahkan referensi untuk panel yang sering digunakan
    private GameObject pauseMenuPanel;
    private GameObject settingMenuPanel;
    private GameObject victoryPanel;
    private GameObject gameOverPanel;
    private GameObject slider;
    private GameObject hotbar;
    private GameObject pauseButton;
    private AudioSource audioSource;
    [SerializeField] private AudioClip VictoryClip;
    [SerializeField] private AudioClip DefeatClip;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        pauseController = FindObjectOfType<PauseController>();
        
        // Cari elemen-elemen UI di dalam Canvas
        slider = Canvas.Find("Slider")?.gameObject;
        hotbar = Canvas.Find("Hotbar")?.gameObject;

        pauseMenuPanel = Canvas.Find("Pause menu")?.gameObject;
        settingMenuPanel = Canvas.Find("Setting Menu")?.gameObject;
        victoryPanel = Canvas.Find("Victory")?.gameObject;
        gameOverPanel = Canvas.Find("Game Over")?.gameObject;

        pauseButton = Canvas.Find("Paused Button")?.gameObject;

        // Nonaktifkan panel tertentu saat memulai
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingMenuPanel != null) settingMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        if (pauseController != null)
        {
            pauseController.isPaused = true;
        }
        if (pauseMenuPanel != null)
        {   
            CloseGUI(false);
            pauseMenuPanel.SetActive(true);
        }else
        {
            
        Debug.Log("cekkak");
        }
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
        {
            CloseGUI(true);
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Kembalikan waktu normal
    }

    public void OpenSettings()
    {
        if (settingMenuPanel != null)
        {
            settingMenuPanel.SetActive(true);
        }

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        if (settingMenuPanel != null)
        {
            settingMenuPanel.SetActive(false);
        }

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public bool IsSettingMenuOpen()
    {
        return settingMenuPanel != null && settingMenuPanel.activeInHierarchy;
    }

    public void OpenVictory()
    {
        Time.timeScale = 0f;
        CloseGUI(true);
        victoryPanel.SetActive(true);
        audioSource.PlayOneShot(VictoryClip);
    }

    public void OpenGameOver()
    {
        Time.timeScale = 0f;
        CloseGUI(true);
        gameOverPanel.SetActive(true);
        audioSource.PlayOneShot(DefeatClip);
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        int sceneNow = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNow);
    }
    public void CloseGUI(bool cond)
    {
        foreach (Transform child in Canvas)
        {
        Debug.Log("haloo");
            if (child.name == "Accessories UI") 
            {
                child.gameObject.SetActive(cond);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public Button[] menuButtons;        // Array for menu buttons
    public Slider[] sliders;            // Array for sliders in the menu
    public Slider _musicSlider, _sfxSlider; // Sliders for controlling music and sfx volume

    private int selectedIndex = 0;      // Index of the currently selected UI element
    private bool isSliderSelected = false; // Track if the current selection is a slider

    void Start()
    {
        UpdateSelection();
        _musicSlider.onValueChanged.AddListener(delegate { MusicVolume(); });
        _sfxSlider.onValueChanged.AddListener(delegate { SfxVolume(); });
    }

    void Update()
    {
        // Navigate down with W and S keys
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length + sliders.Length) % (menuButtons.Length + sliders.Length);
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % (menuButtons.Length + sliders.Length);
            UpdateSelection();
        }

        // Adjust slider value with A and D keys if a slider is selected
        if (isSliderSelected)
        {
            if (Input.GetKey(KeyCode.A))
            {
                sliders[selectedIndex - menuButtons.Length].value -= 0.003f; // Adjust slider value
                UpdateVolume();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                sliders[selectedIndex - menuButtons.Length].value += 0.003f; // Adjust slider value
                UpdateVolume();
            }
            
            // Toggle audio on/off with Enter key
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ToggleAudio();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // Select button with Enter
        {
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    void UpdateSelection()
    {
        // Determine if the selected index is a button or a slider
        isSliderSelected = selectedIndex >= menuButtons.Length;

        // Highlight the selected element
        for (int i = 0; i < menuButtons.Length; i++)
        {
            var buttonColors = menuButtons[i].colors;
            buttonColors.normalColor = i == selectedIndex ? new Color(0.8f, 0.8f, 0.8f) : Color.white;  // Light gray for selection
            menuButtons[i].colors = buttonColors;
        }

        for (int i = 0; i < sliders.Length; i++)
        {
            var sliderColors = sliders[i].colors;
            sliderColors.normalColor = (i + menuButtons.Length == selectedIndex) ? Color.gray : Color.white;
            sliders[i].colors = sliderColors;
        }
    }

    void UpdateVolume()
    {
        if (selectedIndex - menuButtons.Length == 0) // Assuming the first slider is for music
        {
            MusicVolume();
        }
        else if (selectedIndex - menuButtons.Length == 1) // Assuming the second slider is for SFX
        {
            SfxVolume();
        }
    }

    void ToggleAudio()
    {
        // Check if the selected slider is _musicSlider or _sfxSlider
        if (selectedIndex - menuButtons.Length == 0) // Music slider selected
        {
            ToggleMusic();
        }
        else if (selectedIndex - menuButtons.Length == 1) // SFX slider selected
        {
            ToggleSfx();
        }
    }

    public void MusicVolume()
    {
        audioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SfxVolume()
    {
        audioManager.Instance.SfxVolume(_sfxSlider.value);
    }

    public void ToggleMusic()
    {
        audioManager.Instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        audioManager.Instance.ToggleSFX();
    }
}

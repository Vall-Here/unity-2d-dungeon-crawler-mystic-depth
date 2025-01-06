using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsController : MonoBehaviour
{
    private int fps;
    public TMPro.TextMeshProUGUI fpsText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    // Update is called once per frame
    void GetFPS()
    {
        fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime); // Using Mathf.RoundToInt for rounding
        fpsText.text = "FPS: " +  fps.ToString(); // Removed format specifier for integer display
    }
}
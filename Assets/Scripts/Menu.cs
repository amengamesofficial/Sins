using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{ 
    public Image buttonFPS60;
    public Image buttonFPS30;

    private void Start()
    {
        if (Application.targetFrameRate == 30) buttonFPS30.color = new Color(1, 0.5f, 0);
        else buttonFPS60.color = new Color(1, 0.5f, 0);
    }

    public void Remuse()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Set30FPS()
    {
        
        PlayerPrefs.SetInt("FPS", 30);
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        buttonFPS60.color = Color.gray;
        buttonFPS30.color = new Color(1, 0.5f, 0);

    }

    public void Set60FPS()
    {
        PlayerPrefs.SetInt("FPS", 60);
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        buttonFPS30.color = Color.gray;
        buttonFPS60.color = new Color(1, 0.5f, 0);
    }

    

    
}

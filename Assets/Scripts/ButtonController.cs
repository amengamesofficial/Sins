using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    
    
    public void StartGame()
    {
        SaveManager.instance.ClearSaveData();
        GameManager.instance.ClearSaveData();
        SceneManager.LoadScene("Intro");

    }
    public void LoadGame()
    {
        
         if (PlayerPrefs.HasKey("SavedScene"))
            {
                SaveManager.instance.LoadGame();
            }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

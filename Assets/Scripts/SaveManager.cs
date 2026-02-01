using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != "Menu" && currentScene != "LevelChooser")
        {
            PlayerPrefs.SetString("SavedScene", currentScene);
            PlayerPrefs.Save();
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneName = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
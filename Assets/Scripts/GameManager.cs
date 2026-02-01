using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool eyedooropen;
    public bool footdooropen;
    public bool handdooropen;
    public bool bellydooropen;

    public bool eyeanimation;
    public bool handanimation;
    public bool footanimation;
    public bool bellyanimation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("SavedScene"))
            {
                LoadGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("eyedooropen", eyedooropen ? 1 : 0);
        PlayerPrefs.SetInt("footdooropen", footdooropen ? 1 : 0);
        PlayerPrefs.SetInt("handdooropen", handdooropen ? 1 : 0);
        PlayerPrefs.SetInt("bellydooropen", bellydooropen ? 1 : 0);

        PlayerPrefs.SetInt("eyeanimation", eyeanimation ? 1 : 0);
        PlayerPrefs.SetInt("handanimation", handanimation ? 1 : 0);
        PlayerPrefs.SetInt("footanimation", footanimation ? 1 : 0);
        PlayerPrefs.SetInt("bellyanimation", bellyanimation ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        eyedooropen = PlayerPrefs.GetInt("eyedooropen") == 1;
        footdooropen = PlayerPrefs.GetInt("footdooropen") == 1;
        handdooropen = PlayerPrefs.GetInt("handdooropen") == 1;
        bellydooropen = PlayerPrefs.GetInt("bellydooropen") == 1;

        eyeanimation = PlayerPrefs.GetInt("eyeanimation") == 1;
        handanimation = PlayerPrefs.GetInt("handanimation") == 1;
        footanimation = PlayerPrefs.GetInt("footanimation") == 1;
        bellyanimation = PlayerPrefs.GetInt("bellyanimation") == 1;

    }

    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
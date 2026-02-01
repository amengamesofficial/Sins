using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAnimation : MonoBehaviour
{

    public void EnableScene()

    {
        GetComponent<Animator>().enabled = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("EyeScene");
    }

    public void DisableScene()

    {

        GetComponent<Animator>().enabled = false;
    }
    
}

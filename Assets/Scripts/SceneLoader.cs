using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    
    public string sceneName;
    public GameObject sceneFinilizer;


    private void OnEnable()
    {
        StartCoroutine(LoadScene());
    }


    public  IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }
}

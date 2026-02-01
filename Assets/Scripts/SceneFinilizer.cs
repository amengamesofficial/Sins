using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneFinilizer : MonoBehaviour
{

    public string nextScene;
    public GameObject playerUI;
    private void OnEnable()
    {

        StartCoroutine(AfterEnabling());

    }

    IEnumerator AfterEnabling()
    {

        if(playerUI != null)
        {
            playerUI.transform.GetChild(0).GetComponent<Joystick>().OnPointerUp(null);
            playerUI.SetActive(false);
            
        }
              

        while(AudioListener.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            AudioListener.volume -= 0.1f;
        }

        if(nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }



    }
}

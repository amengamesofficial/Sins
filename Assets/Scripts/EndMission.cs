using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMission : MonoBehaviour
{
    public GameObject Player;
    public GameObject sceneFinilizer;
    

    void Start()
    {
        StartCoroutine(WaitUntilNotBusy());
    }

    IEnumerator WaitUntilNotBusy()
    {
        yield return new WaitUntil(() => !Player.GetComponent<AngleMovment>().isBusy);
        yield return new WaitForSeconds(1f);
        sceneFinilizer.SetActive(true);
      
    }

}
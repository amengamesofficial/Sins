using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public GameObject SceneFinilizer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.instance == null)
            return;

        if (PlayerMovement.instance.isDead) StartCoroutine(DelaySceneReloader());
    }

    IEnumerator DelaySceneReloader()
    {
        yield return new WaitForSeconds(2f);
        SceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

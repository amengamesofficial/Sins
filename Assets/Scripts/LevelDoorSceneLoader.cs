using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorSceneLoader : MonoBehaviour
{
    public GameObject sceneFinilizer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneFinilizer.SetActive(true);
        }
    }
}

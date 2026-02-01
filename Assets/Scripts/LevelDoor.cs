using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public GameObject crashAudio;
    public GameObject cameraShake;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Open", true);
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Open", false);
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            crashAudio.SetActive(true);
            cameraShake.SetActive(true);
        }
    }
}

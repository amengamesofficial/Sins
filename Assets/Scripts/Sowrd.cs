using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sowrd : MonoBehaviour
{
    public GameObject sowrd;
    public GameObject crashSound;
    public GameObject cameraShake;
    public GameObject interactable;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            sowrd.GetComponent<Rigidbody2D>().gravityScale = 5;
            Debug.Log(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "Sowrd" && collision.gameObject.tag == "Ground")
        {
            crashSound.SetActive(true);
            cameraShake.SetActive(true);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        interactable.SetActive(true);
    }
}

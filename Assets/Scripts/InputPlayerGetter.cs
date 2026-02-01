using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerGetter : MonoBehaviour
{
    GameObject temp;
    public AudioClip clip;
    public AudioSource audioSource;

    private void OnEnable()
    {
        temp = GameObject.FindWithTag("PlayerUI");
        temp.SetActive(false);  
    }

    private void OnDisable()
    {
        temp.SetActive(true);
    }

    public void PlayTypeSound()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

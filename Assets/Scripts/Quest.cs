using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public RTLTextMeshPro quest;
    public string questText;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource.clip = audioClips[Random.RandomRange(0, audioClips.Length)];
        audioSource.Play(); 
        quest.text = questText;
    }
}

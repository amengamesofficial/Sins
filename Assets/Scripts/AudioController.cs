using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProximitySound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // اطمینان از سه‌بعدی بودن صدا
        audioSource.spatialBlend = 1.0f;
        audioSource.loop = true;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}

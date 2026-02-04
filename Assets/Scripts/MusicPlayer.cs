using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musics;

    private int currentIndex = 0;

    void Start()
    {
        if (musics.Length > 0)
        {
            PlayMusic(currentIndex);
        }
    }

    void Update()
    {
        // ???? ????? ???? ??? ???? ??? ???
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            NextMusic();
        }
    }

    void PlayMusic(int index)
    {
        audioSource.clip = musics[index];
        audioSource.Play();
    }

    void NextMusic()
    {
        currentIndex++;

        if (currentIndex >= musics.Length)
        {
            currentIndex = 0; // ??? ?????? ?? ??? ???? ???? ??? ?? ?? ????? ???
        }

        PlayMusic(currentIndex);
    }
}

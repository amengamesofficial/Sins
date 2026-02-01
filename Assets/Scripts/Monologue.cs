using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{
    public string[] text;
    RTLTextMeshPro textBox;


    public AudioClip textTypeSound;
    AudioSource audioSource;
    public Button nextLineButton;

    public float waitingForNextLetter = 0.1f;
    public float waitingForNextLine = 1f;

    bool goNextLine = false;
    bool canGoNextLine = false;
    public bool dialogueFinished = false;


    bool firstTime = true;

    private void Awake()
    {
        if (FindObjectOfType<EventSystem>() == null)
            Debug.LogError("EventSystem not found !");
    }

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        nextLineButton.onClick.AddListener(GoNextLine);
        textBox = GetComponent<RTLTextMeshPro>();
        
        
    }


    public void StartDialog()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        
        for (int i = 0; i < text.Length; i++)
        {
            //Audio
            audioSource.clip = textTypeSound;
            audioSource.time = 0f;
            audioSource.Play();

            //Text Show
            string temp = "";
            canGoNextLine = false;
            for (int j = 0; j < text[i].Length; j++)
            {
                temp += text[i][j];
                textBox.text = temp;
                yield return new WaitForSeconds(waitingForNextLetter);

            }

            //Audio
            audioSource.Pause();

            //TextShow
            canGoNextLine=true;

            //yield return new WaitForSeconds(waitingForNextLine);
            yield return new WaitUntil(() => goNextLine);
            goNextLine = false;
        }

        dialogueFinished = true;
    }

    public void GoNextLine()
    {

        if(firstTime)
        {
            firstTime = false;
            StartCoroutine(ShowText());
        }

        if(canGoNextLine)
        {
            goNextLine = true;
        }
        
    }

    


}

using RTLTMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class StaticSpeak : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip textTypeSound;
    public RTLTextMeshPro textBox;
    public Dictionary<Sprite, string>[] dialogue;
    public Image character;
    public float waitingForNextLetter = 0.1f;
    public float waitingForNextLine = 1f;

    bool skipLine = false;
    bool skipWholeDialogue = false;
    bool onTyping = false;
    private bool dialogueIsFinished = false;
    public bool isPlayerExist = true;
    public GameObject sceneLoader;



    private void OnEnable()
    {
        
  


        StartCoroutine(StartDialogue());
        
        
    }

    private void OnDisable()
    {

        StopCoroutine(StartDialogue());
    }


    public IEnumerator StartDialogue()
    {



        audioSource.clip = textTypeSound;
        for (int i = 0; i < dialogue.Length; i++)
        {

            

            audioSource.time = 0f;
            audioSource.Play();
            character.sprite = dialogue[i].x;


            string temp = "";
            onTyping = true;
            for (int j = 0; j < dialogue[i].y.Length; j++)
            {
                temp += dialogue[i].y[j];
                textBox.text = temp;
                yield return new WaitForSeconds(waitingForNextLetter);

            }


            onTyping = false;
            audioSource.Pause();

            yield return new WaitUntil(() => skipLine);
            skipLine = false;

        }



        if (sceneLoader != null)
        {
            sceneLoader.SetActive(true);
        }

        gameObject.SetActive(false);
        


    }

    public void ChangeSkipValue()
    {
        if(!onTyping)
            skipLine = true;
    }

}

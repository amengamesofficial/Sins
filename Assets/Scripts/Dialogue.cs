using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
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
    public SceneLoader sceneLoader;

    public GameObject playerUI;
    GameObject player;
    public bool isAngel = true;

    
    public MonoBehaviour[] scripts;
    public GameObject[] objects;



    private void OnEnable()
    {
        if(scripts != null)
        {
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }

        if (objects != null)
        {
            foreach (GameObject objectt in objects)
            {
                objectt.SetActive(false);
            }
        }


        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player");
            player.GetComponent<Animator>().SetInteger("MoveMode", 0);
            if (isAngel)
                AngleMovment.instance.isBusy = true;

        }

        if (GameObject.FindWithTag("PlayerUI") != null)
        {
            playerUI = GameObject.FindWithTag("PlayerUI");
            playerUI.SetActive(false);
            playerUI.transform.GetChild(0).GetComponent<Joystick>().OnPointerUp(null);
        }



        StartCoroutine(StartDialogue());


    }

    private void OnDisable()
    {

        playerUI.SetActive(true);

        StopCoroutine(StartDialogue());
        if(AngleMovment.instance != null)
            AngleMovment.instance.isBusy = false;

        if(scripts != null)
        {
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }

        if (objects != null)
        {
            foreach (GameObject objectt in objects)
            {
                objectt.SetActive(true);
            }
        }

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



        if (playerUI != null)
            playerUI.SetActive(true);


        gameObject.SetActive(false);

        if (sceneLoader != null)
        {
            sceneLoader.StartCoroutine(sceneLoader.LoadScene());
        }



    }

    public void ChangeSkipValue()
    {
        if (!onTyping)
            skipLine = true;
    }

}

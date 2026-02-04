using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManagement : MonoBehaviour
{
    public static DoorsManagement instance;
    public GameObject eyedoor;
    public GameObject foot;
    public GameObject handdoor;
    public GameObject bellydoor;
    public GameObject sceneFinilizer;
    public GameObject Player;
    public GameObject FirstDialog;
    public GameObject AfterEyeDialog;
    public GameObject AfterHandDialog;
    public GameObject AfterFootDialog;
    public GameObject AfterBellyDialog;


    public bool eyedooropen = false;
    public bool footdooropen = false;
    public bool handdooropen = false;
    public bool canpressbutton = false;
    public bool bellydooropen = false;


    public float timeLoadNextScene = 3f;

    void Awake()
    {
        instance = this;
    }

    
    void Update()
    {
        if (eyedooropen) eyedoor.GetComponent<Animator>().enabled = true;
        if (handdooropen) handdoor.GetComponent<Animator>().enabled = true;
        if (footdooropen) foot.GetComponent<Animator>().enabled = true;
        if (bellydooropen) bellydoor.GetComponent<Animator>().enabled = true;
        

        eyedooropen = GameManager.instance.eyedooropen;
        handdooropen = GameManager.instance.handdooropen;
        footdooropen = GameManager.instance.footdooropen;
        bellydooropen = GameManager.instance.bellydooropen;

        MainSceneManager();

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "eyedoor")
        {
            canpressbutton = true;
        }

        if (other.gameObject.tag == "handdoor" && eyedooropen == true)
        {
            canpressbutton = true;
        }

        if (other.gameObject.tag == "footdoor" && handdooropen == true)
        {
            canpressbutton = true;
        }

        if (other.gameObject.tag == "bellydoor" && footdooropen == true)
        {
            canpressbutton = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "eyedoor")
        {
            canpressbutton = false;
        }

        if (other.gameObject.tag == "handdoor")
        {
            canpressbutton = false;
        }

        if (other.gameObject.tag == "footdoor")
        {
            canpressbutton = false;
        }

        if (other.gameObject.tag == "bellydoor")
        {
            canpressbutton = false;
        }
    }


    public void EyeDoorButton()
    {
        if (canpressbutton && !eyedooropen)
        {
            GameManager.instance.eyedooropen = true;
            Player.GetComponent<AngleMovment>().PickUp();
            eyedoor.GetComponent<Animator>().enabled = true;
            eyedoor.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(LoadEyeSceneDelay());
            
            
        }
    }

    IEnumerator LoadEyeSceneDelay()
    {

        canpressbutton = false;
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(timeLoadNextScene);
        SceneManager.LoadScene("EyeScene");

    }


    public void HandDoorButton()
    {
        if (canpressbutton && eyedooropen && !handdooropen)
        {
            Player.GetComponent<AngleMovment>().PickUp();
            handdoor.GetComponent<Animator>().enabled = true;
            handdoor.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(LoadHandSceneDelay());
        }
    }


    IEnumerator LoadHandSceneDelay()
    {
        GameManager.instance.handdooropen = true;
        canpressbutton = false;
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(timeLoadNextScene);
        SceneManager.LoadScene("KindScene");

    }


    public void FootDoorButton()
    {
        if (canpressbutton && handdooropen && !footdooropen)
        {
            Player.GetComponent<AngleMovment>().PickUp();
            foot.GetComponent<Animator>().enabled = true;
            foot.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(LoadFootSceneDelay());
        }
    }

    IEnumerator LoadFootSceneDelay()
    {
        GameManager.instance.footdooropen = true;
        canpressbutton = false;
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(timeLoadNextScene);
        SceneManager.LoadScene("PuzzleScene");

    }


    public void BellyDoorButton()
    {
        if (canpressbutton && footdooropen && !bellydooropen)
        {
            Player.GetComponent<AngleMovment>().PickUp();
            bellydoor.GetComponent<Animator>().enabled = true;
            bellydoor.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(LoadBellySceneDelay());
        }
    }


    IEnumerator LoadBellySceneDelay()
    {
        GameManager.instance.bellydooropen = true;
        canpressbutton = false;
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(timeLoadNextScene);
        SceneManager.LoadScene("LabratoryScene");

    }
    

    void MainSceneManager()
    {
        if (!eyedooropen) FirstDialog.SetActive(true);
        else FirstDialog.SetActive(false);
        if (eyedooropen && !handdooropen) AfterEyeDialog.SetActive(true);
        if (handdooropen && !footdooropen) AfterHandDialog.SetActive(true);
        if (footdooropen && !bellydooropen) AfterFootDialog.SetActive(true);
        if (bellydooropen) AfterBellyDialog.SetActive(true);
        
        
    }

  
  
}

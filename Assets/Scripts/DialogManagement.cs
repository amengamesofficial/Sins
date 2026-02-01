using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManagement : MonoBehaviour
{
    public GameObject Speak;
    public GameObject Quest;
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            Speak.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(ActivateQuest());

        }


    }
    IEnumerator ActivateQuest()
    {
        while (Speak.activeSelf) yield return null;
        if(Quest != null)
            Quest.SetActive(true);

    }

}

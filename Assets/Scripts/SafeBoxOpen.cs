using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBoxOpen : MonoBehaviour
{

    public static SafeBoxOpen instance;
    public GameObject Hammer;

    void Awake()
    {
        instance = this;
    }

    public void OpenSafeBox()
    {
        
        gameObject.GetComponent<Animator>().enabled = true;
        
    }

    public void HammerActive()
    {
        Hammer.SetActive(true);
    }



    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPoorDialog : MonoBehaviour
{
    // Start is called before the first frame update
  GameObject Speak;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfterGetMoney()
    {
        Speak.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
    }
}

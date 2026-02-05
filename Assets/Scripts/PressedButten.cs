using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButten : MonoBehaviour
{
    public butten[] buttens;

    private void OnMouseDown()
    {
       foreach(butten but in buttens)
        {
            but.obj.SetActive(but.activation);
        }
        
    }
}

[System.Serializable]
public class butten
{
    public GameObject obj;
    public bool activation;
}


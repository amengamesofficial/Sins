using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPad : MonoBehaviour
{
    public GameObject SafeBoxPad;
    public GameObject AngleUi;

    private void OnMouseDown()
    {
        SafeBoxPad.SetActive(true);
        AngleUi.SetActive(false);
    }

    }




    


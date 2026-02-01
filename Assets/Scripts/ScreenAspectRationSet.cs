using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAspectRationSet : MonoBehaviour
{

    private void Start()
    {
        
        if (Screen.height < 1100)
        {
            transform.localScale = new Vector2(1920,1080);
        }
        else
        {
            transform.localScale = new Vector2(1440, 900);
        }
    }
}

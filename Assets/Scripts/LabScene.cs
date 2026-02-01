using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabScene : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject labPuzzleSign;
    public GameObject guide;
    public int objectChooser;
    
    


    private void OnMouseDown()
    {

        switch(objectChooser)
        {
            case 0:
                puzzle.SetActive(true);
                labPuzzleSign.SetActive(false);
                GameObject.FindWithTag("PlayerUI").SetActive(false);
                break;

            case 1:
                guide.SetActive(true);
                GameObject.FindWithTag("PlayerUI").SetActive(false);
                break;

            

        }

            

        
    }


}

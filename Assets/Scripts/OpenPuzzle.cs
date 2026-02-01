using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour
{
    public GameObject PuzzleGame;
    public GameObject player;
    public GameObject playerUI;


    private void OnMouseDown()
    {
        PuzzleGame.SetActive(true);


            player.GetComponent<Animator>().SetInteger("MoveMode", 0);
            //AngleMovment.instance.isBusy = true;

            playerUI.SetActive(false);
            playerUI.transform.GetChild(0).GetComponent<Joystick>().OnPointerUp(null);
        

    }
    
         
        
}

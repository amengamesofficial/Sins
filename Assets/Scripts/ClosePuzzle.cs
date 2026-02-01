using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClosePuzzle : MonoBehaviour
{

    public GameObject PuzzleGame;
    public GameObject playerUI;

    public void OnMouseDown()
    {
        PuzzleGame.SetActive(false);
        playerUI.SetActive(true);
    }

}

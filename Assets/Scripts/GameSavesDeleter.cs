using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavesDeleter : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerPrefs.DeleteAll();
    }

}

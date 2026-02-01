using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int fPS;
    void Start()
    {
        Application.targetFrameRate = fPS;
    }

    
}

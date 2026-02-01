using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public static PlayerHealthBar instance;
    public Slider Health;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        Health.minValue = 0;
        Health.maxValue = 20;

        
    }

    // Update is called once per frame
}

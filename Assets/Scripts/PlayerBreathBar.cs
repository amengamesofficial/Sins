using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBreathBar : MonoBehaviour
{
    public static PlayerBreathBar instance;
    public Slider BreathBar;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        BreathBar = GetComponent<Slider>();
        BreathBar.minValue = 0;
        BreathBar.maxValue = 20;
    }

    // Update is called once per frame

}

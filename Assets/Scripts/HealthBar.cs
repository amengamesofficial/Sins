using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public static EnemyHealthBar instance;
    public Slider Health;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (gameObject.tag == "Hizi")
        {
            Health.minValue = 0;
            Health.maxValue = 20;
        }
        else if (gameObject.tag == "Gheibat")
        {
            Health.minValue = 0;
            Health.maxValue = 30;
        }

        else if(gameObject.tag == "BadSelf")
        {
            Health.minValue = 0;
            Health.maxValue = 40;
        }
        
    }



}

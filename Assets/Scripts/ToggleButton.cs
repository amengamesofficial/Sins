using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject obj;
    public void ClickButton()
    {
        obj.SetActive(!obj.activeSelf);
    }

}

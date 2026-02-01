using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYAxisActivator : MonoBehaviour
{
    public bool isYAxisActivator;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
            if (isYAxisActivator)
                Camera.main.GetComponent<FollowPlayer>().yAxisMove = true;
            else
                Camera.main.GetComponent<FollowPlayer>().yAxisMove = false;

    }
}

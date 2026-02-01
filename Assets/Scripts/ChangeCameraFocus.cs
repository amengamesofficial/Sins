using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeCameraFocus : MonoBehaviour
{
    public GameObject target;
    public float cameraMoveSpeed = 2f;
    public float distanceThershold = 0.05f;
    float cameraSize;
    bool focus = false;

  

    private void LateUpdate()
    {
        if (focus)
        {
            if ((Vector3.Distance(Camera.main.transform.position, target.transform.position) > distanceThershold))
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position, Time.deltaTime * cameraMoveSpeed) + new Vector3(0, 0, -10);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cameraSize = Camera.main.GetComponent<Camera>().orthographicSize;
            Camera.main.GetComponent<Camera>().orthographicSize -= 3;
            Camera.main.GetComponent<FollowPlayer>().enabled = false;
            focus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Camera.main.GetComponent<Camera>().orthographicSize = cameraSize;
            Camera.main.GetComponent<FollowPlayer>().enabled = true;
            focus = false;
        }
    }

}

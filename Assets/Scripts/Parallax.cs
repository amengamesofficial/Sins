using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;  // سرعت افکت پارالاکس
    private Transform cam;             // مرجع دوربین
    private Vector3 lastCamPos;        // آخرین موقعیت دوربین

    void Start()
    {
        cam = Camera.main.transform;   // گرفتن مرجع دوربین اصلی
        lastCamPos = cam.position;     // ذخیره موقعیت اولیه دوربین
    }


    private void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCamPos; // مقدار تغییر موقعیت دوربین
        transform.position += new Vector3(deltaMovement.x * parallaxSpeed, 0, 0); // اعمال افکت پارالاکس فقط در جهت X
        lastCamPos = cam.position;
    }




}

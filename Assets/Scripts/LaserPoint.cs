using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private LineRenderer line;
    public Transform firePoint;
    public float maxDistance = 20f;
    public LayerMask collisionMask;

    public bool isFiring = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Update()
    {
        if (isFiring)
        {
            ShootLaser();
        }
    }

   void ShootLaser()
{
    line.enabled = true;
    Vector3 startPos = firePoint.position;
    line.SetPosition(0, startPos);

   
    Vector2 laserDirection = firePoint.right; 
    

    if (transform.lossyScale.x < 0)
    {
        laserDirection = -firePoint.right;
    }


    RaycastHit2D hit = Physics2D.Raycast(startPos, laserDirection, maxDistance, collisionMask);

    if (hit.collider != null)
    {
        line.SetPosition(1, hit.point);

        if (hit.collider.CompareTag("Player"))
        {
            PlayerHealthBar.instance.Health.value -= 5f * Time.deltaTime;
        }
    }
    else
    {
     
        line.SetPosition(1, startPos + (Vector3)laserDirection * maxDistance);
    }
}

    public void StopLaser() => line.enabled = false;


}
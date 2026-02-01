using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    public bool yAxisMove = false;

    public float transitionSpeed = 5f;
    private float transitionThreshold = 0.05f;
    float yAxis;

    public float normalPhoneCameraSize = 7.5f;
    public float tabletCameraSize = 12f;
    public float widePhoneCameraSize = 6f;

    Vector3 targetPosition;

    public float normalPhoneOffset;
    public float tabletPhoneOffset;
    public float widePhoneOffset;

    // ===== Look Ahead (Always On) =====
    public float lookAheadDistance = 2.5f;
    public float lookAheadSmooth = 5f;
    float currentLookAhead;
    // =================================

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        SetOrthographicCameraSize();
        normalPhoneOffset = transform.position.y;
    }

    private void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        // ===== Detect Facing Direction (Flip Safe) =====
        float facingDirection = Mathf.Sign(player.localScale.x);
        if (facingDirection == 0) facingDirection = 1;
        // ==============================================

        float targetLookAhead = facingDirection * lookAheadDistance;

        currentLookAhead = Mathf.Lerp(
            currentLookAhead,
            targetLookAhead,
            Time.deltaTime * lookAheadSmooth
        );

        if (yAxisMove)
        {
            targetPosition = new Vector3(
                player.position.x + currentLookAhead,
                player.position.y,
                -10
            );
        }
        else
        {
            targetPosition = new Vector3(
                player.position.x + currentLookAhead,
                yAxis,
                -10
            );
        }

        if (Vector3.Distance(transform.position, targetPosition) > transitionThreshold)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * transitionSpeed
            );
        }
    }

    void SetOrthographicCameraSize()
    {
        Camera cam = GetComponent<Camera>();

        if (Screen.height < 1000)
        {
            cam.orthographicSize = widePhoneCameraSize;
            yAxis = widePhoneOffset;
        }
        else if (Screen.height > 1300)
        {
            cam.orthographicSize = tabletCameraSize;
            yAxis = tabletPhoneOffset;
        }
        else
        {
            cam.orthographicSize = normalPhoneCameraSize;
            yAxis = normalPhoneOffset;
        }
    }
}

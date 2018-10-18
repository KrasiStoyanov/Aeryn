using System.Collections;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 velocity;

    public float delay = 0.15f;
    public Transform targetToFollow;

    private void Start()
    {
        mainCamera = this.GetComponent<Camera>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetToFollow)
        {
            Vector3 positionOfTarget = mainCamera.WorldToViewportPoint(targetToFollow.position);
            Vector3 delayedPosition = targetToFollow.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, positionOfTarget.z));
            Vector3 finalPosition = transform.position + delayedPosition;

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, delay);
        }
    }
}
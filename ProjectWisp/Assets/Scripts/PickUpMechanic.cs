using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PickUpMechanic : MonoBehaviour
{
    [Tooltip("The picking source GameObject.")]
    public GameObject pickingSource;

    private GameObject[] pickUpObjects;

    public GameObject pickedUpObject;
    private float pickedUpObjectInitialGravityScale;

    private const float speedForCatchingUpToSource = 50f;
    private const int minDistanceBetweenSourceAndTarget = 15;
    private Vector3 maxDistanceBetweenSourceAndTarget;

    private int cooldownBetweenStartOfPickUpAndEnd = 2;

    // Use this for initialization
    void Start()
    {
        pickUpObjects = GameObject.FindGameObjectsWithTag("PickUp");
        maxDistanceBetweenSourceAndTarget = new Vector3(5f, 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUpObject)
        {
            PickUpObject();
            if (Input.GetKeyDown(KeyCode.E) && cooldownBetweenStartOfPickUpAndEnd <= 0)
            {
                ReleasePickedUpObject();

                cooldownBetweenStartOfPickUpAndEnd = 0;
            }

            cooldownBetweenStartOfPickUpAndEnd--;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpDetection();
        }
    }

    private void PickUpDetection()
    {
        Vector3 positionOfSource = pickingSource.transform.position;
        foreach (GameObject pickUpObject in pickUpObjects)
        {
            Vector3 positionOfTarget = pickUpObject.transform.position;
            float distanceBetweenSourceAndTarget = Vector3.Distance(positionOfSource, positionOfTarget);

            if (distanceBetweenSourceAndTarget <= minDistanceBetweenSourceAndTarget)
            {
                Rigidbody2D pickedUpObjectRigidBody = pickUpObject.GetComponent<Rigidbody2D>();

                pickedUpObjectInitialGravityScale = pickedUpObjectRigidBody.gravityScale;
                pickedUpObjectRigidBody.gravityScale = 0;

                pickedUpObject = pickUpObject;
            }
        }
    }

    private void PickUpObject()
    {
        Vector3 positionOfSource = pickingSource.transform.position;
        Vector3 positionOfTarget = pickedUpObject.transform.position;

        positionOfTarget = Vector3.MoveTowards(positionOfTarget, positionOfSource - maxDistanceBetweenSourceAndTarget, Time.deltaTime * speedForCatchingUpToSource);

        pickedUpObject.transform.position = positionOfTarget;
    }

    public void ReleasePickedUpObject()
    {
        pickedUpObject.GetComponent<Rigidbody2D>().gravityScale = pickedUpObjectInitialGravityScale;

        pickedUpObject = null;
    }
}

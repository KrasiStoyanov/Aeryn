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
        maxDistanceBetweenSourceAndTarget = new Vector3(3.0f, 3.0f, 3.0f);
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

                break;
            }
        }
    }

    private void PickUpObject()
    {
        // Enable the relative joint of the source
        RelativeJoint2D sourceRelativeJoint = pickingSource.GetComponent<RelativeJoint2D>();
        sourceRelativeJoint.connectedBody = pickedUpObject.GetComponent<Rigidbody2D>();

        sourceRelativeJoint.enabled = true;

        RepositionPickedUpObject();
    }

    private void RepositionPickedUpObject()
    {
        Vector3 positionOfSource = pickingSource.transform.position;

        // Dimensions of the source object
        Renderer dimensionsOfSource = pickingSource.GetComponent<Renderer>();

        float widthOfSource = dimensionsOfSource.bounds.size.x;
        float heightOfSource = dimensionsOfSource.bounds.size.y;

        // Dimensions of the picked up object
        Renderer dimensionsOfPickedUpObject = pickedUpObject.GetComponent<Renderer>();

        float widthOfPickedUpObject = dimensionsOfPickedUpObject.bounds.size.x;
        float heightOfPickedUpObject = dimensionsOfPickedUpObject.bounds.size.y;

        // The target position for the picked up object to move to
        Vector3 targetPosition = new Vector3();

        targetPosition.x = positionOfSource.x + ((widthOfSource + widthOfPickedUpObject) / 2) + maxDistanceBetweenSourceAndTarget.x;
        targetPosition.y = positionOfSource.y;
        targetPosition.z = positionOfSource.z;

        pickedUpObject.transform.position = Vector3.MoveTowards(pickedUpObject.transform.position, targetPosition, Time.deltaTime * 35.0f);
    }

    public void ReleasePickedUpObject()
    {
        pickedUpObject.GetComponent<Rigidbody2D>().gravityScale = pickedUpObjectInitialGravityScale;

        // Disable the relative joint of the source
        RelativeJoint2D sourceRelativeJoint = pickingSource.GetComponent<RelativeJoint2D>();

        sourceRelativeJoint.enabled = false;
        sourceRelativeJoint.connectedBody = null;

        pickedUpObject = null;
    }
}

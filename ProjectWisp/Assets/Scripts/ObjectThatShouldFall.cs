using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectThatShouldFall : MonoBehaviour
{
    [SerializeField]
    private float minFallingSpeedDetection = 0.5f;

    public bool isOnGround = false;

    public void ObjectIsFalling()
    {
        if (gameObject.tag != "FallingObject" && gameObject.tag != "PickUp")
        {
            gameObject.tag = "FallingObject";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D pickUpObjectRigidBody = GetComponent<Rigidbody2D>();

        float fallingSpeed = pickUpObjectRigidBody.velocity.x;
        if (fallingSpeed < minFallingSpeedDetection && collision.collider.tag == "Map" && gameObject.tag != "PickUp")
        {
            isOnGround = true;
            gameObject.tag = "PickUp";
        }
    }
}

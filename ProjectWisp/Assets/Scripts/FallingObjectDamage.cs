using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FallingObjectDamage : MonoBehaviour
{
    private HealthMechanic healthManager;

    private GameObject fallenObject;

    [SerializeField]
    private float minFallingSpeedDetection = 0.5f;

    void Start()
    {
        healthManager = GetComponent<HealthMechanic>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "PickUp")
        {
            if (collision.collider.tag == "FallingObject" && collision.collider.gameObject != fallenObject)
            {
                ObjectThatShouldFall fallingObject = collision.collider.gameObject.GetComponent<ObjectThatShouldFall>();
                if (!fallingObject.isOnGround)
                {
                    GameObject pickUpObject = collision.collider.gameObject;
                    Rigidbody2D pickUpObjectRigidBody = pickUpObject.GetComponent<Rigidbody2D>();

                    float fallingSpeed = pickUpObjectRigidBody.velocity.x;
                    float weight = pickUpObjectRigidBody.mass;
                    float gravity = pickUpObjectRigidBody.gravityScale;

                    if (fallingSpeed > minFallingSpeedDetection)
                    {
                        float lostHealth = fallingSpeed * weight * gravity;
                        lostHealth = Mathf.Floor(lostHealth);

                        float health = healthManager.GetHealth();
                        Vector3 healthToLose = new Vector3(lostHealth / 10, 0.0f, 0.0f);

                        healthManager.LoseHealth(healthToLose);

                        fallenObject = collision.collider.gameObject;

                        Debug.Log(fallingSpeed + " " + weight + " " + gravity);
                        Debug.Break();
                    }
                }
            }
        }
    }
}

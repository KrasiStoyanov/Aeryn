using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WispManager : MonoBehaviour
{
    [Tooltip("The firebal GameObject.")]
    public GameObject fireball;

    [Tooltip("The wisp GameObject.")]
    public GameObject wisp;

    [Tooltip("The speed of the fireball.")]
    public float fireballSpeed;

    [Tooltip("The charge speed of the fireball.")]
    public float chargeSpeed = 5;

    [Tooltip("The strength of the fireball.")]
    public float strength = 10;

    private float normalGravity = 1;

    private bool isFacingOpposite = false;

    void Update()
    {
        RotateObject(wisp);
        Shoot();
    }

    /// <summary>
    /// Shoot whenever the specific control key is pressed.
    /// </summary>
    private void Shoot()
    {
        transform.position = wisp.transform.position;
        float widthOfSource = wisp.GetComponent<Renderer>().bounds.size.x;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 target = mousePosition - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target);

        if (Input.GetMouseButton(0))
        {
            strength += chargeSpeed * Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 instantiatingPosition = new Vector3(transform.position.x + (widthOfSource / 2), transform.position.y, transform.position.z);
            if (isFacingOpposite)
            {
                instantiatingPosition.x = instantiatingPosition.x - widthOfSource;
            }

            GameObject newBullet = Instantiate(fireball, instantiatingPosition, transform.rotation) as GameObject;

            // Set source and target/targets of shooting to the fireball.
            FireballBehaviour bulletBehaviourScript = newBullet.GetComponent<FireballBehaviour>();
            GameObject[] shootingTargets = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject shootingSource = gameObject.transform.parent.gameObject;

            bulletBehaviourScript.SetShootingSource(shootingSource);
            bulletBehaviourScript.SetShootingTarget(shootingTargets);

            // Set the bullet's size.
            Transform fireballSize = newBullet.GetComponent<Transform>();
            fireballSize.localScale = new Vector3(0.1f * strength, 0.1f * strength, 1);

            // Give the bullet speed.
            Rigidbody2D rigidBody = newBullet.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = normalGravity;
            rigidBody.velocity = transform.up * strength * fireballSpeed;

            strength = 10;
        }
    }

    /// <summary>
    /// Rotate a GameObject based on mouse position.
    /// </summary>
    /// <param name="objectToRotate">The GameObject that needs to be rotated.</param>
    private void RotateObject(GameObject objectToRotate)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - objectToRotate.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (angle > 0 && angle <= 90 && isFacingOpposite)
        {
            FlipObject(objectToRotate);
        }
        else if (angle < 0 && angle >= -90 && isFacingOpposite)
        {
            FlipObject(objectToRotate);
        }

        if (angle > 90 && angle <= 189 && !isFacingOpposite)
        {
            FlipObject(objectToRotate);
        }
        else if (angle < -90 && angle >= -180 && !isFacingOpposite)
        {
            FlipObject(objectToRotate);
        }

        objectToRotate.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
    }

    /// <summary>
    /// Vertically flip a GameObject.
    /// </summary>
    /// <param name="objectToFlip">The GameObject that needs to be flipped.</param>
    private void FlipObject(GameObject objectToFlip)
    {
        objectToFlip.transform.localScale = new Vector3(objectToFlip.transform.localScale.x, -objectToFlip.transform.localScale.y, objectToFlip.transform.localScale.z);

        isFacingOpposite = !isFacingOpposite;
    }
}
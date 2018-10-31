﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballBehaviour : MonoBehaviour
{
    [Tooltip("The lifetime of the GameObject.")]
    public float lifeTime = 2f;

    private GameObject shootingSource;
    private GameObject[] shootingTargets;
    
    void Start()
    {
        // Exclude the shooting source from the collision detection if the source is set to an instance of an object.
        if (shootingSource)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shootingSource.GetComponent<Collider2D>());
        }

        // Start the countdown of the fireball lifespan.
        StartCoroutine(DestroyFireball());
    }

    /// <summary>
    /// Check whether fireball is colliding with the specified target/targets and if so, remove the needed health.
    /// </summary>
    /// <param name="collision">Reference to the collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If there are no shooting targets added to the array, exit the collision detection.
        if (shootingTargets == null)
        {
            return;
        }

        string collisionTargetTag = collision.collider.tag;
        foreach (GameObject target in shootingTargets)
        {
            string targetTag = target.tag;
            if (collisionTargetTag == targetTag)
            {
                Transform turretOfShootingTarget = target.transform.GetChild(target.transform.childCount - 1);

                // Get the health mechanic script that is attached to the target and remove the needed health.
                HealthMechanic healthMechanicScript = turretOfShootingTarget.GetComponent<HealthMechanic>();
                healthMechanicScript.ChangeHealth(-10);

                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Destroy the fireball GameObject after its lifetime expires.
    /// </summary>
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    /// <summary>
    /// Set the value for the shooting source.
    /// </summary>
    /// <param name="source">Reference to the shooting source GameObject.</param>
    public void SetShootingSource(GameObject source)
    {
        shootingSource = source;
    }

    /// <summary>
    /// Set the value for the shooting target.
    /// </summary>
    /// <param name="target">Reference to the shooting target GameObject.</param>
    public void SetShootingTarget(GameObject[] targets)
    {
        shootingTargets = targets;
    }
}

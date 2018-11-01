using System.Collections;
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
        Vector3 bulletSize = transform.localScale;

        // If there are no shooting targets added to the array, exit the collision detection.
        if (shootingTargets == null)
        {
            return;
        }

        foreach (GameObject target in shootingTargets)
        {
            if (collision.collider.gameObject == target)
            {
                // Check what the target tag is and based on that get their manager scripts to call their different change health functions.
                switch (target.tag)
                {
                    case "Player":
                        WispManager wispManager = target.transform.GetChild(target.transform.childCount - 1).GetComponent<WispManager>();
                        wispManager.ChangeHealth(bulletSize);

                        break;
                    case "Enemy":
                        EnemyBehaviourScorpion enemyScorpionManager = target.GetComponent<EnemyBehaviourScorpion>();
                        enemyScorpionManager.ChangeHealth(bulletSize);

                        break;
                }
            }
        }

        // If an enemy shoots and hits another enemy - do damage.
        if (collision.collider.tag == "Enemy" && collision.collider.tag == shootingSource.tag)
        {
            EnemyBehaviourScorpion enemyScorpionManager = collision.gameObject.GetComponent<EnemyBehaviourScorpion>();

            enemyScorpionManager.ChangeHealth(bulletSize);
        }

        if (collision.collider.tag != shootingSource.tag)
        {
            Destroy(gameObject);
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

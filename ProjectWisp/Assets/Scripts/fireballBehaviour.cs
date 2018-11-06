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
                HealthMechanic targetHealthMechanic = target.GetComponent<HealthMechanic>();

                targetHealthMechanic.LoseHealth(bulletSize);
            }
        }

        if (collision.collider.tag == "HealingSource")
        {
            if (shootingSource.tag == "Player")
            {
                // Disable all movement and visuals of the fireball in order to smoothly increase the intensity of the hit candle.

                // Hide the fireball.
                gameObject.GetComponent<Renderer>().enabled = false;

                // Stop the falling of the fireball - keep it in its last position.
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                for (int index = 0; index < transform.childCount; index++)
                {
                    // Disable all child elements of the fireball.
                    transform.GetChild(index).gameObject.SetActive(false);
                }
                
                Light lightSource = collision.collider.gameObject.transform.GetComponent<Light>();

                // Smoothly increase the light of the candle.
                StartCoroutine(LightCandle(lightSource, 5.0f, 6.0f));
            }
        }

        if (collision.collider.tag == "RopePiece")
        {
            BurningScript burningScript = collision.collider.gameObject.GetComponent<BurningScript>();

            burningScript.IsBurning = true;
        }

        if (collision.collider.tag == "CrackedPillar") {
            CrackedPillarBehaviour pillarScript = collision.collider.gameObject.GetComponent<CrackedPillarBehaviour>();

            pillarScript.runDestruction = true;
        }

        // Whenever the fireball collides with any other game object from the above - destroy.
        // Exclude elements with tag HealingSource from this check in order to smoothly increase candle ligth and then destroy.
        if (collision.collider.tag != shootingSource.tag && collision.collider.tag != "HealingSource")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Smoothly increase the intensity of the light of the given candle.
    /// </summary>
    /// <param name="lightSource">The light source that needs to have its intensity increased.</param>
    /// <param name="targetIntensity">The maximum intensity that the light should reach.</param>
    /// <param name="speed">The speed at which the intensity is increasing.</param>
    private IEnumerator LightCandle(Light lightSource, float targetIntensity, float speed)
    {
        // Smoothly increase the intensity of the candle's light until it reaches the target value.
        while (lightSource.intensity < targetIntensity)
        {
            lightSource.intensity += speed * Time.deltaTime;

            // Keep the value of the intensity between min and max.
            lightSource.intensity = Mathf.Clamp(lightSource.intensity, 0, targetIntensity);

            yield return null;
        }

        // When done with lightin up the candle, destroy the fireball.
        if (lightSource.intensity == targetIntensity)
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

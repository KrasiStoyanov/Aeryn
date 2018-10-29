using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballBehaviour : MonoBehaviour
{
    [Tooltip("The lifetime of the GameObject.")]
    public float lifeTime = 2f;

    // Use this for initialization
    void Start()
    {
        // Get all enemies and exclude them from collision detection
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        }

        StartCoroutine(DestroyFireball());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject wisp = GameObject.FindGameObjectWithTag("Player");
            Transform turret = wisp.transform.GetChild(wisp.transform.childCount - 1);

            HealthMechanic healthMechanicScript = turret.GetComponent<HealthMechanic>();
            healthMechanicScript.ChangeHealth(-10);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballBehaviour : MonoBehaviour
{
    [Tooltip("The lifetime of the GameObject.")]
    public float lifeTime = 2f;

    [Tooltip("Reference to the main character.")]
    public WispManager wispManager;

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
            wispManager.ChangeHealth(-10);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}

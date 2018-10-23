using System.Collections;

using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    [Tooltip("The lifetime of the GameObject.")]
    public float lifeTime = 2f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyFireball());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}

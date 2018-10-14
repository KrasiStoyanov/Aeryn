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
    
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}

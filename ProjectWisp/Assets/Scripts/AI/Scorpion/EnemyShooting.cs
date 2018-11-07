using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyBehaviourScorpion
{
    // The list of all targets available.
    // In this context - the targets would be the main player (the Wisp).
    private GameObject[] shootingTargets;

    private Vector3 targetPosition;
    private const float shootingSourceOffset = 0.5f;

    private void Start()
    {
        shootingTargets = GameObject.FindGameObjectsWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    private void Update()
    {
        targetPosition = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
    }

    public void Shoot()
    {
        // Set the position at which the new bullet should be instantiated.
        float widthOfSource = gameObject.GetComponent<Renderer>().bounds.size.x;
        Vector3 instantiatingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        GameObject newBullet = Instantiate(projectile, instantiatingPosition, transform.rotation) as GameObject;

        // Set source and target/targets of shooting to the fireball.
        FireballBehaviour bulletBehaviourScript = newBullet.GetComponent<FireballBehaviour>();
        GameObject shootingSource = gameObject.transform.parent.gameObject;
        
        bulletBehaviourScript.SetShootingSource(shootingSource);
        bulletBehaviourScript.SetShootingTarget(shootingTargets);

        // Give the bullet speed.
        Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * 60;
    }
}

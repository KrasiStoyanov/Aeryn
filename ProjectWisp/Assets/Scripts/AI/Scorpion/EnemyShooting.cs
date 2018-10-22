using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyBehaviourScorpion
{
    private Vector3 targetWisp;
    private const float shootingSourceOffset = 0.5f;

    // Update is called once per frame
    void Update()
    {
        //transform.position = monsterPosition.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetWisp = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetWisp);
    }

    public void Shoot()
    {
        float widthOfSource = gameObject.GetComponent<Renderer>().bounds.size.x;
        Vector3 instantiatingPosition = new Vector3(transform.position.x + (widthOfSource / 2) + shootingSourceOffset, transform.position.y, transform.position.z);

        GameObject newBullet = Instantiate(projectile, instantiatingPosition, transform.rotation) as GameObject;
        Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * 60;
    }
}

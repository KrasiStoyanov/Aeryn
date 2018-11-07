using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FloatingCastle : MonoBehaviour
{

    // Starting all vars --------- Made by Robin ---------
    private Rigidbody2D rb;
    [Tooltip("The speed of the idle movement")]
    public float idleSpeed = 1f;
    [Tooltip("The speed of the player-induced movement")]
    public float moveSpeed = 10f;
    private Vector3 endPosition;

    private float Timer = 0.5f;
    [Tooltip("The time the player has to let go of the controls to start idle movement")]
    public float waitTime = 4f;

    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        // Robin grabs current position
        startPosition = this.transform.position;
        // and opens up rigidbody
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        // also starts the random movement right from the get-go
        RandomPosition();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Timer <= 0f)
        {
            Timer = waitTime;

            Vector3 direction = (endPosition - this.transform.position).normalized;
            // and the force added to the rigidbody
            rb.AddForce(direction * idleSpeed);
            // then we check if the distance is small enough to calculate again
            if (Vector3.Distance(endPosition, this.transform.position) < 0.1f)
            {
                // and then calculate a new direction when needed
                RandomPosition();
            }

        }

        // obvious timer work
        if (Timer > 0f)
        {
            Timer -= 1f * Time.deltaTime;
        }
        else if (Timer < 0f)
        {
            Timer = 0;
        }

        // for if she's trying to sneak off while you're not looking
        if (Time.deltaTime > 1)
        {
            // pull her back in to her startposition
            this.transform.position = startPosition;
        }

    }

    private void RandomPosition()
    {
        endPosition = startPosition + (new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0.0f));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	 //------PREDICTION----Made by Cyril----------
    [SerializeField]
    GameObject predictionPoint;
    [SerializeField]
    float predictionDistance;
    private Vector2 predictionPosition;
    private Transform wispTransform;
    private Vector2 wispPosition;


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
	void Start () {

		// Cyrill grabs the transform or something idk
		wispTransform = GetComponent<Transform>();

		// Robin grabs current position
		startPosition = this.transform.position;
		// and opens up rigidbody
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		// also starts the random movement right from the get-go
		RandomPosition();
		
	}
	
	// Update is called once per frame, fixed tho, cuz it was spazzing out
	void FixedUpdate () {

		// if no keys are pressed and the cooldown timer has run out
		if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && Timer <= 0f) {
			// the random direction is set
			Vector3 direction = (endPosition - this.transform.position).normalized;
			// and the force added to the rigidbody
			rb.AddForce(direction * idleSpeed);
			// then we check if the distance is small enough to calculate again
			if (Vector3.Distance(endPosition, this.transform.position) < 0.1f)
			{
				// and then calculate a new direction when needed
				RandomPosition();
			}
			// otherwise
		} else {
			// check if the player is pressing buttons to move
			Vector2 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			// then add this movement
			rb.AddForce(movement * moveSpeed);
			// and calculate the predictions
			MovePrediction(movement);
			// then we grab the startposition again, for if we idle
			startPosition = this.transform.position;
			// and run the random position generator for if we want to go idle
			RandomPosition();
			// then we check again if there are no buttons checked, so we can reset the timer when the player lets go
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				Timer = waitTime;
			}
		}

		// obvious timer work
		if (Timer > 0f)
        {
            Timer -= 1f * Time.deltaTime;
        } else if (Timer < 0f) {
			Timer = 0;
		}

		// for if she's trying to sneak off while you're not looking
		if (Time.deltaTime > 1)
		{	
			// pull her back in to her startposition
			this.transform.position = startPosition;
		}

	}

	void RandomPosition () {
		endPosition = startPosition + (new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0.0f));
	}
	void MovePrediction (Vector2 movementVector) {
		wispPosition = new Vector2(wispTransform.position.x, wispTransform.position.y);
        predictionPoint.transform.position = wispPosition;

        predictionPosition = movementVector * predictionDistance;
        predictionPoint.transform.position =  predictionPosition + wispPosition;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour {
	 //------PREDICTION----Made by Cyril----------
    [SerializeField]
    GameObject predictionPoint;
    [SerializeField]
    float predictionDistance;
    private Vector2 predictionPosition;
    private Transform wispTransform;
    private Vector2 wispPosition;

	private Rigidbody2D rb;
	public float SPEED = 1f;
	public float moveSPEED = 10f;
	public Vector3 endPosition;

	public float Timer = 0.5f;
	public float waitTime = 4f;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {

		 wispTransform = GetComponent<Transform>();
		startPosition = this.transform.position;
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		RandomPosition();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && Timer <= 0f) {

			Vector3 direction = (endPosition - this.transform.position).normalized;

			rb.AddForce(direction * SPEED);

			if (Vector3.Distance(endPosition, this.transform.position) < 0.1f)
			{
				RandomPosition();
			}
			
		} else {

			Vector2 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			rb.AddForce(movement * moveSPEED);
			MovePrediction(movement);
			startPosition = this.transform.position;
			RandomPosition();
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				Timer = 4f;
			}
		}

		if (Timer > 0f)
        {
            Timer -= 1f * Time.deltaTime;
        } else if (Timer < 0f) {
			Timer = 0;
		}

		// for if she's trying to sneak off while you're not looking
		if (Time.deltaTime > 1)
		{
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

		// if (rb.velocity.y <= 1f && rb.velocity.x <= 1f && Timer <= 0.0f)
		// {
		// 	Timer = waitTime;
		// 	rb.AddForce(direction * SPEED);
		// 	RandomPosition();
		// }

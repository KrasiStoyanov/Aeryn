using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	[SerializeField]
	[Range(0, 30)]
	float Speed = 30.0f;

	// Animator animator;
	SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
		// transform.position = new Vector3(0, 0, 0);

		// get Access to Animator component
		// animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		// currentPosition.x += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		// currentPosition.y += Input.GetAxis("Vertical") * Speed * Time.deltaTime;
		// transform.position = currentPosition;

		// set value to characterState parameter based on input

		// animation
		// if (Input.GetAxis("Horizontal") == 0){

		// 	// animator.SetInteger("characterState", 0);

		// 	if (Input.GetAxis("Vertical") == 0){
		// 		animator.SetInteger("characterState", 0);
		// 	}
		// 	else{
		// 		animator.SetInteger("characterState", 2);
		// 	}
		// }
		// else{
		// 	animator.SetInteger("characterState", 1);
		// }

		if (Input.GetAxis("Horizontal") > 0){
			// spriteRenderer.flipX = true;
			gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
		else if(Input.GetAxis("Horizontal") < 0){
			// spriteRenderer.flipX = false;
			gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
		}
		
		// if (Input.GetAxis("Vertical") > 0){
		// 	spriteRenderer.flipY = false;
		// }
		// else if(Input.GetAxis("Vertical") < 0){
		// 	spriteRenderer.flipY = true;
		// }

		// movement
		// transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Input.GetAxis("Vertical") * Speed * Time.deltaTime, 0);
	}
}

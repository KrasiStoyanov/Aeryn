using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wispShooting : MonoBehaviour {

	// [SerializeField]
	// [Range(0, 10)]
	[SerializeField]
	GameObject fireball;

	[SerializeField]
	GameObject wisp;

	[SerializeField]
	float fireballSpeed;

	[SerializeField]
	private float Strength = 10;
	
	[SerializeField]
	float ChargeSpeed = 5;

	private float NormalGravity = 1;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	//transform.position = wisp.transform.position;
	Vector3 mousePositionVector3 = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);	
	mousePositionVector3 = Camera.main.ScreenToWorldPoint(mousePositionVector3);

	Vector3 targetdir = mousePositionVector3 -transform.position;
	transform.rotation= Quaternion.LookRotation(Vector3.forward,targetdir);

	// if(Input.GetMouseButtonDown(0)){
		
		
		
	// }
	if (Input.GetMouseButton(0)){
			Strength += ChargeSpeed*Time.deltaTime;

		}
	else if(Input.GetMouseButtonUp(0)){
			GameObject newBullet = Instantiate(fireball,transform.position,transform.rotation) as GameObject;
			Transform fireballSize = newBullet.GetComponent<Transform>();
			fireballSize.localScale = new Vector3( 0.3f*Strength, 0.3f*Strength,1);
			Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
			rigid.gravityScale = 0;
			rigid.gravityScale = NormalGravity;
			rigid.velocity= transform.up*Strength*fireballSpeed;
			Strength = 10;
	}
		
	}
}

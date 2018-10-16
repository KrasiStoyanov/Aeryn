using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawBehaviour : MonoBehaviour {

	//movement

	//attack
	[SerializeField]
	float timeBtwAttacks;
	[SerializeField]
	float rotationSpeed;

	//general
	private Transform target;
	private Vector3 clawInitialPosition;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		clawInitialPosition = transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 direction = target.position - transform.position;
		float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
		Quaternion rotation =	Quaternion.AngleAxis(angle,Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

	}
}

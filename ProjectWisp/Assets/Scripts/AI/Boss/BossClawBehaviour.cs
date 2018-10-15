using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawBehaviour : MonoBehaviour {

	//movement

	//attack
	[SerializeField]
	float timeBtwAttacks;
	//general
	private Transform target;
	private Vector3 clawInitialPosition;

	// Use this for initialization
	void Start () {
		clawInitialPosition = transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

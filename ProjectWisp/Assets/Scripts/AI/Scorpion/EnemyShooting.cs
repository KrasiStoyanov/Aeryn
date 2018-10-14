using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyBehaviourScorpion {

	private Vector3 targetWisp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = monsterPosition.position;
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetWisp = target.position -transform.position;
		transform.rotation= Quaternion.LookRotation(Vector3.forward,targetWisp);
	}
	public void Shoot () {
		GameObject newBullet = Instantiate(projectile,transform.position,transform.rotation) as GameObject;
		Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
		rigid.velocity= transform.up*60;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScorpion : MonoBehaviour {

	//movement
	public float movementSpeed;
	public float triggerDistance;
	public float shootDistance;
	public float retreatDistance;
	//shooting
	public float weaponCoolDown;
	private float timeBtwShots = 3;
	public GameObject projectile;
	//general
	private Transform target;
	private Transform monsterPosition;
	private Vector2 targetProjection;

	// Use this for initialization
	void Start () {
		monsterPosition = GetComponent<Transform>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetProjection = new Vector2(target.position.x,monsterPosition.position.y);
		if (timeBtwShots > 0){
			Run();
			timeBtwShots -= Time.deltaTime;
		}
		else {
			Attack();
			
		}

	}

	// Attack behaviour
	private void Attack () {
		if(Vector2.Distance(transform.position, targetProjection) > shootDistance){

			transform.position = Vector2.MoveTowards(transform.position, targetProjection, movementSpeed * Time.deltaTime);
		}
		else if (Vector2.Distance(transform.position, target.position) <= shootDistance || Vector2.Distance(transform.position, targetProjection) <= shootDistance){
			Shoot();
		}
		

	}
	//Retreat behaviour
	private void Run () {
		transform.position = Vector2.MoveTowards(transform.position, targetProjection, -movementSpeed * Time.deltaTime);
	}
	// Shooting function
	private void Shoot () {
		GameObject newBullet = Instantiate(projectile,transform.position,Quaternion.LookRotation(Vector3.forward, target.position-transform.position)) as GameObject;
		Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
		rigid.velocity= new Vector2(target.position.x, target.position.y)*2;
		timeBtwShots = weaponCoolDown;

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScorpion : MonoBehaviour {

	//movement
	[SerializeField]
	float movementSpeed;
	[SerializeField]
	float triggerDistance;
	private bool triggered = false;
	[SerializeField]
	float shootDistance;
	[SerializeField]
	
	float retreatDistance;
	//shooting
	[SerializeField]
	float weaponCoolDown;
	protected float timeBtwShots = 3;
	[SerializeField]
	GameObject projectile;
	//general
	private Transform target;
	protected Transform monsterPosition;
	private Vector2 targetProjection;

	// Use this for initialization
	void Start () {
		monsterPosition = GetComponent<Transform>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetProjection = new Vector2(target.position.x,monsterPosition.position.y);

		if (Vector2.Distance(transform.position, targetProjection) <= triggerDistance)
		{
			triggered = true;	
		}
		
		if (triggered == true){ 
			if (timeBtwShots > 0){
				Run();
				timeBtwShots -= Time.deltaTime;
			}
			else {
				Attack();
					
			}
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
		//Vector3 mousePositionVector3 = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);	
		Vector3 mousePositionVector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//GameObject newBullet = Instantiate(projectile,transform.position,Quaternion.LookRotation(Vector3.forward, target.position-transform.position)) as GameObject;
		GameObject newBullet = Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
		Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
		rigid.velocity= new Vector2(mousePositionVector3.x,mousePositionVector3.y)*1;
		//rigid.velocity= transform.*20;
		timeBtwShots = weaponCoolDown;

	}
}

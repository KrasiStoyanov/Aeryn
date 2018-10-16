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
	protected Vector3 targetdir;
	[SerializeField]
	public EnemyShooting monsterScorpionTurret;
	[SerializeField]
	protected float weaponCoolDown;
	protected float timeBtwShots = 3;
	[SerializeField]
	protected GameObject projectile;
	//general
	protected Transform target;
	protected Transform monsterPosition;
	private Vector2 targetProjection;

	// Use this for initialization
	void Start () {
		monsterPosition = GetComponent<Transform>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetdir = target.position -transform.position;
		targetProjection = new Vector2(target.position.x,monsterPosition.position.y);
		
		if (triggered == true){ 
			if (timeBtwShots > 0){
				Run();
				timeBtwShots -= Time.deltaTime;
			}
			else {
				Attack();
				
			}
		}
		else{
			if (Vector2.Distance(transform.position, targetProjection) <= triggerDistance)
		{
			triggered = true;	
		}
		}
		
	}

	// Attack behaviour
	private void Attack () {
		if(Vector2.Distance(transform.position, targetProjection) > shootDistance){

			transform.position = Vector2.MoveTowards(transform.position, targetProjection, movementSpeed * Time.deltaTime);
		}
		else if (Vector2.Distance(transform.position, target.position) <= shootDistance || Vector2.Distance(transform.position, targetProjection) <= shootDistance){
			monsterScorpionTurret.Shoot();
			timeBtwShots = weaponCoolDown;
		}
		

	}
	//Retreat behaviour
	private void Run () {
		transform.position = Vector2.MoveTowards(transform.position, targetProjection, -movementSpeed * Time.deltaTime);
	}
	//Shooting function old
	// private void Shoot () {
	// 	//Vector3 mousePositionVector3 = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);	
	// 	Vector3 mousePositionVector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	// 	//GameObject newBullet = Instantiate(projectile,transform.position,Quaternion.LookRotation(Vector3.forward, target.position-transform.position)) as GameObject;
	// 	GameObject newBullet = Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
	// 	Rigidbody2D rigid = newBullet.GetComponent<Rigidbody2D>();
	// 	rigid.velocity= new Vector2(mousePositionVector3.x,mousePositionVector3.y)*1;
	// 	timeBtwShots = weaponCoolDown;

	// }
}

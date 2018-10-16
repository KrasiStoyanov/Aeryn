using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawBehaviour : MonoBehaviour {

	//movement
	[SerializeField]
	float rotationSpeed;

	//attack
	[SerializeField]
	float attackCooldown;
	[SerializeField]
	float flyToTargetSpeed;
	private float timeBtwAttacks = 3;
	private bool canAttack;
	private bool didAttack;
	private Vector3 attackTarget;

	//general
	private Transform target;
	private Vector3 clawInitialPosition;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		clawInitialPosition = transform.position;

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeBtwAttacks > 0)
			{
				Rotate();
				timeBtwAttacks -= Time.deltaTime;
			}
			else 
			{
				Attack();
				
			}

	}
	//claw rotates towards character
	void Rotate() 
	{
		Vector2 direction = target.position - transform.position;
		float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
		Quaternion rotation =	Quaternion.AngleAxis(angle,Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
	}
	//claw attacks
	void Attack()
	{
		if (canAttack)
		{
			if (Vector2.Distance(transform.position, attackTarget) > 0 && didAttack == false)
			{
				transform.position = Vector2.MoveTowards(transform.position, attackTarget, flyToTargetSpeed * Time.deltaTime);
			}
			else 
			{
				didAttack = true;
				if (Vector2.Distance(transform.position, clawInitialPosition) > 0)
				{
					transform.position = Vector2.MoveTowards(transform.position, clawInitialPosition, flyToTargetSpeed/2 * Time.deltaTime);
				}
				else
				{
					timeBtwAttacks = attackCooldown;
					canAttack = false;
					didAttack = false;
				}
			}
		}
		else
		{
			attackTarget = target.position;
			canAttack = true;
		}
	}

		
}

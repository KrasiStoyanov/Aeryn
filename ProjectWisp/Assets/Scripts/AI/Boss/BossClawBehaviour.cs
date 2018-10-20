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
	[SerializeField]
	[Range(0,100)]
	int chanceOfFirstAttack;
	private int attackType;
	private float timeBtwAttacks = 3;
	private bool canAttack;
	private bool canDropClaw;
	private bool canReturn;
	private Vector3 attackTarget;
	private Vector3 attackfromHeigth;
	private Vector3 bottomAttackPoint;

	//general
	[SerializeField]
	GameObject screenDividerUp;
	[SerializeField]
	GameObject screenDividerBottom;
	private Transform target;
	private Transform targetPrediction;
	private Vector3 clawInitialPosition;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetPrediction = GameObject.FindGameObjectWithTag("Prediction").GetComponent<Transform>();
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
		Vector2 direction = targetPrediction.position - transform.position;
		float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
		Quaternion rotation =	Quaternion.AngleAxis(angle,Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
	}
	//claw attacks
	void Attack()
	{
		//float chance = chanceOfFirstAttack/100;
		if (attackType <= chanceOfFirstAttack)
		{
			if (canAttack)
			{
				if (Vector2.Distance(transform.position, attackTarget) != 0 && canReturn == false)
				{
					transform.position = Vector2.MoveTowards(transform.position, attackTarget, flyToTargetSpeed * Time.deltaTime);
				}
				else 
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) != 0)
					{
						transform.position = Vector2.MoveTowards(transform.position, clawInitialPosition, flyToTargetSpeed/2 * Time.deltaTime);
					}
					else
					{
						timeBtwAttacks = attackCooldown;
						Debug.Log("1st Attack");
						attackType = Random.Range(0, 100);
						canAttack = false;
						canReturn = false;
					}
				}
			}
			else
			{
				attackTarget = targetPrediction.position;
				canAttack = true;
			}
		}
		else 
		{
			if (canAttack)
			{
				if (Vector2.Distance(transform.position, attackfromHeigth) != 0 && canReturn == false && canDropClaw == false)
				{
					attackfromHeigth = new Vector3(target.position.x, screenDividerUp.transform.position.y, target.position.z);
					transform.position = Vector2.MoveTowards(transform.position, attackfromHeigth, flyToTargetSpeed/2 * Time.deltaTime);
				}
				else if(Vector2.Distance(transform.position, bottomAttackPoint) != 0 && canReturn == false)
				{
					canDropClaw = true;
					bottomAttackPoint = new Vector3(target.position.x, screenDividerBottom.transform.position.y, target.position.z);
					transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2 * Time.deltaTime);
				}
				else
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) != 0)
					{
						transform.position = Vector2.MoveTowards(transform.position, clawInitialPosition, flyToTargetSpeed/2 * Time.deltaTime);
					}
					else
					{
						timeBtwAttacks = attackCooldown;
						Debug.Log("2nd Attack");
						attackType = Random.Range(0, 100);
						canAttack = false;
						canDropClaw = false;
						canReturn = false;
					}
				}
			}
			else
			{
				attackfromHeigth = new Vector3(target.position.x, screenDividerUp.transform.position.y, target.position.z);
				bottomAttackPoint = new Vector3(targetPrediction.position.x, screenDividerBottom.transform.position.y, target.position.z);
				attackTarget = targetPrediction.position;
				canAttack = true;
			}
		}
	}

		
}

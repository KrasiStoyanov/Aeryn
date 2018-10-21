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
	private float timeBtwAttacks;
	private bool canAttack = false;
	private bool canDropClaw = false;
	private bool canReturn = false;
	private Vector3 attackTarget;
	private Vector3 attackfromHeigth;
	private Vector3 bottomAttackPoint;

	//general
	[SerializeField]
	bool rightClaw;
	[SerializeField]
	GameObject screenDividerUp;
	[SerializeField]
	GameObject screenDividerBottom;
	private bool wispInRange = false;
	private bool canRotate = false;
	private Transform target;
	private Transform targetPrediction;
	private Vector3 clawInitialPosition;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetPrediction = GameObject.FindGameObjectWithTag("Prediction").GetComponent<Transform>();
		clawInitialPosition = transform.position;
		timeBtwAttacks = attackCooldown;
		
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
				RangeCheck();
				if (wispInRange)
				{
					Attack();
				}
				else
				{
					Rotate();
				}
				
			}

	}
	//claw rotates towards character
	void Rotate() 
	{
		if (rightClaw)
		{
			if (target.position.x > clawInitialPosition.x + 20)
			{
				Vector2 direction = targetPrediction.position - transform.position;
				float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
				Quaternion rotation =	Quaternion.AngleAxis(angle,Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			}
		}
		else
		{
			if (target.position.x < clawInitialPosition.x - 20)
			{
				Vector2 direction = targetPrediction.position - transform.position;
				float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
				Quaternion rotation =	Quaternion.AngleAxis(angle,Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			}
		}
	}
	//Chacks is character is in the range of attack;
	void RangeCheck()
	{
		if (canAttack)
		{
			wispInRange = true;
		}
		else if (rightClaw)
		{
			if (targetPrediction.position.x > screenDividerBottom.transform.position.x && targetPrediction.position.y > screenDividerBottom.transform.position.y)
			{
				if (targetPrediction.position.x < screenDividerUp.transform.position.x && targetPrediction.position.y < screenDividerUp.transform.position.y)
				{
					wispInRange = true;
				}
				else if (targetPrediction.position.x >= screenDividerUp.transform.position.x || targetPrediction.position.y >= screenDividerUp.transform.position.y)
				{
					wispInRange = false;
				}
			}
			else if (targetPrediction.position.x <= screenDividerBottom.transform.position.x || targetPrediction.position.y <= screenDividerBottom.transform.position.y)
			{
				wispInRange = false;
			}
		}
		else if (!rightClaw)
		{
			if (targetPrediction.position.x < screenDividerBottom.transform.position.x && targetPrediction.position.y > screenDividerBottom.transform.position.y)
			{
				if (targetPrediction.position.x > screenDividerUp.transform.position.x && targetPrediction.position.y < screenDividerUp.transform.position.y)
				{
					wispInRange = true;
				}
				else if (targetPrediction.position.x <= screenDividerUp.transform.position.x || targetPrediction.position.y >= screenDividerUp.transform.position.y)
				{
					wispInRange = false;
				}
			}
			else if (targetPrediction.position.x >= screenDividerBottom.transform.position.x || targetPrediction.position.y <= screenDividerBottom.transform.position.y)
			{
				wispInRange = false;
			}
		}
	}
	//claw attacks
	void Attack()
	{
		//float chance = chanceOfFirstAttack/100;
		if (attackType <= chanceOfFirstAttack)
		{
			if (canAttack)
			{
				if (canReturn == false && Vector2.Distance(transform.position, attackTarget) > 1)
				{
					transform.position = Vector2.MoveTowards(transform.position, attackTarget, flyToTargetSpeed * Time.deltaTime);
				}
				else
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) > 0)
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
				if (canDropClaw == false && Vector2.Distance(transform.position, attackfromHeigth) > 3)
				{
					attackfromHeigth = new Vector3(target.position.x, screenDividerUp.transform.position.y, target.position.z);
					transform.position = Vector2.MoveTowards(transform.position, attackfromHeigth, flyToTargetSpeed/2 * Time.deltaTime);
				}
				else if(canReturn == false && Vector2.Distance(transform.position, bottomAttackPoint) > 1)
				{
					canDropClaw = true;
					bottomAttackPoint = new Vector3(target.position.x, screenDividerBottom.transform.position.y, target.position.z);
					transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2 * Time.deltaTime);
				}
				else 
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) > 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStingBehaviour : MonoBehaviour {
//movement
	[SerializeField]
	float rotationSpeed;

	//attack
	[SerializeField]
	float attackCooldown;
	[SerializeField]
	float flyToTargetSpeed;
	private float timeBtwAttacks;
	private bool canAttack = false;
	private bool canDropSting = false;
	private bool canReturn = false;
	private bool step1 = false;
	private bool step2 = false;
	private bool step3 = false;
	private float midAttackPoint;
	private Vector3 attackTarget;
	private Vector3 attackfromHeigth;
	private Vector3 bottomAttackPoint;
	private Vector3 bottomAttackPoint2;

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
	private Vector3 stingInitialPosition;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetPrediction = GameObject.FindGameObjectWithTag("Prediction").GetComponent<Transform>();
		stingInitialPosition = transform.position;
		timeBtwAttacks = attackCooldown;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeBtwAttacks > 0)
			{
				Rotate(targetPrediction.position);
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
					Rotate(targetPrediction.position);
				}
			}

	}
	//claw rotates towards character
	void Rotate(Vector3 rotationTarget) 
	{
		if (target.position.y < stingInitialPosition.y)
		{
			Vector2 direction = rotationTarget - transform.position;
			float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
			Quaternion rotation =	Quaternion.AngleAxis(angle + 90f,Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
	//Chacks is character is in the range of attack;
	void RangeCheck()
	{
		if (canAttack)
		{
			wispInRange = true;
		}
		else 
		{
			if (targetPrediction.position.x < screenDividerBottom.transform.position.x && targetPrediction.position.y > screenDividerBottom.transform.position.y)
			{
				if (targetPrediction.position.x > screenDividerUp.transform.position.x && targetPrediction.position.y < screenDividerUp.transform.position.y)
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
	}
	//Sting attacks
	void Attack()
	{
		
		if (canAttack)
		{
			if (canDropSting == false && Vector2.Distance(transform.position, bottomAttackPoint) > midAttackPoint)
			{
				Rotate(bottomAttackPoint);
				transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2f  * Time.deltaTime);
			}
			else if (step1 == false && Vector2.Distance(transform.position, bottomAttackPoint) > 1)
			{
				canDropSting = true;
				transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2f * Time.deltaTime);
			}
			else if (step2 == false && Vector2.Distance(transform.position, stingInitialPosition) > midAttackPoint)
			{
				step1 = true;
				Rotate(bottomAttackPoint2);
				transform.position = Vector2.MoveTowards(transform.position, stingInitialPosition , flyToTargetSpeed * Time.deltaTime);
			}
			else if (canReturn == false && Vector2.Distance(transform.position, bottomAttackPoint2) > 1)
			{
				step2 = true;
				transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint2 , flyToTargetSpeed * 2f * Time.deltaTime);
			}
			else
			{
				canReturn = true;
				if (Vector2.Distance(transform.position,stingInitialPosition) > 1)
				{
					transform.position = Vector2.MoveTowards(transform.position, stingInitialPosition, flyToTargetSpeed * Time.deltaTime);
				}
				else
				{
					timeBtwAttacks = attackCooldown;
					Debug.Log("2nd Attack");
					canAttack = false;
					canDropSting = false;
					step1 = false;
					step2 = false;
					canReturn = false;
				}
			}
		}
		else
		{
			bottomAttackPoint = new Vector3(targetPrediction.position.x, screenDividerBottom.transform.position.y, target.position.z);
			bottomAttackPoint2 = new Vector3(stingInitialPosition.x + (stingInitialPosition.x - bottomAttackPoint.x), screenDividerBottom.transform.position.y, target.position.z);
			midAttackPoint = Vector2.Distance(transform.position, bottomAttackPoint) / 3f;
			canAttack = true;
		}
		
	}

}

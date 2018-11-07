using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawBehaviour : MonoBehaviour {

	//movement
	[SerializeField]
	float rotationSpeed;

	//attack
	private bool canHurt = true;
	[SerializeField]
	float healthDamage;
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
	// Final move
	private bool finalStingMovePhase = false;
	private bool finalStingMoveChangableVariable = false;
	private bool triggeredToAttack;

	[SerializeField]
	GameObject bossBehaviourController;

	//Animation
	private float distanceToTarget;

	//Sounds
	private bool playSound;

	[SerializeField]
	AudioClip clawAttack;

	[SerializeField]
	AudioClip clawAttackCue;


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
	private float healthAmount;

	// Use this for initialization
	void Start () 
	{
		triggeredToAttack =  bossBehaviourController.GetComponent<BossBehaviourManager>().isTriggered;
		finalStingMoveChangableVariable = bossBehaviourController.GetComponent<BossBehaviourManager>().finalTailWhip;
		healthAmount = gameObject.GetComponent<HealthMechanic>().health;
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetPrediction = GameObject.FindGameObjectWithTag("Prediction").GetComponent<Transform>();
		clawInitialPosition = transform.position;
		timeBtwAttacks = attackCooldown;
		
	}
	void OnCollisionEnter2D (Collision2D col)
    {
		Debug.Log("collides");
        if(col.gameObject.tag == "Player" && canHurt == true)
        {
			Debug.Log("collides w player");
            col.gameObject.GetComponent<HealthMechanic>().health -= healthDamage;
			canHurt = false;
			StartCoroutine(WaitForAttack());
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		triggeredToAttack =  bossBehaviourController.GetComponent<BossBehaviourManager>().isTriggered;
		finalStingMoveChangableVariable = bossBehaviourController.GetComponent<BossBehaviourManager>().finalTailWhip;
		if(triggeredToAttack)
		{
			if (healthAmount > 0)
			{
				if (!finalStingMovePhase)
				{
					if (timeBtwAttacks > 0)
					{
						Rotate(targetPrediction.position);
						timeBtwAttacks -= Time.deltaTime;
						// if(timeBtwAttacks < 1){
						// if(!playSound){
						// 	AudioSource.PlayClipAtPoint(clawAttackCue, transform.position);
						// 	playSound = true;
						// }
			
						// }
						
						
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
				else
				{
					Rotate(screenDividerBottom.transform.position);
					//play Claw clicking animation multiple times.
				}
			}
			else
			{
				Rotate(screenDividerBottom.transform.position);
				Debug.Log("I guess I will die");
				//Claw should be disabled or crippled somehow
			}
		}
		
		

	}
	
	IEnumerator WaitForAttack(){
		yield return new WaitForSeconds(1f);
		canHurt = true;
	}
	//claw rotates towards character
	void Rotate(Vector3 rotationTarget) 
	{
		if (rightClaw)
		{
			if (target.position.x > transform.position.x + 20)
			{
				Vector2 direction = rotationTarget - transform.position;
				float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
				Quaternion rotation =	Quaternion.AngleAxis(angle + 20f,Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			}
		}
		else
		{
			if (target.position.x < transform.position.x - 20)
			{
				Vector2 direction = rotationTarget - transform.position;
				float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
				Quaternion rotation =	Quaternion.AngleAxis(angle - 20f,Vector3.forward);
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
					if (Vector2.Distance(transform.position, attackTarget) > distanceToTarget/2)
					{
						//play claw opening animation---------------------------------ANIMATION--------------------------------------------------
						if(!playSound){
							AudioSource.PlayClipAtPoint(clawAttack, transform.position);
							playSound = true;
						}
						
					}
					else
					{
						//play claw closure animation---------------------------------ANIMATION--------------------------------------------------
					}
				}
				else
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) > 0)
					{
						transform.position = Vector2.MoveTowards(transform.position, clawInitialPosition, flyToTargetSpeed/2 * Time.deltaTime);
						Rotate(target.position);
						//plays Claw idle animation---------------------------------ANIMATION--------------------------------------------------
					}
					else
					{
						timeBtwAttacks = attackCooldown;
						Debug.Log("1st Attack");
						attackType = Random.Range(0, 100);
						canAttack = false;
						canReturn = false;
						playSound = false;
						if (finalStingMoveChangableVariable)
						{
							finalStingMovePhase = true;
						}
					}
				}
			}
			else
			{
				attackTarget = targetPrediction.position;
				distanceToTarget = Vector2.Distance(transform.position, attackTarget);
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
					Rotate(screenDividerUp.transform.position);
				}
				else if(canReturn == false && Vector2.Distance(transform.position, bottomAttackPoint) > 1)
				{
					canDropClaw = true;
					bottomAttackPoint = new Vector3(target.position.x, screenDividerBottom.transform.position.y, target.position.z);
					transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2 * Time.deltaTime);
					//play claw closure animation---------------------------------ANIMATION--------------------------------------------------
				}
				else 
				{
					canReturn = true;
					if (Vector2.Distance(transform.position, clawInitialPosition) > 0)
					{
						transform.position = Vector2.MoveTowards(transform.position, clawInitialPosition, flyToTargetSpeed/2 * Time.deltaTime);
						Rotate(target.position);
						//plays Claw idle animation---------------------------------ANIMATION--------------------------------------------------
					}
					else
					{
						timeBtwAttacks = attackCooldown;
						Debug.Log("2nd Attack");
						attackType = Random.Range(0, 100);
						canAttack = false;
						canDropClaw = false;
						canReturn = false;
						playSound = false;
						if (finalStingMoveChangableVariable)
						{
							finalStingMovePhase = true;
						}
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

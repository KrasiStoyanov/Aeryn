using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStingBehaviour : MonoBehaviour {
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
	// Final move
	private bool finalStingMovePhase = false;
	private bool finalStingMoveChangableVariable = false;
	private bool triggeredToAttack;
	[SerializeField]
	GameObject bossBehaviourController;
		
	//Sounds
	private bool playSound;

	[SerializeField]
	AudioClip stingAttack;


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
	//Health
	private float healthAmount;
	private bool isCrippled;

	// Use this for initialization
	void Start () 
	{
		triggeredToAttack =  bossBehaviourController.GetComponent<BossBehaviourManager>().isTriggered;
		healthAmount = gameObject.GetComponent<HealthMechanic>().health;
		finalStingMoveChangableVariable = bossBehaviourController.GetComponent<BossBehaviourManager>().finalTailWhip;
		healthAmount = gameObject.GetComponent<HealthMechanic>().health;
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		targetPrediction = GameObject.FindGameObjectWithTag("Prediction").GetComponent<Transform>();
		stingInitialPosition = transform.position;
		timeBtwAttacks = attackCooldown;
		
	}
	void OnCollisionEnter2D (Collision2D col)
    {
		if(col.gameObject.tag == "Player" && finalStingMovePhase)
		{
			col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(1000,0,0);
		}
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
					FinalAttack();
				}
			}
			else
			{
				if (finalStingMovePhase)
				{
					FinalAttack();
				}
				Debug.Log("I guess I will die");
				//Sting should be disabled or crippled somehow
			}
		}
		else
		{
			Debug.Log("I am not attacking !!!!!!!!!");
		}


	}
	IEnumerator WaitForAttack(){
		yield return new WaitForSeconds(1f);
		canHurt = true;
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
				if(!playSound){
						AudioSource.PlayClipAtPoint(stingAttack, transform.position);
						playSound = true;
				}
				transform.position = Vector2.MoveTowards(transform.position, bottomAttackPoint, flyToTargetSpeed * 2f * Time.deltaTime);
			}
			else if (step2 == false && Vector2.Distance(transform.position, stingInitialPosition) > midAttackPoint)
			{
				step1 = true;
				playSound = false;
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

				if(!playSound){
						AudioSource.PlayClipAtPoint(stingAttack, transform.position);
						playSound = true;
					}
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
			bottomAttackPoint = new Vector3(targetPrediction.position.x, screenDividerBottom.transform.position.y, target.position.z);
			bottomAttackPoint2 = new Vector3(stingInitialPosition.x + (stingInitialPosition.x - bottomAttackPoint.x), screenDividerBottom.transform.position.y, target.position.z);
			midAttackPoint = Vector2.Distance(transform.position, bottomAttackPoint) / 3f;
			canAttack = true;
		}
		
	}
	void FinalAttack()
	{
		if(isCrippled)
		{
			if (canAttack)
			{
				if (canDropSting == false && Vector2.Distance(transform.position, new Vector3(screenDividerUp.transform.position.x, target.position.y, target.position.z)) > 3)
				{
					transform.position = Vector2.MoveTowards(transform.position, new Vector3(screenDividerUp.transform.position.x, target.position.y, target.position.z), flyToTargetSpeed * Time.deltaTime);
				}
				else if ( step1 ==false && Vector2.Distance(transform.position, new Vector3(screenDividerBottom.transform.position.x, target.position.y, target.position.z)) > 3)
				{
					canDropSting = true ;
					transform.position = Vector2.MoveTowards(transform.position, new Vector3(screenDividerBottom.transform.position.x, target.position.y, target.position.z), flyToTargetSpeed * 1.5f  * Time.deltaTime);
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
		else
		{
			isCrippled = true;
			canAttack = false;
			canDropSting = false;
			step1 = false;
			step2 = false;
			canReturn = false;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviourManager : MonoBehaviour {
	//arms
	[SerializeField]
	GameObject clawRight;
	[SerializeField]
	GameObject clawLeft;
	[SerializeField]
	GameObject sting;
	[SerializeField]
	GameObject face;
	private float healthRightClaw;
	private float healthLeftClaw;
	private float healthSting;
	private float healthFace;
	//Slider
	[SerializeField]
	GameObject Slider;
	//general
	private bool isTriggered = false;
	private bool readyForBattle = false;
	private bool secondStage = false;
	private bool finalTailWhip = false;
	private float generalHealth;
	private float initialGeneralHealth;
	private Transform target;

	[SerializeField]
	GameObject countdownPoint;

	// Use this for initialization
	void Start () {
		Slider.GetComponent<RectTransform>().sizeDelta = new Vector2(0,40f);
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		healthFace = face.GetComponent<HealthMechanic>().health;
		healthRightClaw = clawRight.GetComponent<HealthMechanic>().health;
		healthLeftClaw = clawLeft.GetComponent<HealthMechanic>().health;
		healthSting = sting.GetComponent<HealthMechanic>().health;
		initialGeneralHealth = healthFace + healthLeftClaw + healthRightClaw + healthSting;
		Debug.Log(initialGeneralHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, countdownPoint.transform.position) < 50 || isTriggered == true)
		{
			if (readyForBattle == true)
			{
				ManageHealth();
			}
			else
			{
				
				isTriggered = true;
				readyForBattle = true;

			}
		}
	}
	void ManageHealth () 
	{
		generalHealth = healthFace + healthLeftClaw + healthRightClaw + healthSting;
		if (generalHealth * 100f / initialGeneralHealth < 50f && secondStage == false)
		{
			secondStage = true;

		}
		if (generalHealth * 100f / initialGeneralHealth < 2f && finalTailWhip == false);
		{
			finalTailWhip = true;
		}
	}
}

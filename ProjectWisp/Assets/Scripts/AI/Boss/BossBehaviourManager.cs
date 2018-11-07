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
	GameObject canvas;
	[SerializeField]
	Slider bossHealth;
	//general
	private Canvas canvasVisibility;
	public bool isTriggered = false;
	private bool readyForBattle = false;
	private bool secondStage = false;
	public bool finalTailWhip = false;
	private float generalHealth;
	private float initialGeneralHealth;
	private RectTransform sliderSize;
	private Vector2 sliderSizeVariable;
	private float sliderValue;
	private Transform target;

	//Sounds
	private bool playSound;
	private bool bossIntroduction;
	private float currentMaxHealth;
	
	[SerializeField]
	AudioClip bossHit;

	[SerializeField]
	AudioClip startBoss;

	public AudioSource audioSource1;
	public AudioSource audiosource2;





	[SerializeField]
	GameObject countdownPoint;

	// Use this for initialization
	void Start () {
		
		audioSource1.Play();
		canvasVisibility = canvas.GetComponent<Canvas>();
		canvasVisibility.enabled = false;
		sliderSizeVariable = new Vector2(0,40f);
		sliderSize = canvas.transform.Find("Slider").GetComponent<RectTransform>();	
		sliderSize.sizeDelta = new Vector2(0,40f);
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		healthFace = face.GetComponent<HealthMechanic>().health;
		healthRightClaw = clawRight.GetComponent<HealthMechanic>().health;
		healthLeftClaw = clawLeft.GetComponent<HealthMechanic>().health;
		healthSting = sting.GetComponent<HealthMechanic>().health;
		initialGeneralHealth = healthFace + healthLeftClaw + healthRightClaw + healthSting;
		sliderValue = generalHealth * 100f/ initialGeneralHealth;
		currentMaxHealth = initialGeneralHealth;
		//canvas.transform.Find("Slider").GetComponent<Slider>().value = sliderValue;

		Debug.Log(initialGeneralHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, countdownPoint.transform.position) < 50 || isTriggered == true)
		{
			if(!bossIntroduction){
				AudioSource.PlayClipAtPoint(startBoss, transform.position);
				bossIntroduction = true;
				audioSource1.Stop();
				audiosource2.Play();

			}
			
			if (readyForBattle == true)
			{
				ManageHealth();
			}
			else
			{
				canvasVisibility.enabled = true;
				if (sliderSizeVariable.x < 500)
				{
					sliderSizeVariable.x += 10;
					sliderSize.sizeDelta = sliderSizeVariable;
				}
				else
				{
					isTriggered = true;
					readyForBattle = true;
					playSound = false;
				}
				// while (sliderSizeVariable.x < 500)
				// {
				// 	sliderSizeVariable.x += Time.deltaTime;
				// 	sliderSize.sizeDelta = sliderSizeVariable;
				// }

			}
		}
	}
	void ManageHealth () 
	{
		Debug.Log(generalHealth);
		healthFace = face.GetComponent<HealthMechanic>().health;
		healthRightClaw = clawRight.GetComponent<HealthMechanic>().health;
		healthLeftClaw = clawLeft.GetComponent<HealthMechanic>().health;
		healthSting = sting.GetComponent<HealthMechanic>().health;
		generalHealth = healthFace + healthLeftClaw + healthRightClaw + healthSting;
		sliderValue = generalHealth * 100f/ initialGeneralHealth;
		canvas.transform.Find("Slider").GetComponent<Slider>().value = sliderValue;
		if(generalHealth < currentMaxHealth){
			AudioSource.PlayClipAtPoint(bossHit, transform.position);
			playSound = true;
			currentMaxHealth = generalHealth;
		}
		if (generalHealth * 100f / initialGeneralHealth < 50f && secondStage == false)
		{
			Debug.Log("FUCK OFF");
			secondStage = true;

		}
		if (generalHealth * 100f / initialGeneralHealth < 2f && finalTailWhip == false)
		{
			Debug.Log("Now it should fuck up the wisp");
			clawRight.GetComponent<PolygonCollider2D>().enabled = false;
			clawLeft.GetComponent<PolygonCollider2D>().enabled = false;
			finalTailWhip = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	Image Bar;


	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(ClickMe);
		Bar.fillAmount = 0.5f;
		
	}

	void ClickMe()
	{
		Bar.fillAmount -= 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

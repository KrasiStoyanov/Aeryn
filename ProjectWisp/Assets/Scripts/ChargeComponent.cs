using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeComponent : MonoBehaviour {

    public Gradient gradient;
    public float duration;
    float t = 0f;
	 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// now if the player is holding the button
		if(Input.GetMouseButtonDown(0)) 
		{	
			// // make sure her eyes glow red to indicate the charge
			// eyeColour = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
			// //Can't have pretty eyes without any colour
			// GameObject.Find("Eye").GetComponent<SpriteRenderer>().color = eyeColour;
			
			float value = Mathf.Lerp(0f, 1f, t);
        	t += Time.deltaTime / duration;
     		Color color = gradient.Evaluate(value);
        	GameObject.Find("Eye").GetComponent<SpriteRenderer>().color = color;

		}
	}
}

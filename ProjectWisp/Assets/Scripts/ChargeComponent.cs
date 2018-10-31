using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeComponent : MonoBehaviour {

    public Gradient gradient;
    public float duration;
    private float t = 0f;
	private SpriteRenderer eyeColor;
	private Color initialColor; 
	private float percentValue;
	// Use this for initialization
	void Start () {
		eyeColor = GetComponent<SpriteRenderer>();
		initialColor = eyeColor.color;
		
	}
	
	// Update is called once per frame
	void Update () {
		// now if the player is holding the button
		if(Input.GetMouseButton(0)) 
		{	
			// // make sure her eyes glow red to indicate the charge
			// eyeColour = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
			// //Can't have pretty eyes without any colour
			// GameObject.Find("Eye").GetComponent<SpriteRenderer>().color = eyeColour;

			
			Color colorValue = Color.Lerp(gradient.Evaluate(0f), gradient.Evaluate(1f), t);
			t += Time.deltaTime/duration;
        	eyeColor.color = colorValue;

		}
		else if (Input.GetMouseButtonUp(0))
		{
			eyeColor.color = initialColor;
		}
	}
}

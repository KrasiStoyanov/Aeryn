using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour {

	private Rigidbody2D Variable;

	// Use this for initialization
	void Start () {
		Variable = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			Variable.AddForce(Vector2.up*10,ForceMode2D.Impulse);
		}
		
	}
}

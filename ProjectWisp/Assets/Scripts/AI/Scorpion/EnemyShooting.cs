using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyBehaviourScorpion {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = monsterPosition.position;
		
	}
}

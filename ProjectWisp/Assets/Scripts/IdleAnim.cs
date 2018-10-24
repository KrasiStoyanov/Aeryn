using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour {

		private Rigidbody2D rb;
		
		private ParticleSystem ps;
		private GameObject player;
		public float emissionOverTime = 20.0f;
		public float emissionOverDistance = 50.0f;
		public float StartSpeed = 5.0f;

	// Use this for initialization	 
	void Start () {
		ps = GetComponent<ParticleSystem>();
		player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		// Particle hacking initiated
		var main = ps.main;
		main.startSpeed = StartSpeed;
		var emission = ps.emission;
		// var animationType = ps.textureSheetAnimation.animation;

		if (rb.velocity.y <= 5f && rb.velocity.y >= -5f && rb.velocity.x <= 5f && rb.velocity.x >= -5f)
		{
			emissionOverDistance = 0f;
			emission.rateOverDistance = emissionOverDistance;
			emissionOverTime = 20.0f;
			emission.rateOverTime = emissionOverTime;
			StartSpeed = 5.0f;
			transform.rotation = Quaternion.Euler(-60, -90, 90);
		} else
		{
			emissionOverDistance = 50.0f;
			emission.rateOverDistance = emissionOverDistance;
			emissionOverTime = 0f;
			emission.rateOverTime = emissionOverTime;
			StartSpeed = 0f;
			transform.rotation = Quaternion.Euler(-180, 0, 0);
		}
	}
}

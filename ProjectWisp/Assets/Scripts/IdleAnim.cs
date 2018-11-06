using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour {

		private Rigidbody2D rb;
		private SpriteRenderer sr;
		private ParticleSystem ps;
		private GameObject player;
		private float emissionOverTime;
		private float emissionOverDistance;
		private float StartSpeed;
		private float StartSize;
		private float StartLifetime;

	// Use this for initialization	 
	void Start () {
		ps = GetComponent<ParticleSystem>();
		player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D>();
		sr = player.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// Particle hacking initiated
		var main = ps.main;
		main.startSpeed = StartSpeed;
		main.startSize  = StartSize;
		main.startLifetime = StartLifetime;
		var emission = ps.emission;
		// var animationType = ps.textureSheetAnimation.animation;

		if (rb.velocity.y <= 5f && rb.velocity.y >= -5f && rb.velocity.x <= 5f && rb.velocity.x >= -5f)
		{
			emissionOverDistance = 0f;
			emission.rateOverDistance = emissionOverDistance;
			emissionOverTime = 20.0f;
			emission.rateOverTime = emissionOverTime;
			StartSpeed = 5.0f;
			StartSize = 8f;
			StartLifetime = 1.5f;
		} else
		{
			emissionOverDistance = 50.0f;
			emission.rateOverDistance = emissionOverDistance;
			emissionOverTime = 0f;
			emission.rateOverTime = emissionOverTime;
			StartSpeed = 0f;
			transform.rotation = Quaternion.Euler(-180, 0, 0);
			StartSize = 8f;
			StartLifetime = 0.65f;
		}

		// if (sr.flipX == true)
		// {
		// 	transform.rotation = Quaternion.Euler(-45, 0, 90);
		// } else {
		// 	transform.rotation = Quaternion.Euler(45, 0, 90);
		// }
	}
}

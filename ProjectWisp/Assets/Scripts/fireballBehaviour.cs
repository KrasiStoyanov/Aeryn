using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballBehaviour : MonoBehaviour {

	public float lifeTime = 2f;

	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyFireball());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator DestroyFireball() {
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}
} 

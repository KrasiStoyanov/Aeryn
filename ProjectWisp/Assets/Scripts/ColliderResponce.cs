using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResponce : MonoBehaviour {

	/// <summary>
	/// </summary>
	/// <param name="other"></param>
	
	void OnCollisionEnter2D(Collision2D other)
	{
		this.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;

		Invoke("DestroyAfterTime", 2f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{

	}

	void DestroyAfterTime()
	{
		Destroy(this.gameObject);
	}
	

	void Update () {
		
	}
}

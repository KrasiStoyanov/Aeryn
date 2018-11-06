using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedPillarBehaviour : MonoBehaviour {
	//private GameObject[] pillarPieces;
	public bool runDestruction;
	private bool isDestroyed = false;
	private EdgeCollider2D pillarEdge;
	private CircleCollider2D destructor;

	// Use this for initialization
	void Start () {
		pillarEdge = GetComponent<EdgeCollider2D>();
		destructor = GetComponent<CircleCollider2D>();
		//pillarPieces = GameObject.FindGameObjectsWithTag("PillarPiece");
	}
	
	// Update is called once per frame
	void Update () {
		if(runDestruction == true && isDestroyed == false)
		{
			StartCoroutine(WaitForIt(0.5f));
			pillarEdge.enabled = false;
			destructor.enabled = true;
			isDestroyed = true;
			runDestruction = false;
			
		}
		
	}
	IEnumerator WaitForIt(float time)
	{
		yield return new WaitForSeconds(time);
		destructor.enabled = false;
	}
	
}

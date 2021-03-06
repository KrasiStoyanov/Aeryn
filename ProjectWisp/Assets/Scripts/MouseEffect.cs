﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MouseEffect : MonoBehaviour {

	private GameObject decoLeft;
	private GameObject decoRight;
	private GameObject decoCenter;
	public GameObject menu; // Assign in inspector
	private GameObject gameManagerObject;

	private bool isShownVar;

	// Use this for initialization
	void Start () {
		decoLeft = GameObject.Find("ResumeHoverLeft");
		decoRight = GameObject.Find("ResumeHoverRight");
		decoCenter = GameObject.Find("ResumeHoverCenter");
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnMouseOver() {
		decoLeft.transform.position += Vector3.left * 15.0f;
		decoRight.transform.position += Vector3.right * 15.0f;
		decoCenter.transform.position += Vector3.down * 10.0f;
 	}
	
	public void OnMouseExit() {
		decoLeft.transform.position -= Vector3.left * 15.0f;
		decoRight.transform.position -= Vector3.right * 15.0f;
		decoCenter.transform.position -= Vector3.down * 10.0f;
	}

	public void OnMouseDown() {
		menu.SetActive(false);
		isShownVar = gameManagerObject.GetComponent<MenuManager>().isShowing;
		gameManagerObject.GetComponent<MenuManager>().isShowing = !isShownVar;
		Time.timeScale = 1f;
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject menu; // Assign in inspector
	private GameObject turret;
    private bool isShowing;
	
	
	// Use this for initialization
	void Start () {
		turret = GameObject.Find("turret");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape")) {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }

		if (menu.activeSelf) {
			Time.timeScale = 0f;
			turret.SetActive(false);
		} else {
			Time.timeScale = 1f;
			turret.SetActive(true);
		}
	}
}

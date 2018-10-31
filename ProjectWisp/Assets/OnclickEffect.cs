using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OnclickEffect : MonoBehaviour {

    public GameObject menu; // Assign in inspector
    private bool isShowing;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape")) {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
	}

	// public void OnPointerClick(PointerEventData eventData)
    // {
    //     if (ButtonName = "Resumebutton")
	// 	{
	// 		Canvas.SetActive(false);
	// 	} else if (ButtonName = "OptionsButton")
	// 	{
			
	// 	} else if (ButtonName = "QuitButton")
	// 	{
	// 		EditorSceneManager.OpenScene("Assets/Scenes/StartScene");
	// 	}
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMotion : MonoBehaviour
{
    //------PREDICTION----Made by Cyril----------
    [SerializeField]
    GameObject predictionPoint;
    [SerializeField]
    float predictionDistance;
    private Vector2 predictionPosition;
    private Transform wispTransform;
    private Vector2 wispPosition;

    //------MOVEMENT----Made by Krasi----------
    public float speed;
    private Rigidbody2D rb2d;

    // SpriteRenderer spriteRenderer;

    void Start()
    {
        wispTransform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();

        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);
        wispPosition = new Vector2(wispTransform.position.x, wispTransform.position.y);
        //predictionPoint.transform.position = wispPosition;
        predictionPosition = movement * predictionDistance;
        predictionPoint.transform.position =  predictionPosition + wispPosition;
    }

    // void Update()
    // {
    // 	if (Input.GetAxis("Horizontal") > 0){
    // 		// spriteRenderer.flipX = true;
    // 		gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    // 	}
    // 	else if(Input.GetAxis("Horizontal") < 0){
    // 		// spriteRenderer.flipX = false;
    // 		gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    // 	}

    // 	if (Input.GetAxis("Vertical") > 0){
    // 		spriteRenderer.flipY = false;
    // 	}
    // 	else if(Input.GetAxis("Vertical") < 0){
    // 		spriteRenderer.flipY = true;
    // 	}
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningScript : MonoBehaviour {

	private GameObject RopeAbove;
	private GameObject RopeBelow;
	public Component RopeAboveBool;
	public Component RopeBelowBool;
	private int Counter = 0;
	public bool IsBurning = false;
	private bool HasDone = false;
    public GameObject fire;
    private GameObject burningObject;

	//sound
	private bool playSound;

	[SerializeField]
	AudioClip burnSound;

	void Start () {
		if (transform.parent.tag != "EditorOnly"){
			RopeAbove = transform.parent.gameObject;
		}
		if (transform.childCount > 0){
    		RopeBelow = transform.GetChild(0).gameObject;
		} 
		//Error handling//
		if (RopeAbove != null){
			RopeAboveBool = RopeAbove.GetComponent<BurningScript>();
		}
		if (RopeBelow != null){
			RopeBelowBool = RopeBelow.GetComponent<BurningScript>();
		}
	}

	//Add oncollisionenter2d or ontriggerenter2d here to set IsBurning to true!//
		//Fuck me sideways, it doesn't call oncollisionenter2d.
	// void oncollisionenter2d(Collider2D Collision){
	// 	if (Collision.gameObject.tag == "Fireball"){
	// 		IsBurning = true;
	// 	}
	// 	Debug.Log("1");
	// }

	/////////////////////////////////////////////////////////////////////////////

	void Update () {
		if(IsBurning == true){
			if(!playSound){
				AudioSource.PlayClipAtPoint(burnSound, transform.position);
				playSound = true;
			}
						

			Counter += 1;
            if (burningObject == null && Counter < 300)
            {
                float heightOfRopePiece = gameObject.GetComponent<Renderer>().bounds.size.y;
                float bottomOfRopePiece = transform.position.y + (heightOfRopePiece / 2);

                burningObject = Instantiate(fire, new Vector3(transform.position.x, bottomOfRopePiece, transform.position.z), Quaternion.identity);
                burningObject.transform.parent = transform;
                burningObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                burningObject.transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
            }

			if (HasDone == false){

				//Add any executable code that you want to activate once, before waiting here!//

				playSound = false;

				////////////////////////////////////////////////////////////////////////////////

				HasDone = true;
			}
			if (Counter == 150){
				//If x != null is for error handling//
				if (RopeAboveBool != null){
					if(RopeAboveBool.GetComponent<BurningScript>().IsBurning == false){
						RopeAboveBool.GetComponent<BurningScript>().IsBurning = true;
					}
				}
				if(RopeBelowBool != null){
					if(RopeBelowBool.GetComponent<BurningScript>().IsBurning == false){
						RopeBelowBool.GetComponent<BurningScript>().IsBurning = true;
					}
				}
				//Add any executable code here!//
				
				
				
				/////////////////////////////////
			}
			if (Counter == 300){
				GetComponent<HingeJoint2D>().enabled = false;
				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent<BoxCollider2D>().enabled = false;

                Destroy(burningObject);

                ObjectThatShouldFall objectThatShouldFallScript = transform.root.GetChild(0).GetComponent<ObjectThatShouldFall>();
                objectThatShouldFallScript.ObjectIsFalling();
            }
		}
	}
}
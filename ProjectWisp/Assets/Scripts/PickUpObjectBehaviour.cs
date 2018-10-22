using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PickUpObjectBehaviour : MonoBehaviour
{
    public PickUpMechanic pickUpManager;

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickUpManager.pickedUpObject)
        {
            pickUpManager.ReleasePickedUpObject();
        }
    }
}

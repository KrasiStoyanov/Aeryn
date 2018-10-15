using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpriteVisibility : MonoBehaviour
{
    [Tooltip("The GameObject that needs to have its visibility changed.")]
    public GameObject objectToToggleVisibility;

    // Use this for initialization
    void Start()
    {
        Renderer spriteRenderer = objectToToggleVisibility.GetComponent<Renderer>();

        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}

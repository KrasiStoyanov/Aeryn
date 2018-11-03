using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthMechanic : MonoBehaviour
{
    public float maxHealth = 100;
    public float minHealth = 0;

    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    /// <summary>
    /// Change the value of the health and keep it between the maximum and minimum value.
    /// </summary>
    /// <param name="newHealth">The new health value.</param>
    public void ChangeHealth(float newHealth)
    {
        // Limit the value of the health to be between the min and maximum.
        health = Mathf.Clamp(newHealth, minHealth, maxHealth);
    }

    /// <summary>
    /// Get the current health value.
    /// </summary>
    public float GetHealth()
    {
        return health;
    }
}

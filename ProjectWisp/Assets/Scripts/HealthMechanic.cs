using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthMechanic : MonoBehaviour
{
    private const int maxHealth = 100;
    private const int minHealth = 0;

    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    /// <summary>
    /// Change the value of the health.
    /// </summary>
    /// <param name="additionalHealth">Additional helath to add to the current one (could be positive or negative).</param>
    public void ChangeHealth(int additionalHelth)
    {
        health += additionalHelth;

        // Limit the value of the health to be between the min and maximum.
        Mathf.Clamp(health, maxHealth, minHealth);
    }

    /// <summary>
    /// Get the current health value.
    /// </summary>
    public int GetHealth()
    {
        return health;
    }
}

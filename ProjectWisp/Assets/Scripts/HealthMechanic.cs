using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthMechanic : MonoBehaviour
{
    private const float maxHealth = 100;
    private const float minHealth = 0;

    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    /// <summary>
    /// Change the value of the health and keep it between the maximum and minimum value.
    /// </summary>
    /// <param name="bulletSize">The size of the bullet.</param>
    public void ChangeHealth(Vector3 bulletSize)
    {
        float newHealth = health - (bulletSize.x * 10);

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

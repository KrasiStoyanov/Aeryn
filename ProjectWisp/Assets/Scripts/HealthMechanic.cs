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
    /// Lose health and keep it between the maximum and minimum value.
    /// </summary>
    /// <param name="bulletSize">The size of the bullet.</param>
    public void LoseHealth(Vector3 bulletSize)
    {
        float newHealth = health - (bulletSize.x * 10);

        // Limit the value of the health to be between the min and maximum.
        health = Mathf.Clamp(newHealth, minHealth, maxHealth);
    }

    /// <summary>
    /// Gain health and keep it between the maximum and minimum value.
    /// </summary>
    /// <param name="healthToGain">Health value to be added to the current one.</param>
    public void GainHealth(float healthToGain)
    {
        float newHealth = health + healthToGain;

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

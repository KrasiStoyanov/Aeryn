using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthMechanic : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of maximum health.")]
    private const int maxHealth = 100;

    private const int minHealth = 0;
    private int health;

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
        Mathf.Clamp(health, maxHealth, minHealth);

        Debug.Log(health);
    }

    /// <summary>
    /// Get the current health value.
    /// </summary>
    public int GetHealth()
    {
        return health;
    }
}

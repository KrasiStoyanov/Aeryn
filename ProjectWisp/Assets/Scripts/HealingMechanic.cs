using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealingMechanic : MonoBehaviour
{
    [Tooltip("Reference to object that needs to heal.")]
    public GameObject objectToHeal;

    [Tooltip("Reference to the healint mechanic of the object to heal.")]
    public HealingMechanic healingMechanic;

    [Tooltip("Reference to the health mechanic of the object to heal.")]
    public HealthMechanic healthMechanic;

    // The list of all healing sources in the game.
    private GameObject[] healingSources;

    // The health value that the wisp would gain every frame.
    private float healthToGain = 1.0f;

    // The range between which the object to heal can actually get health.
    private float minDistance = 0.5f;
    private float maxDistance = 0.5f;

    void Start()
    {
        // Get all healing sources in the game and add them to the list.
        healingSources = GameObject.FindGameObjectsWithTag("HealingSource");
    }

    /// <summary>
    /// Heal the object that needs to heal based on its position and the position of the healing sources.
    /// </summary>
    void Update()
    {
        float health = healthMechanic.GetHealth();
        float maxHealth = healthMechanic.maxHealth;

        if (health < maxHealth)
        {
            foreach (GameObject healingSource in healingSources)
            {
                // Get the intensity of the light of the healing source.
                Light lightOfHealingSource = healingSource.GetComponent<Light>();
                float intensityofLightOfHealingSource = lightOfHealingSource.intensity;

                // If the healing source is lit up - heal the object to heal.
                if (intensityofLightOfHealingSource > 0)
                {
                    // Get the position of the healing source and get the distance between it and the object to heal.
                    Vector3 healingSourcePosition = new Vector3(healingSource.transform.position.x, healingSource.transform.position.y, objectToHeal.transform.position.z);
                    float distanceBetweenHealingSourceAndObjectToHeal = Vector3.Distance(objectToHeal.transform.position, healingSourcePosition);

                    if (distanceBetweenHealingSourceAndObjectToHeal > minDistance &&
                        distanceBetweenHealingSourceAndObjectToHeal < maxDistance)
                    {
                        healthMechanic.GainHealth(healthToGain);
                    }
                }
            }
        }
    }
}

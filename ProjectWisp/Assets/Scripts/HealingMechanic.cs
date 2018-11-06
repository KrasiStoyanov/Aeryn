using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealingMechanic : MonoBehaviour
{
    public GameObject objectToHeal;

    public HealingMechanic healingMechanic;

    private GameObject[] healingSources;

    // Use this for initialization
    void Start()
    {
        healingSources = GameObject.FindGameObjectsWithTag("HealingSource");
    }

    // Update is called once per frame
    void Update()
    {
        Light lightOfObjectToHeal = objectToHeal.transform.GetChild(0).GetComponent<Light>();
        float intensityOfLightOfObjectToHeal = lightOfObjectToHeal.intensity;

        if (intensityOfLightOfObjectToHeal < 5)
        {
            foreach (GameObject healingSource in healingSources)
            {
                Light lightOfHealingSource = healingSource.GetComponent<Light>();
                float intensityofLightOfHealingSource = lightOfHealingSource.intensity;

                if (intensityofLightOfHealingSource > 0)
                {
                    float distanceBetweenHealingSourceAndObjectToHeal = Vector3.Distance(objectToHeal.transform.position, healingSource.transform.position);
                        Debug.Log(healingSource.name + " " + objectToHeal.transform.position + " " + healingSource.transform.position + " " + distanceBetweenHealingSourceAndObjectToHeal);
                    if (distanceBetweenHealingSourceAndObjectToHeal > 0.5f &&
                        distanceBetweenHealingSourceAndObjectToHeal < 1.5f)
                    {
                        lightOfObjectToHeal.intensity += 0.1f;
                    }
                }
            }
        }
    }
}

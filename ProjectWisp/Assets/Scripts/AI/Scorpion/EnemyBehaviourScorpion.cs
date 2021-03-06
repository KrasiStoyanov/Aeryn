﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyBehaviourScorpion : MonoBehaviour
{
    //movement
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float triggerDistance;

    private bool triggered = false;

    [SerializeField]
    float shootDistance;

    [SerializeField]
    float safeDistance;

    private bool waitCommand;

    public GameObject dotRight;
    public GameObject dotLeft;

    private Vector2 rayCasterRight;
    private Vector2 rayCasterLeft;

    private RaycastHit2D groundInfoLeft;
    private RaycastHit2D groundInfoRight;

    private RaycastHit2D wallInfoLeft;
    private RaycastHit2D wallInfoRight;

    float retreatDistance;

    //shooting
    protected Vector3 targetdir;

    [SerializeField]
    public EnemyShooting monsterScorpionTurret;

    [SerializeField]
    protected float weaponCoolDown;
    protected float timeBtwShots = 3;

    [SerializeField]
    protected GameObject projectile;

    //general
    protected Transform target;
    protected Transform monsterPosition;
    private Vector2 targetProjection;

    private HealthMechanic healthManager;
    private List<GameObject> lightSources;

    // The intensity rate for light sources.
    [SerializeField]
    private float intensityRate = 0.1f;

    // The range between which the object to heal can actually get health.
    [SerializeField]
    private float minDistance = 15.0f;
    [SerializeField]
    private float maxDistance = 25.0f;

    // Use this for initialization
    void Start()
    {
        monsterPosition = GetComponent<Transform>();

        healthManager = GetComponent<HealthMechanic>();

        lightSources = new List<GameObject>();
        foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (go.name == "Light Source")
            {
                lightSources.Add(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rayCasterLeft = new Vector2(dotLeft.transform.position.x, dotLeft.transform.position.y - 0.3f);
        rayCasterRight = new Vector2(dotRight.transform.position.x, dotRight.transform.position.y - 0.3f);

        groundInfoLeft = Physics2D.Raycast(rayCasterLeft, Vector2.down, 4f);
        groundInfoRight = Physics2D.Raycast(rayCasterRight, Vector2.down, 4f);

        wallInfoLeft = Physics2D.Raycast(dotLeft.transform.position, Vector2.left, 0.1f);
        wallInfoRight = Physics2D.Raycast(dotRight.transform.position, Vector2.right, 0.1f);

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetdir = target.position - transform.position;
        targetProjection = new Vector2(target.position.x, monsterPosition.position.y);

        if (GetComponent<HealthMechanic>().health <= 0f)
        {
            Destroy(gameObject);
        }
        if (triggered == true)
        {
            if (timeBtwShots > 0)
            {
                Run();
                timeBtwShots -= Time.deltaTime;
            }
            else
            {
                waitCommand = false;
                Attack();
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, targetProjection) <= triggerDistance)
            {
                triggered = true;
            }
        }

        InfluenceLight();
    }

    // Attack behaviour
    private void Attack()
    {
        if (Vector2.Distance(transform.position, target.position) <= shootDistance || Vector2.Distance(transform.position, targetProjection) <= 2f || groundInfoLeft.collider == false || groundInfoRight.collider == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetProjection, -movementSpeed * Time.deltaTime);
            monsterScorpionTurret.Shoot();

            timeBtwShots = weaponCoolDown;
        }
        else if (Vector2.Distance(transform.position, target.position) >= shootDistance && groundInfoLeft.collider == true && groundInfoRight.collider == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetProjection, movementSpeed * Time.deltaTime);
        }

    }

    //Retreat behaviour
    private void Run()
    {
        if (Vector2.Distance(transform.position, target.position) <= safeDistance)
        {
            if (wallInfoLeft.collider == false && wallInfoRight.collider == false)
            {
                if (groundInfoLeft.collider == true && groundInfoRight.collider == true && waitCommand == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetProjection, -movementSpeed * Time.deltaTime);
                }
                else if (groundInfoLeft.collider == false || groundInfoRight.collider == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetProjection, movementSpeed * Time.deltaTime);

                    waitCommand = true;
                }
            }
        }
    }

    private void InfluenceLight()
    {
        for (int index = 0; index < lightSources.Count; index++)
        {
            GameObject lightSource = lightSources[index];
            Light lightComponent = lightSource.GetComponent<Light>();

            Vector3 lightSourcePosition = new Vector3(lightSource.transform.position.x, lightSource.transform.position.y, transform.position.z);
            float distanceBetweenLightSourceAndTheEnemy = Vector3.Distance(transform.position, lightSourcePosition);
            
            if (distanceBetweenLightSourceAndTheEnemy > minDistance &&
                distanceBetweenLightSourceAndTheEnemy < maxDistance)
            {
                lightComponent.intensity = distanceBetweenLightSourceAndTheEnemy / intensityRate;
            }

            lightComponent.intensity = Mathf.Clamp(lightComponent.intensity, 0, 5);
        }
    }
}

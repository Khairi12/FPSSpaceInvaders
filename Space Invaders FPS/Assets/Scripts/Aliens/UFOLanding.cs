using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLanding : MonoBehaviour
{
    [HideInInspector]
    public bool landed { get; private set; }

    public delegate void LandingEvent();
    public event LandingEvent OnLand;

    public float landingDist = 5f;
    public float landingSpeed = 1f;

    public float spawnAlienCount = 10f;
    public float spawnAlienInterval = 3f;

    private UFOTeleport teleportScript;
    private float outSpawnAlienInterval;
    private float maxDistance = 100f;
    private float distanceToGround = 5f;

    private void Awake()
    {
        teleportScript = GetComponent<UFOTeleport>();
    }

    private void Start()
    {
        landed = false;
        outSpawnAlienInterval = spawnAlienInterval;
    }

    private bool WithinLandingRange()
    {
        if (Physics.Raycast(transform.position, Vector3.down, landingDist, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }

        return false;
    }

    private void UpdateGroundDist()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            distanceToGround = Vector3.Distance(hit.point, transform.position);
        }
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * (distanceToGround / maxDistance) * landingSpeed * Time.deltaTime);
    }

    private void AlienSpawn()
    {
        if (outSpawnAlienInterval <= 0f)
        {
            if (OnLand != null)
                OnLand();

            outSpawnAlienInterval = spawnAlienInterval;
            spawnAlienCount -= 1f;
        }
        else
        {
            outSpawnAlienInterval -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (teleportScript.teleporting)
            return;

        // spawn aliens if the ship has landed
        if (landed)
        {
            if (spawnAlienCount > 0f)
                AlienSpawn();

            return;
        }

        // update the distance to ground
        UpdateGroundDist();

        // continue moving down until landed
        if (WithinLandingRange())
        {
            if (OnLand != null)
            {
                landed = true;
                OnLand();
            }
        }
        else
        {
            Movement();
        }
	}
}

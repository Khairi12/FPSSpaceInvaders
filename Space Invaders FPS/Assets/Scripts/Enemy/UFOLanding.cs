using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLanding : MonoBehaviour
{
    public delegate void LandingEvent();
    public event LandingEvent OnLand;

    public float landingDist = 5f;
    public float landingSpeed = 1f;

    private UFOTeleport teleportScript;
    private float maxDistance = 100f;
    private float distanceToGround = 5f;
    private bool landed = false;

    private void Awake()
    {
        teleportScript = GetComponent<UFOTeleport>();
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

    private void Update()
    {
        if (landed || teleportScript.teleporting)
            return;

        UpdateGroundDist();

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

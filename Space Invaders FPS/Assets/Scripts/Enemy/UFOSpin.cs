using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpin : MonoBehaviour
{
    public float spinSpeed = 1f;

    private UFOTeleport teleportScript;
    private float maxDistance = 100f;
    private float distanceToGround = 5f;

    private void Awake()
    {
        teleportScript = GetComponent<UFOTeleport>();
    }

    private void UpdateGroundDist()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            distanceToGround = Vector3.Distance(hit.point, transform.position);
        }
    }

    private void Spin()
    {
        transform.Rotate(new Vector3(0f, (distanceToGround / maxDistance) * spinSpeed * Time.deltaTime, 0f));
    }

	private void Update ()
    {
        if (teleportScript.teleporting)
            return;

        UpdateGroundDist();

        Spin();
	}
}

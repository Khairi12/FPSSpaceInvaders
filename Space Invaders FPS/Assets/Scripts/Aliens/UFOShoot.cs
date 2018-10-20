using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShoot : MonoBehaviour
{
    public WeaponData weapon;

    public float minFireInterval = 1f;
    public float maxFireInterval = 10f;

    public float minErrorAmount = 0.1f;
    public float maxErrorAmount = 2.5f;

    private UFOTeleport teleportScript;
    private Transform playerTransform;
    private float outFireInterval;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        teleportScript = transform.parent.parent.GetComponent<UFOTeleport>();
    }

    private void Start()
    {
        outFireInterval = Random.Range(minFireInterval, maxFireInterval);
    }

    private void LaunchProjectile(WeaponData data)
    {
        GameObject bulletObject = Instantiate(data.bulletModel);
        Vector3 gunBarrel = transform.GetChild(0).GetChild(0).position;
        Bullet bullet = bulletObject.transform.GetChild(0).GetComponent<Bullet>();

        Quaternion approximateAim = transform.localRotation *
            Quaternion.AngleAxis(Random.Range(minErrorAmount, maxErrorAmount), Vector3.up) *
            Quaternion.AngleAxis(Random.Range(minErrorAmount, maxErrorAmount), Vector3.right);

        bulletObject.transform.SetPositionAndRotation(gunBarrel, approximateAim);
        bullet.SetWeaponData(data);
        bullet.Fire();
    }

    private void UpdateFireInterval()
    {
        outFireInterval -= Time.deltaTime;

        if (outFireInterval <= 0f)
        {
            outFireInterval = Random.Range(minFireInterval, maxFireInterval);
            LaunchProjectile(weapon);
        }
    }

    private void Update()
    {
        if (teleportScript.teleporting)
            return;

        transform.parent.rotation = Quaternion.identity;
        transform.LookAt(playerTransform.position);

        UpdateFireInterval();
    }
}

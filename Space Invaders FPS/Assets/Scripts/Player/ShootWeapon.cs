using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public event Action<WeaponData> OnWeaponFire;

    public List<WeaponData> SkillInventory;

    public int skillIndex = 0;

    private AmmunitionManager playerAmmo;

    // -----------------------------------------------------------------
    // PUBLIC
    // -----------------------------------------------------------------

    public void Clear()
    {
        OnWeaponFire = null;
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<AmmunitionManager>();
    }

    private void OnEnable()
    {
        OnWeaponFire += LaunchProjectile;
    }

    private void OnDisable()
    {
        OnWeaponFire -= LaunchProjectile;
    }

    private void LaunchProjectile(WeaponData data)
    {
        GameObject bulletObject = Instantiate(data.bulletModel);
        Vector3 gunBarrel = transform.GetChild(0).transform.position;
        Bullet bullet = bulletObject.transform.GetChild(0).GetComponent<Bullet>();

        bulletObject.transform.SetPositionAndRotation(gunBarrel, (Camera.main.transform.rotation));
        bullet.SetWeaponData(data);
        bullet.Fire();

        playerAmmo.UseAmmo(data);
    }

    private void SwapProjectile()
    {
        if (skillIndex + 1 < SkillInventory.Count)
            skillIndex += 1;
        else
            skillIndex = 0;
    }

    private void Update()
    {
        if (OnWeaponFire == null)
            return;

        if (Input.GetMouseButtonDown(1))
            SwapProjectile();

        if (Input.GetMouseButtonDown(0) && playerAmmo.curAmmo > 0f)
            OnWeaponFire(SkillInventory[skillIndex]);
    }
}

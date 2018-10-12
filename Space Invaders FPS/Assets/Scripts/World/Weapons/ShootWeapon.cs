using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public event Action<WeaponData> OnWeaponFire;

    public List<WeaponData> SkillInventory;

    // -----------------------------------------------------------------
    // PUBLIC
    // -----------------------------------------------------------------

    public void Clear()
    {
        OnWeaponFire = null;
    }

    private void LaunchProjectile(WeaponData data)
    {
        GameObject bulletObject = Instantiate(data.bulletModel);
        Vector3 gunBarrel = transform.GetChild(0).GetChild(0).position;
        Bullet bullet = bulletObject.transform.GetChild(0).GetComponent<Bullet>();

        bulletObject.transform.SetPositionAndRotation(gunBarrel, transform.localRotation);
        bullet.SetWeaponData(data);
        bullet.Fire();
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void OnEnable()
    {
        OnWeaponFire += LaunchProjectile;
    }

    private void OnDisable()
    {
        OnWeaponFire -= LaunchProjectile;
    }

    private void Update()
    {
        if (OnWeaponFire == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && SkillInventory.Count >= 1)
        {
            OnWeaponFire(SkillInventory[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && SkillInventory.Count >= 2)
        {
            OnWeaponFire(SkillInventory[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && SkillInventory.Count >= 3)
        {
            OnWeaponFire(SkillInventory[2]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionManager : MonoBehaviour
{
    public bool noBullets { get; private set; }

    public delegate void Damage();
    public event Damage OnAmmoUse;

    public float curAmmo = 25f;

    public void UseAmmo(WeaponData data)
    {
        if (curAmmo - data.ammoCost > 0f)
        {
            curAmmo -= data.ammoCost;

            if (OnAmmoUse != null)
                OnAmmoUse();
        }
        else
        {
            noBullets = true;
            curAmmo = 0f;
        }
    }

    public void GetAmmo(float gain)
    {
        if (curAmmo + gain > 100f)
        {
            curAmmo = 100f;
        }
        else
        {
            curAmmo += gain;
        }

        if (OnAmmoUse != null)
            OnAmmoUse();

        if (noBullets)
            noBullets = false;
    }

    private void Awake()
    {
        noBullets = false;
    }
}

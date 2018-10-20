using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static void HealthItem(Transform target, float amount)
    {
        HealthManager hm = target.GetComponent<HealthManager>();

        hm.TakeHeal(amount);
    }

    public static void AmmoItem(Transform target, float amount)
    {
        AmmunitionManager am = target.GetComponent<AmmunitionManager>();

        am.GetAmmo(amount);
    }
}

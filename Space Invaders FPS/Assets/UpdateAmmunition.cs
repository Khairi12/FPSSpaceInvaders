using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAmmunition : MonoBehaviour
{
    private AmmunitionManager playerAmmo;
    private Slider ammoBar;

    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<AmmunitionManager>();
        ammoBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        playerAmmo.OnAmmoUse += UpdateDisplay;
    }

    private void OnDisable()
    {
        playerAmmo.OnAmmoUse -= UpdateDisplay;
    }

    private void Start()
    {
        ammoBar.maxValue = playerAmmo.curAmmo;
        ammoBar.value = playerAmmo.curAmmo;
    }

    private void UpdateDisplay()
    {
        ammoBar.value = playerAmmo.curAmmo;
    }
}

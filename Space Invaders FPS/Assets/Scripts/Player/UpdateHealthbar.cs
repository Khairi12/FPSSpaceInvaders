using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthbar : MonoBehaviour
{
    private HealthManager playerHealth;
    private Slider healthBar;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        healthBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        playerHealth.OnDamage += UpdateDisplay;
    }

    private void OnDisable()
    {
        playerHealth.OnDamage -= UpdateDisplay;
    }

    private void Start()
    {
        healthBar.maxValue = playerHealth.curHealth;
        healthBar.value = playerHealth.curHealth;
    }

    private void UpdateDisplay()
    {
        healthBar.value = playerHealth.curHealth;
    }
}

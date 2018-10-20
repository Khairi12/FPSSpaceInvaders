using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void OnEnable()
    {
        healthManager.OnDeath += Death;
    }

    private void OnDisable()
    {
        healthManager.OnDeath -= Death;
    }

    private void Death()
    {
        GameManager.gameManager.ChangeLevel(2);
    }
}

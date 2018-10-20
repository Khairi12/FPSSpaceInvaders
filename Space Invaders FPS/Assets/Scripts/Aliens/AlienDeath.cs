using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDeath : MonoBehaviour
{
    private Animator anim;
    private HealthManager healthManager;
    private AlienMovement alienMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        alienMovement = GetComponent<AlienMovement>();
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
        anim.SetBool("Dead", true);
        alienMovement.ToggleMovement(false);
        GameManager.gameManager.AddAlienKill();

        Destroy(gameObject, 3f);
    }
}

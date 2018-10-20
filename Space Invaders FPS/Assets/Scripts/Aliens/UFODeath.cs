using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFODeath : MonoBehaviour
{
    private HealthManager healthManager;
    private GameObject stableCube;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        stableCube = Resources.Load<GameObject>("Prefabs/CubeStable");
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
        Transform children = transform.parent.GetComponentInChildren<Transform>();

        foreach (Transform child in children)
        {
            GameObject cube = Instantiate(stableCube, child.position, Quaternion.identity);
            HealthManager cubeHM = cube.GetComponent<HealthManager>();
            BreakCube breakCube = cube.GetComponent<BreakCube>();

            breakCube.explosionPower = 750f;
            breakCube.explosionRadius = 5f;
            breakCube.SetOrigin(child.position);

            cubeHM.Kill();
        }

        GameManager.gameManager.AddUFOKill();
        Destroy(transform.parent.gameObject);
    }
}

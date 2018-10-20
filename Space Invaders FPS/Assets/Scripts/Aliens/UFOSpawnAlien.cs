using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawnAlien : MonoBehaviour
{
    private GameObject alienSpawn;
    private UFOLanding uFOLanding;

    private void Awake()
    {
        alienSpawn = Resources.Load<GameObject>("Prefabs/Alien");
        uFOLanding = GetComponent<UFOLanding>();
    }

    private void OnEnable()
    {
        uFOLanding.OnLand += SpawnEnemy;
    }

    private void OnDisable()
    {
        uFOLanding.OnLand -= SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Instantiate(alienSpawn, hit.point, Quaternion.identity);
        }
    }
}

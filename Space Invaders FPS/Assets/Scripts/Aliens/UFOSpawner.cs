using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public float spawnTime = 5f;
    public float spawnAmount = 5f;

    private GameObject toSpawn;
    private float outSpawnTime;

    private void Awake()
    {
        toSpawn = Resources.Load<GameObject>("Prefabs/UFO");
        outSpawnTime = 0f;
    }

    private void Update()
    {
        if (outSpawnTime <= 0f)
        {
            outSpawnTime = spawnTime;
            Instantiate(toSpawn, transform);
        }
        else
        {
            outSpawnTime -= Time.deltaTime;
        }
    }
}

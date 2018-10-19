using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [HideInInspector]
    public enum SpawnType { Health, Ammo }

    public SpawnType spawnType = SpawnType.Health;
    public float spawnInterval = 5f;

    private float outSpawnInterval = 5f;
    private bool itemSpawned = false;

    private void SpawnItem()
    {
        switch (spawnType)
        {
            case SpawnType.Ammo:
                break;
            case SpawnType.Health:
                break;
        }
    }

    private void Awake()
    {
        outSpawnInterval = spawnInterval;
    }

    private void Update()
    {
        if (outSpawnInterval <= 0f)
        {
            outSpawnInterval = spawnInterval;
            itemSpawned = true;

            SpawnItem();
        }
        else
        {
            outSpawnInterval -= Time.deltaTime;
        }
    }
}

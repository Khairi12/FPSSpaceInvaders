using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float spawnInterval = 5f;

    private Item item;
    private GameObject toSpawn;
    private float outSpawnInterval = 5f;
    private bool itemSpawned = true;

    public void StartSpawningItem()
    {
        itemSpawned = false;
    }

    private void SpawnItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Ammo:
                toSpawn = Resources.Load<GameObject>("Prefabs/AmmoItem");
                break;
            case Item.ItemType.Health:
                toSpawn = Resources.Load<GameObject>("Prefabs/HealthItem");
                break;
        }

        Instantiate(toSpawn, transform.GetChild(0));
    }

    private void Awake()
    {
        outSpawnInterval = spawnInterval;
        item = transform.GetChild(0).GetChild(0).GetComponent<Item>();

    }

    private void Update()
    {
        if (itemSpawned)
            return;

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

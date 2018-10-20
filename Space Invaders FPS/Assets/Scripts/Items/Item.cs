using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public enum ItemType { Health, Ammo }

    public ItemType itemType = ItemType.Health;

    public int minAmount = 1;
    public int maxAmount = 10;

    private ItemSpawner parentSpawner;

    private void Awake()
    {
        parentSpawner = transform.parent.parent.GetComponent<ItemSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player")
            return;

        switch (itemType)
        {
            case ItemType.Ammo:
                ItemManager.AmmoItem(collision.transform, Random.Range(minAmount, maxAmount));
                break;
            case ItemType.Health:
                ItemManager.HealthItem(collision.transform, Random.Range(minAmount, maxAmount));
                break;
        }

        parentSpawner.StartSpawningItem();

        Destroy(gameObject);
    }
}

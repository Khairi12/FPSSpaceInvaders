using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]

public class BreakCube : MonoBehaviour
{
    public float explosionRadius = 10f;
    public float explosionPower = 500f;

    private HealthManager transformHM;
    private GameObject destroyedPrefab;
    private Vector3 origin;

    public void Destruct()
    {
        Destroy(gameObject);

        SpawnDestroyableCube();

        Collider[] colliders = Physics.OverlapSphere(origin, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit.tag == "Cube")
            {
                rb.AddExplosionForce(explosionPower, origin, explosionRadius, 1f);
                Destroy(hit.gameObject, 5f);
            }
        }
    }

    public void SetOrigin(Vector3 position)
    {
        origin = position;
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Awake()
    {
        transformHM = GetComponent<HealthManager>();
        destroyedPrefab = Resources.Load<GameObject>("Prefabs/CubeBroken");
    }

    private void OnEnable()
    {
        transformHM.OnDeath += Destruct;
    }

    private void OnDisable()
    {
        transformHM.OnDeath -= Destruct;
    }

    private void Start()
    {
        origin = transform.position;
    }

    private void SpawnDestroyableCube()
    {
        GameObject destroyedCube = Instantiate(destroyedPrefab, origin, Quaternion.identity);

        Destroy(destroyedCube, 5f);
    }
}

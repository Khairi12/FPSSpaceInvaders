using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]

public class BreakCube : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosionPower = 500f;

    private HealthManager transformHM;
    private GameObject destroyedPrefab;
    private Vector3 origin;

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Awake()
    {
        transformHM = GetComponent<HealthManager>();
        destroyedPrefab = Resources.Load<GameObject>("Prefabs/BrokenCube");
    }

    private void OnEnable()
    {
        transformHM.OnDeath += Destruction;
    }

    private void OnDisable()
    {
        transformHM.OnDeath -= Destruction;
    }

    private void Start()
    {
        origin = transform.position;
    }

    private void SpawnDestroyableCube()
    {
        GameObject destroyedCube = Instantiate(destroyedPrefab);
        destroyedCube.transform.SetPositionAndRotation(origin, Quaternion.identity);

        Destroy(destroyedCube, 5f);
    }

    private void Destruction()
    {
        Destroy(gameObject);

        SpawnDestroyableCube();

        Collider[] colliders = Physics.OverlapSphere(origin, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit.tag == "Cube")
                rb.AddExplosionForce(explosionPower, origin, explosionRadius, 1f);

            Destroy(hit.gameObject, 5f);
        }
    }
}

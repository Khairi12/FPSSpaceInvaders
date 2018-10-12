using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBdy;
    private WeaponData weaponData;

    // -----------------------------------------------------------------
    // PUBLIC
    // -----------------------------------------------------------------
    
    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
    }

    public void Fire()
    {
        rigidBdy.AddForce(transform.up * weaponData.projectileSpeed, ForceMode.VelocityChange);
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Awake()
    {
        rigidBdy = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(transform.parent.gameObject, weaponData.lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthManager colHM = collision.transform.GetComponent<HealthManager>();

        if (colHM != null)
        {
            colHM.TakeDamage(weaponData);
            Destroy(transform.gameObject);
        }
    }
}

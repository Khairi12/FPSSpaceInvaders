using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public delegate void Death();
    public event Death OnDeath;

    [SerializeField] private float curHealth = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float minHealth = 0f;

    // -----------------------------------------------------------------
    // PUBLIC
    // -----------------------------------------------------------------

    public void TakeDamage(WeaponData data)
    {
        if (curHealth - data.physicalDamage <= 0f && OnDeath != null)
        {
            OnDeath();
        }
        else
        {
            curHealth -= data.physicalDamage;
        }
    }

    public void TakeHeal(WeaponData data)
    {
        curHealth += data.magicalDamage;
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Start()
    {

    }
}

using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public delegate void Death();
    public event Death OnDeath;

    public delegate void Damage();
    public event Damage OnDamage;

    public float curHealth = 100f;

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

            if (OnDamage != null)
                OnDamage();
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

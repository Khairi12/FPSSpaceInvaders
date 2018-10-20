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

    public void Kill()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
    }

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

    public void TakeDamage(float amount)
    {
        if (curHealth - amount <= 0f && OnDeath != null)
        {
            OnDeath();
        }
        else
        {
            curHealth -= amount;

            if (OnDamage != null)
                OnDamage();
        }
    }

    public void TakeHeal(float amount)
    {
        if (curHealth + amount > 100f)
        {
            curHealth = 100f;
        }
        else
        {
            curHealth += amount;
        }

        if (OnDamage != null)
            OnDamage();
    }
}

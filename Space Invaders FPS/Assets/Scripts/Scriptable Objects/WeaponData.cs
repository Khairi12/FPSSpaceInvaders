using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Data")]

public class WeaponData : ScriptableObject
{
    [Header("Weapon Models")]

    public GameObject gunModel;
    public GameObject bulletModel;

    [Header("Weapon Stats")]

    public float physicalDamage;
    public float magicalDamage;

    public float range;
    public float piercing;
    public float attackSpeed;
    public float projectileSpeed;

    public float lifeTime;
}

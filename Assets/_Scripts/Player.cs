using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player instance;

    public static List<GameObject> weaponsList;

    [SerializeField] private DamageableSO damageableSO;
    [SerializeField] private HealthbarUI healthbarUI;

    private float health;
    private float damage;
    private float moveSpeed;
    private float maxHealth;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private void Awake()
    {
        instance = this;

        weaponsList = new List<GameObject>();
        
        Health = damageableSO.health;
        Damage = damageableSO.damage;
        MoveSpeed = damageableSO.moveSpeed;

        maxHealth = Health;
    }

    private void OnEnable()
    {
        ActionManager.OnWeaponAdded += SetWeapon;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        healthbarUI.ChangeFillAmount(Health / maxHealth);
    }

    private void SetWeapon(GameObject weapon)
    {
        weaponsList.Add(weapon);
        WeaponManager.instance.SetWeaponPlaces(weaponsList, transform);
    }
}
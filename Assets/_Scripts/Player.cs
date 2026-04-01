using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player instance;

    public static List<GameObject> weaponsList;

    [SerializeField] private DamageableSO damageableSO;
    [SerializeField] private HealthbarUI healthbarUI;
    [SerializeField] private GameObject pistolTest1;
    [SerializeField] private GameObject pistolTest2;
    [SerializeField] private GameObject pistolTest3;
    [SerializeField] private GameObject pistolTest4;

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

        weaponsList.Add(pistolTest1);
        weaponsList.Add(pistolTest2);
        weaponsList.Add(pistolTest3);

        Health = damageableSO.health;
        Damage = damageableSO.damage;
        MoveSpeed = damageableSO.moveSpeed;

        maxHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        healthbarUI.ChangeFillAmount(Health / maxHealth);
    }

    private void SetWeapon()
    {
        ActionManager.OnWeaponAdded?.Invoke(weaponsList, transform);
    }
}
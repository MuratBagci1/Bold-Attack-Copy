using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    private Player playerInstance;

    [SerializeField] private DamageableSO damageableSO;
    [SerializeField] private HealthbarUI healthbarUI;
    [SerializeField] private GameObject xpPrefab;
    private float attackTimer = 0f;

    public float health;
    public float damage;
    public float moveSpeed;
    private float attackCooldown; 
    private float attackRange;

    public float maxHealth;

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

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    private void Awake()
    {
        Health = damageableSO.health;
        Damage = damageableSO.damage;
        MoveSpeed = damageableSO.moveSpeed; 
        AttackCooldown = damageableSO.attackCooldown;
        AttackRange = damageableSO.attackRange;

        maxHealth = Health;
    }

    private void Start()
    {
        playerInstance = Player.instance;
    }

    private void Update()
    {
        Vector3 target = playerInstance.transform.position;
        float distance = Vector3.Distance(transform.position, target);

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
        }
        else
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                playerInstance.TakeDamage(Damage);
                attackTimer = attackCooldown;
            }
        }
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthbarUI.ChangeFillAmount(Health / maxHealth);
        if (Health <= 0)
        {
            DropEXP();

            PoolManager.Instance.Return<Enemy>(this);
        }
    }

    private void DropEXP()
    {
        int xpAmount = UnityEngine.Random.Range(1, 3);

        for (int i = 0; i < xpAmount; i++)
        {
            ExperiencePoint createdXP = PoolManager.Instance.Get<ExperiencePoint>(xpPrefab.GetComponent<ExperiencePoint>());

            createdXP.SetXPPosition(transform);
        }
    }

    public void OnSpawn()
    {
        transform.position = EnemySpawnManager.instance.GiveSpawnPosition();

        Health = damageableSO.health;
        Damage = damageableSO.damage;
        MoveSpeed = damageableSO.moveSpeed;

        healthbarUI.ChangeFillAmount(Health / maxHealth);
    }

    public void OnDespawn()
    {
        EnemySensorCollider.detectedEnemyList.Remove(this.transform);
    }
}
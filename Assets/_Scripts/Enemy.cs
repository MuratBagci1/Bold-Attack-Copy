using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    private Player playerInstance;

    public float moveSpeed;

    public float health;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
        //Health = 100f;
    }

    private void Start()
    {
        playerInstance = Player.instance;
    }

    private void Update()
    {
        Vector3 target = playerInstance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Damage Taken: " + damage + " new Health: " + health);
        if (health <= 0)
        {
            PoolManager.Instance.Return<Enemy>(this);
        }
    }

    public void OnSpawn()
    {
        transform.position = EnemySpawnManager.instance.GiveSpawnPosition();
        Debug.Log("OnSpawn");
    }

    public void OnDespawn()
    {
        Debug.Log("OnDespawn");
    }
}
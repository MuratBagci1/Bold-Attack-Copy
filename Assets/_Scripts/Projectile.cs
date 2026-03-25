using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float projectileSpeed;
    private float damage;

    private Vector3 targetPosition;
    private Transform target;

    private void Update()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            targetPosition = target.position;
        }
        else
        {
            targetPosition = Vector3.zero; 
        }

        if (targetPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);
        }
        else
        {
            PoolManager.Instance.Return<Projectile>(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IDamageable>()?.TakeDamage(damage);

        PoolManager.Instance.Return<Projectile>(this);
    }

    public void OnDespawn()
    {
    }

    public void OnSpawn()
    {
    }

    public void Shoot(Transform target, Vector3 spawnPoint, float damage)
    {
        //Debug.Log("Shoot Çaalýþtý");

        //if (target.position == Vector3.zero)
        //{
        //    PoolManager.Instance.Return<Projectile>(this);

        //    return;
        //}

        this.target = target;

        transform.position = spawnPoint;

        this.damage = damage;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player instance;

    private float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Damage Taken: " + damage + "new Health: " + Health);
    }
}
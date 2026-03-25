using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public abstract float Health { get; set; }

    public abstract void TakeDamage(float damage);
}
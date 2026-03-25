using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageableScriptableObject", order = 3)]
public class DamageableSO : ScriptableObject
{
    public string damageableName;
    public float health;
    public float damage;
    public float moveSpeed;
    public float attackCooldown;
    public float attackRange;
}
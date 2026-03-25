using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    [SerializeField] private WeaponSO weaponSO;
    private float weaponDamage;
    private float weaponCooldown;
    private float weaponCooldownTimer;

    PoolManager poolManager;

    private void Awake()
    {
        weaponDamage = 25;
        weaponCooldown = weaponSO.cooldown;
        weaponCooldownTimer = 0;
    }

    private void Start()
    {
        poolManager = PoolManager.Instance;

        poolManager.CreatePool<Projectile>(projectilePrefab.GetComponent<Projectile>(), 5);
    }

    private void Update()
    {
        if(weaponCooldownTimer > 0)
        {
            weaponCooldownTimer -= Time.deltaTime;
        }
        else if(EnemySensorCollider.detectedEnemyList.Count > 0)
        {
            ShootProjectile();
            weaponCooldownTimer = weaponCooldown;
        }
    }

    private void ShootProjectile()
    {
        Projectile shutProjectile = poolManager.Get<Projectile>(projectilePrefab.GetComponent<Projectile>());

        shutProjectile.Shoot(GetClosestEnemy(), projectileSpawnPoint.position, weaponDamage);
    }

    private Transform GetClosestEnemy()
    {
        Transform closestEnemy = EnemySensorCollider.detectedEnemyList[0].transform;

        foreach (Transform enemyGO in EnemySensorCollider.detectedEnemyList)
        {
            closestEnemy = (Vector3.Distance(enemyGO.transform.position, transform.position) <= Vector3.Distance(closestEnemy.position, transform.position)) ? enemyGO : closestEnemy;
        }

        return closestEnemy;
    }
}
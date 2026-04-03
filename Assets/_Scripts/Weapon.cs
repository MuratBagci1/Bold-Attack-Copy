using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    //working on, look at weaponSO and pistolSO
    private float weaponDamage;
    private float weaponCooldown;
    private float weaponCooldownTimer;

    private PoolManager poolManager;
    private Player playerInstance;

    private void Awake()
    {
        weaponCooldownTimer = 0;
    }

    private void Start()
    {
        poolManager = PoolManager.Instance;

        playerInstance = Player.instance;

        poolManager.CreatePool<Projectile>(projectilePrefab.GetComponent<Projectile>(), 5);
    }

    public void Innit(float damage, float cooldown)
    {
        weaponDamage = damage;
        weaponCooldown = cooldown;
    }
    private void Update()
    {
        if (weaponCooldownTimer > 0)
        {
            if (EnemySensorCollider.detectedEnemyList.Count > 0)
            {
                RotateTowards(GetClosestEnemy());
            }

            weaponCooldownTimer -= Time.deltaTime;
        }
        else if (EnemySensorCollider.detectedEnemyList.Count > 0)
        {
            ShootProjectile();
            weaponCooldownTimer = weaponCooldown;
        }
    }

    private void ShootProjectile()
    {
        Projectile shutProjectile = poolManager.Get<Projectile>(projectilePrefab.GetComponent<Projectile>());

        shutProjectile.Shoot(GetClosestEnemy(), projectileSpawnPoint.position, weaponDamage * playerInstance.Damage);
        Debug.Log("damage: " + weaponDamage.ToString());
    }

    private Transform GetClosestEnemy()
    {
        Transform closestEnemy;

        if (EnemySensorCollider.detectedEnemyList.Count == 0)
            return null;
        else
            closestEnemy = EnemySensorCollider.detectedEnemyList[0].transform;

        foreach (Transform enemyGO in EnemySensorCollider.detectedEnemyList)
        {
            closestEnemy = ((enemyGO.transform.position - transform.position).sqrMagnitude <= (closestEnemy.position - transform.position).sqrMagnitude) ? enemyGO : closestEnemy;
        }

        return closestEnemy;
    }

    private void RotateTowards(Transform target)
    {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        float rotationSpeed = 180f;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
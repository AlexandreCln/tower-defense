using UnityEngine;

public class Construction : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]

    public float rotationSpeed = 20f;
    public float fireRate = 1f;
    public float fireCountdown = 1f;
    public float range = 15f;

    [Header("Setup Fields")]

    public Transform rotatePoint;
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        // Euler angles suffer from "gimbal lock", a loss of 1 degree when 2 of 3 axis 
        // moves in the same direction, that's why Unity store all rotations as Quaternions.
        // Quarternions's functions manage x,y,z,w components (not axis) together as we must never ajust them individually.
        // LookRotation is a quaternion's roration aligned with the Vector3 passed in,
        // that represent how do turret needs to rotate to look in target direction.
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Convert it to eulerAngles to use only the Y axis (instead of uaternion directly).
        // Lerp is for Linear Interpolation, it interpolate 2 normalized quaternions 
        // (unit between 0 and 1 calculated by the 4 components) over time. 
        // The 3rd parameter is  percentage wanted between the 2 quaternions (rotations).
        Vector3 rotation = Quaternion.Lerp(rotatePoint.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        // creates a new quaternion on Y axis only
        rotatePoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        // get the projectile prefab script
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
            projectile.Seek(target);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

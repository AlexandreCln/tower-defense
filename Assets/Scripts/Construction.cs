using UnityEngine;

public class Construction : MonoBehaviour
{
    private Transform target;
    // Enemy component
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    public float rotationSpeed = 20f;

    [Header("Use Projectile (default)")]
    public GameObject projectilePrefab = null;
    public float fireRate = 1f;
    public float fireCountdown = 1f;

    [Header("Use Laser")]
    public  bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public int damagePerSecond;
    public float slowPct = .5f;

    [Header("Setup Fields")]
    public Transform rotatePoint;
    public string enemyTag = "Enemy";
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                LaserOff();
            }

            return;
        }

        LookOnTarget();

        if (useLaser)
        {
            LaserOn();
        }
        else {
            Projectile();
        }
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
        GameObject nearestEnemyTarget = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemyTarget = enemy;
            }
        }

        if (nearestEnemyTarget != null && shortestDistance <= range)
        {
            target = nearestEnemyTarget.transform;
            Enemy nearestEnemy = target.GetComponent<Enemy>();

            if (useLaser && targetEnemy != null && nearestEnemy != targetEnemy)
                targetEnemy.StopSlow();
            
            targetEnemy = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void LookOnTarget()
    {
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
    }

    void LaserOn()
    {
        targetEnemy.Slow(slowPct);
        targetEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
        
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 effectDirection = target.position - transform.position;
        // a Vector3 normalized correspond to 1, that is also fortunally the enemy radius
        impactEffect.transform.position = target.position - (effectDirection.normalized);
        impactEffect.transform.rotation = Quaternion.LookRotation(effectDirection * -1);
    
        if (impactEffect.isStopped)
        {
            impactEffect.Play();
        }
    }

    void LaserOff()
    {
        if (targetEnemy)
            targetEnemy.StopSlow();

        if (!lineRenderer.enabled)
            return;
            
        // Stop method allows to stop emmision and keep already spawned particles
        lineRenderer.enabled = false;
        impactEffect.Stop();

    }

    void Projectile()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

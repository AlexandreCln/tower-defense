using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;
    public float explosionRadius = 0f;
    public int damage = 50;

    [Header("Unity Setup Fields")]
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        // calculate a constant speed
        float distanceThisFrame = speed * Time.deltaTime;
        bool gonnaReachTargetThisFrame = dir.magnitude <= distanceThisFrame;

        if (gonnaReachTargetThisFrame)
        {
            HitTarget();
            return;
        }

        // move projectile relative to the world space, not local space
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // rotate projectile
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        float effectDuration = impactEffect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effectIns, effectDuration);

        // Handle AOE or single target
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Damage(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // can use filter mask instead of tags to filter enemies
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

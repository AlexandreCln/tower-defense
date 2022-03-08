using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int moneyGain = 50;
    public GameObject deathEffect;

    private Transform targetWavepoint;
    private Vector3 dir;
    private int wavepointIndex = 0;

    void Start ()
    {
        targetWavepoint = Waypoints.points[0];
        UpdateDir();
    }

    // Update is called every frame
    void Update ()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWavepoint.position) <=0.2f)
        {
            GetNextWaypoint();
            UpdateDir();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        float effectDuration = deathEffect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, effectDuration);
        PlayerStats.Money += moneyGain;
        Destroy(gameObject);
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            // Endpath destroy an object, this can take some time, so it's nessesary to return void here
            return;
        }
        wavepointIndex++;
        targetWavepoint = Waypoints.points[wavepointIndex];
    }

    void UpdateDir()
    {
        dir = targetWavepoint.position - transform.position;
    }

    void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.Lives -= 1;
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    // speed need to be accessible, but we want to set only startSpeed in the Inspector
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int moneyGain = 50;
    public GameObject deathEffect;

    void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    public void StopSlow()
    {
        speed = startSpeed;
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        float effectDuration = deathEffect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, effectDuration);
        PlayerStats.Money += moneyGain;
        Destroy(gameObject);
    }
}

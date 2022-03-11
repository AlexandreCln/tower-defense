using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform targetWavepoint;
    private int wavepointIndex = 0;
    
    void Start ()
    {
        // If this script is attached to a Enemy, it is injected by RequireComponent Attribute
        enemy = GetComponent<Enemy>();
        targetWavepoint = Waypoints.points[0];
    }
    void Update ()
    {
        Vector3 dir = targetWavepoint.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWavepoint.position) <=0.2f)
        {
            GetNextWaypoint();
        }
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
    void EndPath()
    {
        Destroy(gameObject);

        if (PlayerStats.Lives > 0)
            PlayerStats.Lives--;
    }
}

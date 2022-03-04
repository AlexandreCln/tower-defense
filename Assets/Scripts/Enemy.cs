using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

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

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            // Destroy an object can take some time, so it's nessesary to return; here
            return;
        }
        wavepointIndex++;
        targetWavepoint = Waypoints.points[wavepointIndex];
    }

    void UpdateDir()
    {
        dir = targetWavepoint.position - transform.position;
    }
}

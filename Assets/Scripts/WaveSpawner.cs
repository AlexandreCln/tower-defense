using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public Text waveCountdownFractionText;
    public float timeBetweenWaves = 20.5f;

    private float countdown = 3.5f;
    private int waveIndex = 0;

    void Update ()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);

        if (0 == countdown)
            return;

        int unitCountdown = (int)countdown;
        float fractionCountdown = countdown - unitCountdown;

        waveCountdownText.text = unitCountdown.ToString();
        waveCountdownFractionText.text = "." + fractionCountdown.ToString().Substring(2, 2);
    }

    // https://docs.unity3d.com/Manual/Coroutines.html
    IEnumerator SpawnWave ()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy ()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

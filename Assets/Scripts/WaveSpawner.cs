using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class WaveSpawner : MonoBehaviour
{
    public WaveData[] waves;
    public WaveData[] superWaves; // New array for super waves

    public int curWave = 0;
    private float waveStartTime; // Track wave start time
    private bool superWaveButtonActive = false; // Track if super wave button should be active

    public int remainingEnemies;

    [Header("Components")]
    public Transform enemySpawnPos;
    public TextMeshProUGUI waveText;
    public GameObject nextWaveButton;
    public GameObject superWaveButton; // New button for super waves

    private void Start()
    {
        //remainingEnemies = waves[curWave ].enemySets[0].spawnCount;
        //waveText.text = $"Wave: {curWave + 1}";
        //StartCoroutine(SpawnWave());
    }

    public void SpawnNextWave()
    {
        curWave++;

        if (curWave-1 == waves.Length)
            return;

        waveText.text = $"Wave: {curWave}";

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        remainingEnemies = waves[curWave - 1].enemySets[0].spawnCount;
        nextWaveButton.SetActive(false);
        superWaveButton.SetActive(false); // Hide super wave button

        waveStartTime = Time.time; // Record wave start time

        WaveData wave = waves[curWave - 1];

        for (int x = 0; x < wave.enemySets.Length; x++)
        {
            yield return new WaitForSeconds(wave.enemySets[x].spawnDelay);

            for (int y = 0; y < wave.enemySets[x].spawnCount; y++)
            {
                SpawnEnemy(wave.enemySets[x].enemyPrefab);
                yield return new WaitForSeconds(wave.enemySets[x].spawnRate);
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPos.position, Quaternion.identity);
        //remainingEnemies++;
    }

    public void OnEnemyDestroyed()
    {
        remainingEnemies--;

        if (remainingEnemies == 0)
        {
            float waveDuration = Time.time - waveStartTime; // Calculate wave duration

            if (waveDuration < 3f && curWave < waves.Length - 1)
            {
                superWaveButtonActive = true; // Allow super wave button activation
                superWaveButton.SetActive(true);
            }
            else
            {
                superWaveButtonActive = false;
                nextWaveButton.SetActive(true);
            }
        }
    }

    public void TriggerSuperWaves()
    {
        if (superWaveButtonActive)
        {
            curWave++;
            if (curWave-1 == waves.Length)
                return;

            waveText.text = $"Wave: {curWave}";
            StartCoroutine(SpawnSuperWaves());
            superWaveButtonActive = false; // Reset the flag
            superWaveButton.SetActive(false); // Hide the super wave button again
        }
    }

    IEnumerator SpawnSuperWaves()
    {
        remainingEnemies = superWaves[curWave - 1].enemySets[0].spawnCount;
        // Spawn super waves
        for (int i = 0; i < superWaves.Length; i++)
        {
            WaveData superWave = superWaves[i];

            for (int x = 0; x < superWave.enemySets.Length; x++)
            {
                yield return new WaitForSeconds(superWave.enemySets[x].spawnDelay);

                for (int y = 0; y < superWave.enemySets[x].spawnCount; y++)
                {
                    SpawnEnemy(superWave.enemySets[x].enemyPrefab);
                    yield return new WaitForSeconds(superWave.enemySets[x].spawnRate);
                }
            }
        }
    }
}

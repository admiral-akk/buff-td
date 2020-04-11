using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager waveManager;
    public GameObject startNode;
    public GameObject enemyPrefab;
    public Wave[] waves;


    public Text countDownText;
    public Text currentWaveText;
    public Text nextWaveText;

    private float timeToStart = 10f;
    private float timeBetweenWaves = 4f;
    private float countDown;
    private int currentWave = 0;
    private int numEnemiesRemaining;

    private void Start()
    {
        if (waveManager != null)
        {
            Debug.Log("MULTIPLE WAVE MANAGERS!!!");
            return;
        }
        SetWaveText();
        waveManager = this;
        countDown = timeToStart;
        Invoke("SpawnWave", timeToStart);
    }

    private void SetWaveText()
    {
        currentWaveText.text = string.Format("Current: {0} enemy, {1} per sec", waves[currentWave].enemyCount, waves[currentWave].spawnRate);
        if (currentWave == waves.Length - 1)
        {
            nextWaveText.text = string.Format("Next: Victory!");
        } else
        {
            nextWaveText.text = string.Format("Next: {0} enemy, {1} per sec", waves[currentWave + 1].enemyCount, waves[currentWave + 1].spawnRate);

        }
    }

    private void UpdateCountdownText()
    {
        countDownText.text = string.Format("{0:00.00}s", countDown);
    }

    private void Update()
    {
        UpdateCountdownText();

        if (!GameManager.gameManager.isGameOver) { 
            countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);
    }
    }

    private void SpawnWave()
    {
        if (!GameManager.gameManager.isGameOver)
        {
            numEnemiesRemaining = waves[currentWave].enemyCount;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        SpawnEnemy();
        for (int i = 1; i < waves[currentWave].enemyCount; i++)
        {
            yield return new WaitForSeconds(1f / waves[currentWave].spawnRate);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, startNode.transform.position, Quaternion.identity);
    }

    public void EnemyDeath()
    {
        numEnemiesRemaining--;
        if (numEnemiesRemaining == 0)
        {
            currentWave++;
            if (currentWave >= waves.Length)
            {
                GameManager.gameManager.GameWin();
            } else
            {
                SetWaveText();
                countDown = timeBetweenWaves;
                Invoke("SpawnWave", timeBetweenWaves);
            }
        }
    }
    
}

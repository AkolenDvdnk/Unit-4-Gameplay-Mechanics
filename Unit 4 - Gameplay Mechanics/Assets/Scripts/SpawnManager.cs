using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public float bossPowerupSpawnRate = 3f;

    public int bossRound;

    public GameObject[] powerups;
    public GameObject boss;
    public GameObject[] enemies;

    private float spawnRange = 9f;

    private float countdown;
    private int enemyCount;
    private int waveNumber;

    private bool isBossRound = false;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        SpawnWave();
        BossPowerup();
    }
    private void SpawnBoss()
    {
        Instantiate(boss, GenerateSpawnPosition(), boss.transform.rotation);
        return;
    }
    private void SpawnWave()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0 && waveNumber < bossRound)
        {
            PlayerAbility.instance.currentPowerup = PowerupType.None;
            PlayerAbility.instance.hasPowerup = false;
            waveNumber++;

            if (waveNumber == bossRound)
            {
                isBossRound = true;
                SpawnBoss();
            }
            else
            {
                SpawnEnemy(waveNumber);
            }

            SpawnPowerup();
        }
    }
    private void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyIndex], GenerateSpawnPosition(), enemies[enemyIndex].transform.rotation);
        }
    }
    private void SpawnPowerup()
    {
        if (!isBossRound)
        {
            int powerupIndex = Random.Range(0, powerups.Length);
            Instantiate(powerups[powerupIndex], GenerateSpawnPosition(), powerups[powerupIndex].transform.rotation);
        }
    }
    private void BossPowerup()
    {
        if (Boss.instance != null)
        {
            if (isBossRound)
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0)
                {
                    countdown = bossPowerupSpawnRate;
                    int powerupIndex = Random.Range(0, powerups.Length);
                    Instantiate(powerups[powerupIndex], GenerateSpawnPosition(), powerups[powerupIndex].transform.rotation);
                }
            }
        }
    }
    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
}

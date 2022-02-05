using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public int bossRound;

    public GameObject[] powerups;
    public GameObject boss;
    public GameObject[] enemies;

    private float spawnRange = 9f;

    private int enemyCount;
    private int waveNumber;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        SpawnWave();   
    }
    private void SpawnBoss()
    {
        Instantiate(boss, GenerateSpawnPosition(), boss.transform.rotation);
        return;
    }
    private void SpawnWave()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;

            if (waveNumber == bossRound)
            {
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
        int powerupIndex = Random.Range(0, powerups.Length);
        Instantiate(powerups[powerupIndex], GenerateSpawnPosition(), powerups[powerupIndex].transform.rotation);
    }
    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
}

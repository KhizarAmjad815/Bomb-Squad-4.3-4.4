using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 6;

    public int enemyCount;
    public int waveNumber = 1;
    public TMP_Text waveText;

    public GameObject minePrefab;
    public GameObject healPotionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemies();
        Instantiate(minePrefab, GeneratePickupSpawnPosition(), minePrefab.transform.rotation);
        Instantiate(healPotionPrefab, GeneratePickupSpawnPosition(), minePrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);
        waveText.text = "Wave: " + waveNumber.ToString();
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = 8.0f;
        return new Vector3(spawnPosX, -0.6f, spawnPosZ);
    }

    private Vector3 GeneratePickupSpawnPosition()
    {
        float spawnPosX = Random.Range(-9, 9);
        float spawnPosZ = Random.Range(-2, spawnRange);
        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }

    void SpawnEnemyWave(int enemiestoSpawn)
    {
        for (int i = 0; i < enemiestoSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            waveText.text = "Wave: " + waveNumber.ToString();
            int enemiesToSpawn = 0;

            if (waveNumber >= 1 && waveNumber <= 2)
                enemiesToSpawn = 1;
            else if (waveNumber >= 3 && waveNumber <= 4)
                enemiesToSpawn = 2;
            else
                enemiesToSpawn = 3;

            Instantiate(minePrefab, GenerateSpawnPosition(), minePrefab.transform.rotation);
            Instantiate(healPotionPrefab, GeneratePickupSpawnPosition(), minePrefab.transform.rotation);
            SpawnEnemyWave(enemiesToSpawn);
        }
    }
}

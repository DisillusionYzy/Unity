using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float spawnRangeX = 9.0f;
    public float spawnRangeZ = 9.0f;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber++);
        SpawnPowerup(1);
    }

    // Update is called once per frame
    void Update()
    {
        var count = FindObjectsOfType<Enemy>().Length;
        if (count == 0)
        {
            SpawnEnemyWave(waveNumber++);
            SpawnPowerup(1);
        }
    }

    private Vector3 GenRandomPos()
    {
        var x = Random.Range(-spawnRangeX, spawnRangeX);
        var z = Random.Range(-spawnRangeZ, spawnRangeZ);
        return new Vector3(x, 0, z);
    }

    private void SpawnEnemyWave(int enemyNum)
    {
        for (int i = 0; i < enemyNum; ++i)
        {
            Instantiate(enemyPrefab, GenRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup(int num)
    {
        for (int i = 0; i < num; ++i)
        {
            Instantiate(powerupPrefab, GenRandomPos(), powerupPrefab.transform.rotation);
        }
    }
}

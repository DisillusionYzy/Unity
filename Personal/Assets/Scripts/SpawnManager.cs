using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;
    public float zEnemySpawn = 13.0f;
    public float xSpawnRange = 8.0f;
    public float zPowerupSpawn = 3.0f;
    public float ySpawn = 0.5f;
    public float startDelay = 1.0f;
    public float enemySpawnItv = 1.0f;
    public float powerupSpawnItv = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnItv);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnItv);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        var idx = Random.Range(0, enemies.Length);
        var e = enemies[idx];
        var x = Random.Range(-xSpawnRange, xSpawnRange);
        Instantiate(e, new Vector3(x, ySpawn, zEnemySpawn), e.transform.rotation);
    }

    private void SpawnPowerup()
    {
        var x = Random.Range(-xSpawnRange, xSpawnRange);
        var z = Random.Range(-zPowerupSpawn, zPowerupSpawn);
        Instantiate(powerup, new Vector3(x, ySpawn, z), powerup.transform.rotation);
    }
}

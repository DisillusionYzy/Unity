using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector3 spawnPos = new Vector3(25, 0, 0);
    public float spawnIntervalMin = 1.0f;
    public float spawnIntervalMax = 3.0f;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        var itv = Random.Range(spawnIntervalMin, spawnIntervalMax);
        Invoke("SpawnObstacle", itv);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!playerController.gameOver)
        {
            var idx = Random.Range(0, obstaclePrefabs.Length);
            var obstacle = obstaclePrefabs[idx];
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);

            var itv = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Invoke("SpawnObstacle", itv);
        }
    }
}

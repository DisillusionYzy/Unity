using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    public float spawnLimitXLeft = -22;
    public float spawnLimitXRight = 7;
    public float spawnPosY = 35;

    private float startDelay = 1.0f;
    private float spawnMinInterval = 3.0f;
    private float spawnMaxInterval = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRandomBall", startDelay);
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall()
    {
        // Generate random ball index and random spawn position
        var spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        var idx = Random.Range(0, ballPrefabs.Length);
        var ball = ballPrefabs[idx];
        // instantiate ball at random spawn location
        Instantiate(ball, spawnPos, ball.transform.rotation);

        Invoke("SpawnRandomBall", Random.Range(spawnMinInterval, spawnMaxInterval));
    }

}

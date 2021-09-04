using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float spawnDogInterval = 0.5f;
    private float lastSpawnTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        var now = Time.time;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space)
            && now >= (lastSpawnTime + spawnDogInterval))
        {
            lastSpawnTime = now;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}

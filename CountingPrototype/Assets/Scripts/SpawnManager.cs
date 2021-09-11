using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float zBound = 22.0f;
    private float ySpawn = 24.0f;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            var z = Random.Range(-zBound, zBound);
            Instantiate(ball, new Vector3(0, ySpawn, z), ball.transform.rotation);
        }
    }
}

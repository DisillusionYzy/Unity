using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int Count = 0;
    [SerializeField]
    private float speed = 10.0f;
    private SpawnManager spawnManager;

    private void Start()
    {
        Count = 0;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        CounterText.text = "Count : " + Count;
        Destroy(other.gameObject);
    }

    private void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * hInput * speed * Time.deltaTime);
        if (transform.position.z < -spawnManager.zBound)
        {
            var p = transform.position;
            transform.position = new Vector3(p.x, p.y, -spawnManager.zBound);
        }
        else if (transform.position.z > spawnManager.zBound)
        {
            var p = transform.position;
            transform.position = new Vector3(p.x, p.y, spawnManager.zBound);
        }
    }
}

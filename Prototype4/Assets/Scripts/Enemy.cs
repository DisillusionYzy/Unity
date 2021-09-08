using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private float destroyY = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(dir * speed);

        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}

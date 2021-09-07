using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float zBound = 4.5f;
    private Rigidbody playerRb;
    // public float horizontalInput;
    // public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 限制位置的方式没有cube墙好
        if (transform.position.z < -zBound)
        {
            var pos = transform.position;
            pos.z = -zBound;
            transform.position = pos;
        } else if (transform.position.z > zBound)
        {
            var pos = transform.position;
            pos.z = zBound;
            transform.position = pos;
        }

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.right * speed * horizontalInput);
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float zBound = 4.5f;
    private Rigidbody playerRb;
    private Animator playerAnim;

    enum PlayerState
    {
        kStatic = 0,
        kWalk = 1,
        kRun = 2
    };
    private PlayerState playerState = PlayerState.kStatic;

    // public float horizontalInput;
    // public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        if (playerAnim)
        {
            playerAnim.SetFloat("Speed_f", 0.6f);
            playerAnim.SetBool("Static_b", true);
        }
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
        }
        else if (transform.position.z > zBound)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collide with enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            Debug.Log("Got a powerup");
            Destroy(other.gameObject);
        }
    }
}

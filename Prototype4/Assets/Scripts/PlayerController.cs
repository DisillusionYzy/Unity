using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public float powerupStrength = 15.0f;
    public float powerupCountdown = 3.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * vertical);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collide with: " + collision.gameObject.name + " with hasPowerup set to " + hasPowerup);
        
            if (hasPowerup)
            {
                var enemy = collision.gameObject;
                var enemyRb = enemy.GetComponent<Rigidbody>();
                var dir = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(dir * powerupStrength, ForceMode.Impulse);
            }    
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupCountdown);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}

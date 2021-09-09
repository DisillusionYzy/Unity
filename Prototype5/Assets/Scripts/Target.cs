using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private float torqueStength = 10.0f;
    private float upForceMin = 12.0f;
    private float upForceMax = 15.0f;
    private float xRange = 4.0f;
    private float ySpawn = -1.0f;
    public int point = 5;
    public int subHp = 0;
    public ParticleSystem explosionParticle;
    public AudioClip soundClip;
    private AudioSource soundPlay;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.position = GenSpawnPos();
        rb.AddForce(GenForce(), ForceMode.Impulse);
        rb.AddTorque(GenToque(), GenToque(), GenToque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deadline")
        {
            Destroy(gameObject);

            if (gameObject.tag == "Good" && !gameManager.IsGameOver)
            {
                gameManager.SubHp(subHp);
            }
        }

    }

    private void OnMouseDown()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        gameManager.UpdateScore(point);
        Instantiate(explosionParticle, transform.position, transform.rotation);
        soundPlay.PlayOneShot(soundClip);

        if (gameObject.tag == "Bad")
        {
            gameManager.GameOver();
        }

        Destroy(gameObject);
    }

    private Vector3 GenForce()
    {
        return Vector3.up * Random.Range(upForceMin, upForceMax);
    }

    private float GenToque()
    {
        return Random.Range(0, torqueStength);
    }

    private Vector3 GenSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawn, 0);
    }
}

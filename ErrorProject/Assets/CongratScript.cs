using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem sparksParticles;

    private List<string> textToDisplay;

    private float rotatingSpeed;
    private float timeToNextText;

    private int currentText;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextText = 0.0f;
        currentText = 0;

        rotatingSpeed = 1.0f;

        textToDisplay = new List<string>();
        textToDisplay.Add("Congratulation");
        textToDisplay.Add("All Errors Fixed");

        text.text = textToDisplay[0];

        sparksParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextText += Time.deltaTime;

        if (timeToNextText > 1.5f)
        {
            Debug.Log(timeToNextText);
            timeToNextText = 0.0f;

            currentText = (currentText + 1) % textToDisplay.Count;
            text.text = textToDisplay[currentText];
        }

        text.transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);
    }
}
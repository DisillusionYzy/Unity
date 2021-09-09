using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI hpText;
    public Button restartButton;
    public GameObject titleScreen;

    private int score = 0;
    private int hp = 3;
    public float spawnRate = 1.0f;
    public float ySpawnInit = -10.0f;
    private bool isGameOver = false;
    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        SubHp(0);

        gameoverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }
        gameoverText.gameObject.SetActive(true);
        isGameOver = true;
        restartButton.gameObject.SetActive(true);

        hp = 0;
        SubHp(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototype 5");
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            var idx = Random.Range(0, targets.Count);
            Instantiate(targets[idx], new Vector3(0, ySpawnInit, 0), targets[idx].transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void SubHp(int hpToSub)
    {
        hp -= hpToSub;
        if (hp <= 0)
        {
            hp = 0;
            GameOver();
        }
        hpText.text = "Hp: " + hp;
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());

        titleScreen.SetActive(false);
    }
}

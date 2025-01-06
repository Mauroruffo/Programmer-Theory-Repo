using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 5;

    public TextMeshProUGUI textMeshLives;
    public TextMeshProUGUI textMeshScore;
    public TextMeshProUGUI textMeshGameOver;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        textMeshLives.text = "Lives = " + lives;
        textMeshScore.text = "Score = " + score;

    }

    // Update is called once per frame
    void Update()
    {
        textMeshLives.text = "Lives = " + lives;
        textMeshScore.text = "Score = " + score;

    }

    public void addLives(int value)
    {
        lives += value;

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            textMeshGameOver.enabled = true;
            lives = 0;
            destroyAnimals();
            Destroy(player);
        }
        Debug.Log("Lives = " + lives);

    }

    public void addScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
    }

    void destroyAnimals()
    {
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals) { Destroy(animal); }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current in-game values
    public int score = 0;
    private int lives = 5;

    // HUD
    public TextMeshProUGUI textMeshLives;
    public TextMeshProUGUI textMeshScore;
    public TextMeshProUGUI textMeshGameOver;

    // Fields to display player info
    public TextMeshProUGUI CurrentPlayerName;
    public TextMeshProUGUI BestPlayerNamesAndScore;

    // Player
    public GameObject player;

    // Static variables for holding best player info
    private static int BestScore;
    private static string BestPlayer;

    // Pause variables
    public GameObject pauseScreen;
    public bool paused;

    // Gameover HUD
    private bool gameOver;
    public Button restartButton;
    public Button menuButton;

    // Gameover Particles
    public ParticleSystem particleExplosion;

    // Spawn Manager
    SpawnManager spawnManager;
    int checkpointScore = 0;

    // SFX
    private AudioSource audioSource;
    public AudioClip hit;
    public AudioClip gameOverSound;
    public AudioClip pushedSound;

    private void Awake()
    {
        LoadGameRank();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();
        gameOver = false;
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        textMeshLives.text = "Lives = " + lives;
        textMeshScore.text = "Score = " + score;
        CurrentPlayerName.text = DataHandler.Instance.PlayerName;
        SetBestPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshLives.text = "Lives = " + lives;
        textMeshScore.text = "Score = " + score;
        // Check if game is paused
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            CheckForPaused();
        }

        // Increase difficulty
        if (score % 100 == 0 && checkpointScore != score)
        {
            Debug.Log("Increased spawn rate");
            spawnManager.restartSpawn = true;
            checkpointScore = score;
        }

    }

    public void addLives(int value)
    {
        lives += value;

        if (lives <= 0)
        {
            GameOver();
        }
        Debug.Log("Lives = " + lives);
    }

    public void addScore(int value)
    {
        score += value;
        DataHandler.Instance.Score = score;
        Debug.Log("Score = " + score);
    }

    void destroyAnimals()
    {
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals) { Destroy(animal); }
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayer = data.TheBestPlayer;
            BestScore = data.HighestScore;
        }
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerNamesAndScore.text = "";
        }
        else
        {
            BestPlayerNamesAndScore.text = $"Best Score \n {BestPlayer}: {BestScore}";
        }
    }

    private void CheckBestPlayer()
    {
        int currentScore = DataHandler.Instance.Score;

        if (currentScore > BestScore)
        {
            BestPlayer = DataHandler.Instance.PlayerName;
            BestScore = currentScore;

            BestPlayerNamesAndScore.text = $"Best Score \n {BestPlayer}: {BestScore}";

            SaveGameRank(BestPlayer, BestScore);
        }
    }

    public void SaveGameRank(string bestPlayerName, int bestPlayerScore)
    {
        SaveData data = new SaveData();
        data.TheBestPlayer = bestPlayerName;
        data.HighestScore = bestPlayerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    [System.Serializable]
    class SaveData
    {
        public int HighestScore;
        public string TheBestPlayer;
    }

    void CheckForPaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        // Load Main Menu
        SceneManager.LoadScene(0);
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        gameOver = true;
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        textMeshGameOver.enabled = true;
        lives = 0;
        destroyAnimals();
        GameOverExplosion();
        Destroy(player);
        CheckBestPlayer();
    }

    void GameOverExplosion()
    {
        ParticleSystem explosionInstance = Instantiate(particleExplosion, player.transform.position, player.transform.rotation);
        explosionInstance.Play();
        Destroy(explosionInstance, 6);
    }

    public void HitSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(hit);
    }

    public void Pushed()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(pushedSound);
    }

}

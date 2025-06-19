using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;


public class GameManager : MonoBehaviour
{
    // Ref
    public GameObject[] downPrefabs;
    public GameObject gameOverUI;
    public GameObject timersUI;
    private Transform playerTransform;

    // Spawn Ranges
    private float spawnRangeX = 15.0f;
    private float spawnPosY = 30f;
    
    // Spawn Timers
    private float startDelay = 0f;
    private float spawnInterval = 0.6f;

    // Countdown Down Timer
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    // Countup timers
    public int countupTime;
    public TextMeshProUGUI countupDisplay;

    // Score displays
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highscoreDisplay;

    // Floats/Ints
    private float lowerBound = 1.1f;
    private int savedHighScore;

    // Bools
    public static bool gameOver = false;

    public void Start() // Start is called on first frame
    {
        StartCoroutine(CountdownToStart());
        StartCoroutine(DelayedAudioStart()); // Required so Audio Manager loads in before playing sounds

        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highscoreDisplay.text = savedHighScore.ToString();
    }

    void StartGame() // Starts the game / spawns platforms
    {
        // Spawns platforms repeatedly
        InvokeRepeating("SpawnRandomPlatformDown", startDelay, spawnInterval);
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    // Spawns platforms randomly
    void SpawnRandomPlatformDown()
    {
        // Integer picks a random platform 
        int downPlatform = Random.Range(0, downPrefabs.Length);
        // Vector3 picks a random location
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, Random.Range(-spawnRangeX, spawnRangeX));

        // Spawns random platform at random location
        Instantiate(downPrefabs[downPlatform], spawnPos, downPrefabs[downPlatform].transform.rotation);
    }

    
    IEnumerator DelayedAudioStart() // Required so Audio Manager loads in before playing sounds
    {
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<AudioManager>()?.Play("Tick");
    }

    
    // Countdowns from 3 then plays Startgame() function
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            FindObjectOfType<AudioManager>().Play("Tick");
            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        FindObjectOfType<AudioManager>().Play("GO");

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        gameOver = false;
        StartGame();
        StartCoroutine(TimerStart());
    }

    // Starts a counter going up for score
    IEnumerator TimerStart()
    {
        while (gameOver == false)
        {
            countupDisplay.text = countupTime.ToString();

            yield return new WaitForSeconds(0.1f);

            countupTime++;
        }

        // Game set to gameover
        FindObjectOfType<AudioManager>().Play("GameOver");
        UnityEngine.Debug.Log("Gameover...");
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        scoreDisplay.text = (countupTime - 1).ToString(); // -1 required cause it adds a extra point to the score for fun
        ScoreCheck();
    }

    public void ReplayingScene() // Finds current scene name and reloads it when called
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
        gameOver = false;
    }

    void Update()
    {

        GameObject player = GameObject.Find("Player"); // Find the player GameObject by name and get its Transform

        if (player != null)
        {
            playerTransform = player.transform;
        }

        if (playerTransform.position.y < lowerBound) // Sets gameover to true when player dies
        {
            gameOver = true;
            timersUI.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("Theme");
        }
    }

    void ScoreCheck()
    {
        int currentScore = int.Parse(scoreDisplay.text);// Save current string numbers to int
        int highScore = int.Parse(highscoreDisplay.text);

        if (currentScore > highScore) // checks if score is more then highscore and if true, updates it
        {
            highscoreDisplay.text = currentScore.ToString();

            PlayerPrefs.SetInt("HighScore", currentScore); // Save the new HighScore
            PlayerPrefs.Save(); // Saves HighScore
            UnityEngine.Debug.Log("Highscore updated and saved");
        }
        else
        {
            // Display the existing saved high score
            highscoreDisplay.text = savedHighScore.ToString();
            UnityEngine.Debug.Log("High Score not updated");
        }
    }

}
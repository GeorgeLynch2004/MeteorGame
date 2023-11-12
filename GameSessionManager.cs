using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundUI;
    [SerializeField] public float currentScore;
    [SerializeField] public float maxScore;
    [SerializeField] public float currentTime;
    [SerializeField] public float maxTime;
    [SerializeField] public float spawnsPerSecond;
    [SerializeField] private string saveFilePath;

    [SerializeField] private GameObject spawner;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;

    [SerializeField] public bool freezeGame;
    [SerializeField] private bool gameRunning;

    private float timer;
    [SerializeField] private float timeToWait;
    private bool timerActive;


    private void Start() 
    {
        Time.timeScale = 0;   
        currentTime = 0;
        currentScore = 0;
        gameUI.SetActive(false);
        timerActive = false;
        gameRunning = false;

        // Reads in the previous max socre and time from the text file "save.txt" to ensure accuracy between sessions.

        using(StreamReader reader = new StreamReader(saveFilePath))
        {
            maxScore = int.Parse(reader.ReadLine());
            maxTime = float.Parse(reader.ReadLine());
        }
    }

    private void Update() 
    {
        spawnsPerSecond = 1f / spawner.GetComponent<ObjectSpawner>().timeBetweenSpawns;

        currentTime += Time.deltaTime;

        // Checks for GAME OVER condition every frame.
        if (player.GetComponent<HealthSystem>() != null)
        {
            if (player.GetComponent<HealthSystem>().currentHealth <= 0)
            {
                gameOver();
            }
        }

        // Code used to start and stop the game.
        if (Input.GetKeyDown(KeyCode.Escape) && gameRunning)
        {
            pauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !gameRunning)
        {
            startGame();
        }

        // Code used if the player beats any of their previous records.
        if (maxScore < currentScore)
        {
            maxScore = currentScore;
            writeToFile();
        }
        if (maxTime < currentTime)
        {
            maxTime = currentTime;
            writeToFile();
        }

        // Adds a delay from when the user dies to when the level is reset.
        if (timerActive)
        {
            timer += Time.deltaTime;
        }
        if (timer >= timeToWait)
        {
            SceneManager.LoadScene(0);
        }

        currentScore = player.GetComponent<PlayerMovement>().coinCount;
    }

    public void startGame()
    {
        // Toggles the necessary UI
        Time.timeScale = 1;
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        gameRunning = true;
        soundUI.Play();
        backgroundMusic.Play();
    }

    public void pauseGame()
    {
        // Toggles the necessary UI
        Time.timeScale = 0;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        gameRunning = false;
        backgroundMusic.Pause();
        soundUI.Play();
    }

    public void gameOver()
    {
        freezeGame = true;
        timerActive = true;
        gameRunning = false;
        backgroundMusic.Stop();
    }

    public void closeGame()
    {
        Application.Quit();
    }

    public void writeToFile()
    {
        // Called if there is a change in the highscore or max time alive.
        using (StreamWriter writer = new StreamWriter(saveFilePath))
        {
            writer.WriteLine(maxScore);
            writer.WriteLine(maxTime);
        }
    }
}

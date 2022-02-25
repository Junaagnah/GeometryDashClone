using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas StartingCount;
    public Canvas Menu;
    public Canvas GameOver;

    private bool gameStarted = false;
    private GameObject Player;
    private float elapsedTime = 0f;
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        // Stopping player movement
        Player.GetComponent<Player>().gamePaused = true;

        GameOver.enabled = false;
        Menu.enabled = false;
        StartingCount.enabled = true;

        // Hiding 2 & 1 texts
        StartingCount.transform.GetChild(1).GetComponent<Text>().enabled = false;
        StartingCount.transform.GetChild(2).GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!gameStarted)
        {
            CountDown();
        } else
        {
            if (Player != null)
            {
                // Pause
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                }
            } else
            {
                // If the gameobject player is null then he is dead, show the Game Over canvas
                GameOver.enabled = true;
            }

            // Reset the level when the player presses R
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void CountDown()
    {
        // Countdown starts at 2 secondes after the scene is loaded
        if (elapsedTime >= 1.0f && elapsedTime < 2.0f)
        {
            StartingCount.transform.GetChild(0).GetComponent<Text>().enabled = false;
            StartingCount.transform.GetChild(1).GetComponent<Text>().enabled = true;
        }
        else if (elapsedTime >= 2.0f && elapsedTime < 3.0f)
        {
            StartingCount.transform.GetChild(1).GetComponent<Text>().enabled = false;
            StartingCount.transform.GetChild(2).GetComponent<Text>().enabled = true;
        }
        else if (elapsedTime >= 3.0f)
        {
            StartingCount.transform.GetChild(2).GetComponent<Text>().enabled = false;
            // Starting player movement
            Player.GetComponent<Player>().gamePaused = false;
            gameStarted = true;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            Menu.enabled = true;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            Menu.enabled = false;
        }

        Player.GetComponent<Player>().gamePaused = isPaused;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas StartingCount;
    public Canvas Menu;

    private bool gameStarted = false;
    private GameObject Player;

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        // Stopping player movement

        Menu.enabled = false;
        StartingCount.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        Debug.Log(elapsedTime);

        if (!gameStarted)
        {  
            // Countdown starts at 2 secondes after the scene is loaded
            switch(elapsedTime)
            {
                case 3.0f:
                    StartingCount.GetComponents<Text>()[0].enabled = false;
                    StartingCount.GetComponents<Text>()[1].enabled = true;
                    break;
                case 4.0f:
                    StartingCount.GetComponents<Text>()[1].enabled = false;
                    StartingCount.GetComponents<Text>()[2].enabled = true;
                    break;
                case 5.0f:
                    StartingCount.GetComponents<Text>()[2].enabled = false;
                    // Starting player movement
                    gameStarted = true;
                    break;
            }

        } else
        {
            // Reset the level when the player presses R
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}

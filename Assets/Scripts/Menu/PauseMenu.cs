using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameManu;
    public GameObject gameOverMenu;

    private void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseGameManu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;

        PlayerPrefs.DeleteKey("Balance");
        PlayerPrefs.DeleteKey("Water");
        PlayerPrefs.DeleteKey("Day");
        PlayerPrefs.DeleteKey("WaterSum");


        PlayerPrefs.DeleteKey("Islands");
        PlayerPrefs.DeleteKey("PlacedIslands");
        PlayerPrefs.DeleteKey("KeyObjects");
        PlayerPrefs.DeleteKey("ValueObjects");

        PlayerPrefs.DeleteKey("PaymentCost");
        PlayerPrefs.DeleteKey("PaymentDay");
        PlayerPrefs.DeleteKey("IslandCost");

        PlayerPrefs.DeleteKey("Inventory");

        PlayerPrefs.DeleteKey("CardsInHand");
        PlayerPrefs.DeleteKey("IndexInRotation");
        PlayerPrefs.DeleteKey("CardsInHand");
    }

    public void Resume()
    {
        pauseGameManu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("Balance"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
      
        }
        else
        {
            Debug.Log("У вас нет сохранений");
        }
       
    }
    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void NewPlayGame()
    {
        PlayerPrefs.DeleteKey("Balance");
        PlayerPrefs.DeleteKey("Water");
        PlayerPrefs.DeleteKey("Day");
        PlayerPrefs.DeleteKey("WaterSum");


        PlayerPrefs.DeleteKey("Islands");
        PlayerPrefs.DeleteKey("PlacedIslands");
        PlayerPrefs.DeleteKey("KeyObjects");
        PlayerPrefs.DeleteKey("ValueObjects");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

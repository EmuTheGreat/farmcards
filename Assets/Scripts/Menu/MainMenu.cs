using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button buttonStartNoNew;
    public void PlayGame()
    {

        if (PlayerPrefs.HasKey("Balance"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
      
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

        PlayerPrefs.DeleteKey("PaymentCost");
        PlayerPrefs.DeleteKey("PaymentDay");
        PlayerPrefs.DeleteKey("IslandCost");

        PlayerPrefs.DeleteKey("Inventory");

        PlayerPrefs.DeleteKey("CardsInHand");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Update()
    {
        if (!PlayerPrefs.HasKey("Balance"))
        {
            buttonStartNoNew.interactable = false;
        }
    }
}

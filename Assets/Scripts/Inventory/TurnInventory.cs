using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInventory : MonoBehaviour
{
    private bool buttonClicked = false;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private IslandsPlacementSystem islandPlacement;

    private void Start()
    {
        inventory.SetActive(false);
    }

    public void ShowInventory()
    {
        islandPlacement.StopPlacement();
        inputManager.CancelPlace();
        buttonClicked = !buttonClicked;
        inventory.SetActive(buttonClicked);
    }
}

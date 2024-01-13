using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private InventoryManager inventoryManager; 
    [SerializeField]
    private TMP_Text sellPrice;


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
        UpdateSellPrice();
    }

    public void UpdateSellPrice()
    {
        sellPrice.text = inventoryManager.ShowSellPrice().ToString();
    }
}

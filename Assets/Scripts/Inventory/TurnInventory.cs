using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInventory : MonoBehaviour
{
    private bool buttonClicked = false;
    [SerializeField]
    private GameObject inventory;

    private void Start()
    {
        inventory.SetActive(false);
    }

    public void ShowInventory()
    {
        buttonClicked = !buttonClicked;
        inventory.SetActive(buttonClicked);
    }
}

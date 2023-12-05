using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private InterfaceManager interfaceManager;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private PlacementSystem placementSystem;


    public void NextDay()
    {
        Harvest();
        interfaceManager.SetDay(1);
        interfaceManager.SetWater();
    }


    private void Harvest()
    {
        foreach (PlacementData placementObject in placementSystem.placementData.placedObjects.Values)
        {
            if (dataBase.objectsData[placementObject.ID].Item != null)
            {
                inventoryManager.AddItem(dataBase.objectsData[placementObject.ID].Item);
            }
        }
    }
}

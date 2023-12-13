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
    [SerializeField]
    private IslandsColliders islandsList;

    private List<IslandBuilding> islands = new();
    private int islandsCounter = 0;

    private void Start()
    {
        islandsCounter = islandsList.transform.childCount;
        islands = GetIslands();
    }

    public void NextDay()
    {
        islands = GetIslands();
        Harvest1();
        interfaceManager.SetDay(1);
        interfaceManager.SetWater();
    }

    private void Harvest1()
    {
        foreach (IslandBuilding island in islands)
        {
            foreach (Vector2Int placedObject in island.placedObjects)
            {
                PlacementData data;
                if (placementSystem.placementData.placedObjects.TryGetValue(placedObject, out data))
                {
                    if (dataBase.objectsData[data.ID].Item != null)
                    {
                        inventoryManager.AddItem(dataBase.objectsData[data.ID].Item);
                    }
                }
            }
        }
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

    private List<IslandBuilding> GetIslands()
    {
        if (islandsCounter != islandsList.transform.childCount)
        {
            islandsCounter = islandsList.transform.childCount;
            List<IslandBuilding> result = new();
            for (int i = 0; i < islandsList.transform.childCount; i++)
            {
                result.Add(islandsList.transform.GetChild(i).GetComponent<IslandBuilding>());
            }
            return result;
        }
        return islands;
    }
}

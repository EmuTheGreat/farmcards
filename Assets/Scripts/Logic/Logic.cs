using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class Logic : MonoBehaviour, ISaveState
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private InterfaceManager interfaceManager;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private TurnInventory turnInventory;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private IslandsColliders islandsList;
    [SerializeField]
    private GameObject objectsList;
    [SerializeField]
    private GetCard getCard;
    [SerializeField]
    private PauseMenu pauseMenu;

    private List<IslandBuilding> islands;
    private int islandsCounter = 0;
    public int paymentCost = 100;
    public int paymentDay = 10;
    private HashSet<Vector2> occupied;
    private HashSet<Claster4> clasters4;

    private int dayCounter;
    private int waterCounter;

    private List<ItemScriptableObject> itemsToAdd = new();

    private void Awake()
    {
        clasters4 = new HashSet<Claster4>();
        islands = new();
        occupied = new HashSet<Vector2>();
        dayCounter = 0;
        waterCounter = 0;
        if (PlayerPrefs.HasKey("PaymentCost"))
        {
            Load();
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("PaymentCost"))
        {
            Load();
        }
        islands = GetIslands();
        interfaceManager.SetPaymentsMessage(paymentCost, paymentDay - interfaceManager.day);
    }

    public void NextDay()
    {
        islands = GetIslands();
        GetClasters();
        Harvest1();
        HarvestAnimals();
        interfaceManager.SetDay(1);
        interfaceManager.SetWater();
        interfaceManager.SetBalance(-interfaceManager.buildingsCost);
        interfaceManager.SetPaymentsMessage(paymentCost, paymentDay - interfaceManager.day);
        UpdatePaymentsParams();
        turnInventory.UpdateSellPrice();

        if (interfaceManager.balance < 0)
        {
            dayCounter++;
        }
        else
        {
            dayCounter = 0;
        }

        if (interfaceManager.water < 0)
        {
            waterCounter++;
        }
        else
        {
            waterCounter = 0;
        }
        if (dayCounter == 4 || waterCounter == 4)
        {
            Debug.Log("Игра проиграна!");
            pauseMenu.GameOver();
        }
        Console.Clear();
        Debug.Log(waterCounter + "- вода");
        Debug.Log(dayCounter + "- деньги");
    }

    private void UpdatePaymentsParams()
    {
        if (paymentDay - interfaceManager.day == 0)
        {
            interfaceManager.SetBalance(-paymentCost);
            paymentCost += 20;
            paymentDay += 10;
            getCard.MakeChoice();
        }
    }

    private void GetClasters()
    {
        foreach (IslandBuilding island in islands)
        {
            var sortedList = island.placedObjects.OrderBy(x => x.y).ThenBy(x => x.x).ToList();
            foreach (var placedObject in sortedList)
            {
                PlacementData data;
                if (placementSystem.placementData.placedObjects.TryGetValue(placedObject, out data))
                {
                    if (dataBase.objectsData[data.ID].Type == ObjectType.Plant)
                    {
                        TryFindClaster4(data.ID, placedObject);
                    }
                }
            }
        }
        //Debug.Log(clasters4.Count);
    }

    private void Harvest1()
    {
        foreach (IslandBuilding island in islands)
        {
            var sortedList = island.placedObjects.OrderBy(x => x.y).ThenBy(x => x.x).ToList();
            foreach (var placedObject in sortedList)
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

        foreach (var claster in clasters4)
        {
            for (int i = 0; i < 2; i++)
            {
                if (dataBase.objectsData[claster.id].Item != null)
                {
                    inventoryManager.AddItem(dataBase.objectsData[claster.id].Item);
                }
            }
        }
    }

    public void ShowContainerCapacity()
    {
        foreach (var island in islands)
        {
            foreach (var container in island.animals)
            {
                for (int i = 0; i < objectsList.transform.childCount; i++)
                {
                    var animalContainer = objectsList.transform.GetChild(i).GetComponent<AnimalContainer>();
                    if (animalContainer != null)
                    {
                        if (animalContainer.occupiedPosition.SequenceEqual(container.occupiedPosition))
                        {
                            animalContainer.capacityText.text = $"{container.capacity}/4";
                            animalContainer.capacityText.gameObject.SetActive(true);
                        }
                        else
                        {
                            //foreach(var e in container.occupiedPosition)
                            //{
                            //    Debug.Log(e);
                            //}
                            //foreach (var e in animalContainer.occupiedPosition)
                            //{
                            //    Debug.Log(e);
                            //}
                        }
                    }
                }
            }
        }
    }

    public void HideContainerCapacity()
    {
        for (int i = 0; i < objectsList.transform.childCount; i++)
        {
            var animalContainer = objectsList.transform.GetChild(i).GetComponent<AnimalContainer>();
            if (animalContainer != null)
            {
                animalContainer.capacityText.gameObject.SetActive(false);
            }
        }
    }

    private void HarvestAnimals()
    {
        foreach (var island in islands)
        {
            foreach (var container in island.animals)
            {
                foreach (var animal in container.idOfAnimals)
                {
                    if (inventoryManager.DeleteItems(dataBase.objectsData[animal].FoodItemAmount, dataBase.objectsData[animal].FoodItem))
                    {
                        itemsToAdd.Add(dataBase.objectsData[animal].Item);
                    }
                }
            }
        }

        foreach (var item in itemsToAdd)
        {
            inventoryManager.AddItem(item);
        }
        itemsToAdd = new();
    }

    #region old code
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
    #endregion

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

    #region old code
    private bool TryFindGroupFour(int id, Vector2 pos)
    {
        List<Vector2Int> dirs = new() { new(1, 0), new(1, -1), new(0, -1) };
        foreach (var dir in dirs)
        {
            if (!placementSystem.placementData.placedObjects.ContainsKey(pos + dir) || placementSystem.placementData.placedObjects[pos + dir].ID != id)
            {
                return false;
            }
        }

        occupied.Add(pos);
        foreach (var dir in dirs)
        {
            if (dataBase.objectsData[id].Item != null)
            {
                inventoryManager.AddItem(dataBase.objectsData[id].Item);
                inventoryManager.AddItem(dataBase.objectsData[id].Item);
                occupied.Add(pos + dir);
            }
        }
        return true;
    }
    #endregion

    private bool TryFindClaster4(int id, Vector2 pos)
    {
        List<Vector2Int> dirs = new() { new(0, 0), new(1, 0), new(1, -1), new(0, -1) };
        foreach (var dir in dirs)
        {
            var newPos = pos + dir;

            if (clasters4.Any(x => x.objects.Contains(newPos)) || !placementSystem.placementData.placedObjects.ContainsKey(newPos) || placementSystem.placementData.placedObjects[newPos].ID != id)
            {
                return false;
            }
        }

        Claster4 claster = new Claster4();
        claster.id = id;
        claster.objects = new()
            {
                pos + dirs[0],
                pos + dirs[1],
                pos + dirs[2],
                pos + dirs[3],
            };
        clasters4.Add(claster);
        return true;
    }

    private bool TryFindAllIslands(List<Vector2Int> sortedList, int id) => sortedList.All(x => placementSystem.placementData.placedObjects[x].ID == id) && sortedList.Count == 16;

    public void Save()
    {
        PlayerPrefs.SetInt("PaymentCost", paymentCost);
        PlayerPrefs.SetInt("PaymentDay", paymentDay);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("PaymentCost"))
        {
            paymentCost = PlayerPrefs.GetInt("PaymentCost");
            paymentDay = PlayerPrefs.GetInt("PaymentDay");
        }
    }
}

public class Claster4
{
    public List<Vector2> objects;
    public int id;
}

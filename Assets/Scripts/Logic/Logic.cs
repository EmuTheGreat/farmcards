using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private IslandsColliders islandsList;

    private List<IslandBuilding> islands;
    private int islandsCounter = 0;
    public int paymentCost = 100;
    public int paymentDay = 10;
    private HashSet<Vector2> occupied;
    private HashSet<Claster4> clasters4;

    private void Awake()
    {
        clasters4 = new HashSet<Claster4>();
        islands = new();
        occupied = new HashSet<Vector2>();
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
        interfaceManager.SetDay(1);
        interfaceManager.SetWater();
        interfaceManager.SetBalance(-interfaceManager.buildingsCost);
        interfaceManager.SetPaymentsMessage(paymentCost, paymentDay - interfaceManager.day);
        UpdatePaymentsParams();
    }

    private void UpdatePaymentsParams()
    {
        if (paymentDay - interfaceManager.day == 0)
        {
            interfaceManager.SetBalance(-paymentCost);
            paymentCost += 20;
            paymentDay += 10;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class SaveObk : MonoBehaviour, ISaveState
{
    public GameObject islandlist;
    public GameObject islandPrefab;
    [SerializeField]
    private IslandsPlacementSystem islandsPlacementSystem;
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private ObjectsManager objectsManager;
    [SerializeField]
    private GameObject parentForObjects;



    public void Save()
    {
        //Переделать сохранение, адаптировать под животных!!!
        List<Island> islandBuildings = new List<Island>();
        for (var i = 0; i < islandlist.transform.childCount; i++)
        {
            var temp = islandlist.transform.GetChild(i).GetComponent<IslandBuilding>();
            Island island = new Island(temp.placedObjects, temp.transform.position, temp.animals);
            islandBuildings.Add(island);
        }
        string jsonIsland = JsonUtility.ToJson(new ListContainer<Island>(islandBuildings));
        string jsonPlacedIslands = JsonUtility.ToJson(new ListContainer<Vector2>(islandsPlacementSystem.islandsGridData.placedIslands));
        var keysList = placementSystem.placementData.placedObjects.Keys.ToList();
        string keyObjects = JsonUtility.ToJson(new ListContainer<Vector2>(keysList));
        var valuesList = placementSystem.placementData.placedObjects.Values.ToList();
        string valueObjects = JsonUtility.ToJson(new ListContainer<PlacementData>(valuesList));

        PlayerPrefs.SetString("Islands", jsonIsland);
        PlayerPrefs.SetString("PlacedIslands", jsonPlacedIslands);
        PlayerPrefs.SetString("KeyObjects", keyObjects);
        PlayerPrefs.SetString("ValueObjects", valueObjects);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Islands"))
        {
            string jsonIslands = PlayerPrefs.GetString("Islands");
            string jsonPlacedIslands = PlayerPrefs.GetString("PlacedIslands");
            string keyObjects = PlayerPrefs.GetString("KeyObjects");
            string valueObjects = PlayerPrefs.GetString("ValueObjects");

            ListContainer<Island> listContainer = JsonUtility.FromJson<ListContainer<Island>>(jsonIslands);
            ListContainer<Vector2> listPlacedIslands = JsonUtility.FromJson<ListContainer<Vector2>>(jsonPlacedIslands);
            ListContainer<Vector2> keyObjectsContainer = JsonUtility.FromJson<ListContainer<Vector2>>(keyObjects);
            ListContainer<PlacementData> valueObjectsContainer = JsonUtility.FromJson<ListContainer<PlacementData>>(valueObjects);

            Dictionary<Vector2, PlacementData> placedObjects = new Dictionary<Vector2, PlacementData>();

            for (int i = 0; i < keyObjectsContainer.list.Count; i++)
            {
                placedObjects.Add(keyObjectsContainer.list[i], valueObjectsContainer.list[i]);
            }

            placementSystem.placementData.placedObjects = placedObjects;

            var list = listContainer.list;

            islandsPlacementSystem.islandsGridData.placedIslands = listPlacedIslands.list;

            foreach (var island in list)
            {
                var loadIsland = Instantiate(islandPrefab, islandlist.transform);
                loadIsland.transform.position = island.islandPos;

                foreach (var placedObject in island.placementData)
                {
                    GameObject loadObject = Instantiate(dataBase.objectsData[placedObjects[placedObject].ID].Prefab, parentForObjects.transform);
                    loadObject.transform.position = placedObject;
                    var creatingIsland = loadIsland.GetComponent<IslandBuilding>();
                    creatingIsland.placedObjects.Add(placedObject);

                    if (placedObjects[placedObject].ID == 2)
                    {
                        AnimalContainerToSave animalContainer = new();
                        animalContainer.occupiedPosition = placedObjects[placedObject].occupiedPositions;
                        loadObject.GetComponent<AnimalContainer>().occupiedPosition = animalContainer.occupiedPosition;
                        //foreach (var e in occupied) Debug.Log(e);
                        //Debug.Log(island.animals.Count);
                    }
                }

                foreach (var animalContainer in island.animals)
                {
                    var creatingIsland = loadIsland.GetComponent<IslandBuilding>();
                    creatingIsland.animals.Add(animalContainer);
                    foreach (var animal in animalContainer.idOfAnimals)
                    {
                        GameObject newObject = Instantiate(dataBase.objectsData[animal].Prefab, parentForObjects.transform);
                        Vector2 position = new(animalContainer.occupiedPosition[1].x + 0.470f, animalContainer.occupiedPosition[1].y + 0.270f);
                        newObject.transform.position = position;
                    }
                }
            }
            objectsManager.UpdateDrawObjects();
        }
        //else
        //{
        //    Debug.Log("Сохранение не найдено");
        //}
    }
}

//Класс обёртка, т.к. JsonUnity не может сериализовать обычные списки.
[Serializable]
public class ListContainer<T>
{
    public List<T> list;

    public ListContainer()
    {
        list = new List<T>();
    }

    public ListContainer(List<T> list)
    {
        this.list = list;
    }
}

[Serializable]
public class AnimalContainerToSave
{
    public List<Vector2Int> occupiedPosition;
    public List<int> idOfAnimals = new();
    public int capacity = 4;
}

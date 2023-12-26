using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEditor;
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


    public void Save()
    {
        List<Island> islandBuildings = new List<Island>();
        for (var i = 0; i < islandlist.transform.childCount; i++)
        {
            var temp = islandlist.transform.GetChild(i).GetComponent<IslandBuilding>();
            Island island = new Island(temp.placedObjects, temp.transform.position);
            islandBuildings.Add(island);
        }
        string jsonIsland = JsonUtility.ToJson(new ListContainer<Island>(islandBuildings));
        string jsonPlacedIslands = JsonUtility.ToJson(new ListContainer<Vector2>(islandsPlacementSystem.islandsGridData.placedIslands));
        string keyObjects = JsonUtility.ToJson(new ListContainer<Vector2>(placementSystem.placementData.placedObjects.Keys.ToList()));
        string valueObjects = JsonUtility.ToJson(new ListContainer<PlacementData>(placementSystem.placementData.placedObjects.Values.ToList()));

        PlayerPrefs.SetString("Islands", jsonIsland);
        PlayerPrefs.SetString("PlacedIslands", jsonPlacedIslands);
        PlayerPrefs.SetString("KeyObjects", keyObjects);
        PlayerPrefs.SetString("ValueObjects", valueObjects);
        //Debug.Log(valueObjects);
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

            //Dictionary<Vector2, PlacementData> placedObjects = new Dictionary<Vector2, PlacementData>();

            //for (int i  = 0; i < keyObjects.Length; i++)
            //{
            //    placedObjects.Add(keyObjectsContainer.list[i], valueObjectsContainer.list[i]);
            //}

            //placementSystem.placementData.placedObjects = placedObjects;

            var list = listContainer.list;

            //islandsPlacementSystem.islandsGridData.placedIslands = listPlacedIslands.list;

            foreach (var island in list)
            {
                var loadIsland = Instantiate(islandPrefab, islandlist.transform);
                loadIsland.transform.position = island.islandPos;

                //foreach (var placedObject in island.placementData)
                //{
                //    Instantiate(dataBase.objectsData[0].Prefab, loadIsland.transform);
                //    loadIsland.GetComponent<IslandBuilding>().placedObjects.Add(placedObject);
                //}
            }
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

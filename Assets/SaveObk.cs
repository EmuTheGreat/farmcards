using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Unity.VisualScripting;
using UnityEngine;

public class SaveObk : MonoBehaviour
{
    public PlacementSystem placementSystem;
    public GameObject islandlist;

    public void Save()
    {
        List<IslandBuilding> islandBuildings = new List<IslandBuilding>();
        for (var i = 0; i < islandlist.transform.childCount; i++)
        {
            var temp = islandlist.transform.GetChild(i).GetComponent<IslandBuilding>();
           islandBuildings.Add(temp);
        }
        SerializeAndSaveToJson(islandBuildings, "save.data");
    }

    static void SerializeAndSaveToJson<T>(T obj, string filePath)
    {
        string jsonString = JsonSerializer.Serialize(obj);

        // Сохраняем JSON-строку в файл
        File.WriteAllText(filePath, jsonString);
    }

    public void Load()
    {
        string filePath = "save.data";
        DeserializeFromJson(filePath);
    }

    static T DeserializeFromJson<T>(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<T>(jsonString);
    }
}

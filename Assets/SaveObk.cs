using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class SaveObk : MonoBehaviour
{
    public PlacementSystem placementSystem;
    public GameObject islandlist;

    //public void Save()
    //{
    //    List<IslandBuilding> islandBuildings = new List<IslandBuilding>();
    //    for (var i = 0; i < islandlist.transform.childCount; i++)
    //    {
    //        var temp = islandlist.transform.GetChild(i).GetComponent<IslandBuilding>();
    //       islandBuildings.Add(temp);
    //    }
    //    //SerializeAndSaveToJson(islandBuildings, "save.data");
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath
    //      + "/MySaveData.dat");
    //    bf.Serialize(file, islandBuildings);
    //    file.Close();
    //}

    //static void SerializeAndSaveToJson<T>(T obj, string filePath)
    //{
    //    string jsonString = JsonSerializer.Serialize(obj);

    //    Сохраняем JSON-строку в файл
    //    File.WriteAllText(filePath, jsonString);
    //}

    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath
    //+ "/MySaveData.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file =
    //          File.Open(Application.persistentDataPath
    //          + "/MySaveData.dat", FileMode.Open);
    //        List<IslandBuilding> islands = bf.Deserialize(file) as List<IslandBuilding>;
    //        file.Close();
    //    }
    //        //List<IslandBuilding> islands = DeserializeFromJson(filePath) as List<IslandBuilding>;
    //}

    //public List<IslandBuilding> DeserializeFromJson(string filePath)
    //{
    //    string jsonString = File.ReadAllText(filePath);

    //    return JsonSerializer.Deserialize<List<IslandBuilding>>(jsonString);
    //}
}

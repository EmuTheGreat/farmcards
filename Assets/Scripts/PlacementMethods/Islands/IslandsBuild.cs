using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public class IslandBuilding : MonoBehaviour
{
    private Collider2D islandCollider;
    public List<Vector2Int> placedObjects; 

    void Start()
    {
            islandCollider = GetComponent<Collider2D>();
            placedObjects = new();
    }

    public bool IsBuildOnIsland(List<Vector2> positions)
    {
        if (islandCollider)
        {
            return positions.All(x => islandCollider.OverlapPoint(x));
            //return islandCollider.OverlapPoint(positions);
            
        }
        return false;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        bf.Serialize(file, placedObjects);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath
     + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
          var placedObject = (List<Vector2Int>)bf.Deserialize(file);
            file.Close();
            placedObjects = placedObject;
            
        }
    }
}

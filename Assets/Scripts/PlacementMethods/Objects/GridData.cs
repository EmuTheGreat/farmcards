using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public Dictionary<Vector2, PlacementData> placedObjects = new();


    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary alredy contains this cell position {pos}");
            }
            placedObjects[pos] = data;
        }
    }

    public List<Vector2Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector2Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(new Vector2Int(gridPosition.x, gridPosition.y) + new Vector2Int(x, y));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }
   
}

[Serializable]
public class PlacementData
{
    public List<Vector2Int> occupiedPositions;

    public PlacementData()
    {
        occupiedPositions = new List<Vector2Int>();
        ID = -1;
    }

    public PlacementData(List<Vector2Int> occupiedPositions, int iD)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
    }

    public int ID;
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsGridData
{
    public List<Vector2> placedIslands = new();

    public bool CanPlaceIslandAt(Vector2 gridPosition)
    {
        if (placedIslands.Contains(gridPosition))
        {
            return false;
        }
        return true;
    }
}

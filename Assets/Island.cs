using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island 
{
    public List<Vector2Int> placementDatas;
    public Vector2Int islandPos;

    public Island(List<Vector2Int> vector2Ints, Vector2Int vector2Int)
    {
        placementDatas = vector2Ints;
        islandPos = vector2Int;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Island 
{
    public List<Vector2> placementData;
    public Vector3 islandPos;

    public Island()
    {
        placementData = new List<Vector2>();
        islandPos = Vector3.zero;
    }

    public Island(List<Vector2> placementData, Vector3 position)
    {
        this.placementData = placementData;
        islandPos = position;
    }
}

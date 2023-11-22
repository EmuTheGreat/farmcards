using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsColliders : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public bool CheckIslandBuild(List<Vector2> positions)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var e = parent.transform.GetChild(i).GetComponent<IslandBuilding>();
            if (e.IsBuildOnIsland(positions)) return true;
        }
        return false;
    }
}

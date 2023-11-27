using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IslandBuilding : MonoBehaviour
{
    private Collider2D islandCollider;

    void Start()
    {
        islandCollider = GetComponent<Collider2D>();
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
}

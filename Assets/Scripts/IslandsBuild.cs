using UnityEngine;

public class IslandBuilding : MonoBehaviour
{
    private Collider2D islandCollider;

    void Start()
    {
        islandCollider = GetComponent<Collider2D>();
    }

    public bool IsBuildOnIsland(Vector3 position)
    {
        if (islandCollider)
        {
            return islandCollider.OverlapPoint(position);
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObk : MonoBehaviour
{
    public PlacementSystem placementSystem;
    public GameObject islandlist;

    public void Save()
    {
        for(var i = 0; i < islandlist.transform.childCount; i++)
        {
           var temp =   islandlist.transform.GetChild(i).GetComponent<IslandBuilding>();

        }
    }
}

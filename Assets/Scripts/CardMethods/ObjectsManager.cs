using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private PlacementSystem placementSystem;

    public void UpdateDrawObjects()
    {
        var e = GetObjectsAndSort();
        for (int i = 0; i < e.Count; i++)
        {
            e[i].transform.GetChild(0).GetComponent<Renderer>().sortingOrder = i;
        }
    }

    private List<Transform> GetObjectsAndSort()
    {
        var list = new List<Transform>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            list.Add(parent.transform.GetChild(i));
        }
        return list.OrderByDescending(x => x.transform.position.y).ToList();
    }

    public void Harvest()
    {
        foreach(PlacementData placementObject in placementSystem.placementData.placedObjects.Values)
        {
            if (dataBase.objectsData[placementObject.ID].Item != null)
            {
                inventoryManager.AddItem(dataBase.objectsData[placementObject.ID].Item);
            }
        }
    }
}

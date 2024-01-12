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
    private ObjectsDatabaseSO dataBase;
    [SerializeField]
    private PlacementSystem placementSystem;

    public void UpdateDrawObjects()
    {
        var e = GetObjectsAndSort();
        var animals= GetAnimals();
        for (int i = 0; i < e.Count; i++)
        {
            e[i].transform.GetChild(0).GetComponent<Renderer>().sortingOrder = i;
        }
        for (int i = 0;i < animals.Count; i++)
        {
            animals[i].transform.GetChild(0).GetComponent<Renderer>().sortingOrder = e.Count;
        }
    }

    private List<Transform> GetObjectsAndSort()
    {
        var list = new List<Transform>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).GetComponent<Animal>() == null)
            {
                list.Add(parent.transform.GetChild(i));
            }
        }
        return list.OrderByDescending(x => x.transform.position.y).ToList();
    }

    private List<Transform> GetAnimals()
    {
        var list = new List<Transform>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).GetComponent<Animal>() != null)
            {
                list.Add(parent.transform.GetChild(i));
            }
        }
        return list;
    }
}

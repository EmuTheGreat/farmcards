using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

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
}

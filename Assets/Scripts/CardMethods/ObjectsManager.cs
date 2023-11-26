using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;

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
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var id = parent.transform.GetChild(i).GetComponent<ObjectItem>().id;
            var index = dataBase.objectsData.FindIndex(x => x.ID == id);
            if (dataBase.objectsData[index].Item != null)
            {
                inventoryManager.AddItem(dataBase.objectsData[index].Item);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int Id;
}

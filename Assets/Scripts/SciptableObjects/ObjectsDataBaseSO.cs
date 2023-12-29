using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set; }
    
    [field: SerializeField]
    public ObjectType Type { get; private set; }

    [field: SerializeField]
    public int ID { get; private set; }
    
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    public int WaterCost { get; private set; }
    [field: SerializeField]
    public int BuildingsCost { get; private set; }

    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;

    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    [field: SerializeField]
    public Sprite Background {  get; private set; }

    [field: SerializeField]
    public ItemScriptableObject Item { get; private set; }
    
    [field: SerializeField]
    public int Weight { get; private set; }

    [field: SerializeField]
    public string Description { get; private set; }
}

public enum ObjectType
{
    Plant,
    Animal,
    Structure
}
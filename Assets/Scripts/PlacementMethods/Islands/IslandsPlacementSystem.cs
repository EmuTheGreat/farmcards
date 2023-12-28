using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class IslandsPlacementSystem : MonoBehaviour, ISaveState
{
    [SerializeField]
    private Grid islandsGrid;

    [SerializeField]
    private InterfaceManager interfaceManager;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private PlacementSystem placementSystem;

    [SerializeField]
    private GameObject cellIndicator;

    [SerializeField]
    private GameObject islandsList;

    [SerializeField]
    private GameObject posibleIslandsList;

    [SerializeField]
    private GameObject islandPrefab;

    [SerializeField]
    private SaveObk saveObjects;

    public IslandsGridData islandsGridData;

    public bool placementFlag = false;
    public HashSet<Vector2> posibleIslands;
    public int islandCost;

    private Vector3Int GetGridPosition() => islandsGrid.WorldToCell(GetMousePositon());
    private Vector3Int GetGridPosition(Vector2 position) => islandsGrid.WorldToCell(position);
    private Vector3 GetMousePositon() => inputManager.GetSelectedMapPosition();

    private void Start()
    {
        islandsGridData = new();
        islandsGridData.placedIslands.Add(new(0, -0.5f));
        cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        if (PlayerPrefs.HasKey("Islands"))
        {
            saveObjects.Load();
            Load();
        }
        else 
        {
            CreateIsland(new(0, -0.5f));
            islandCost = 10;
        }

    }

    public void StartPlacement()
    {
        UpdateColor();
        placementSystem.StopPlacement();
        placementFlag = !placementFlag;

        if (placementFlag)
        {
            DrawPosibleIslands();
        }
        else
        {
            RemovePosiblePosibleIslands();
        }
    }

    public void StopPlacement()
    {
        placementFlag = false;
        RemovePosiblePosibleIslands();
    }

    public void DebugButton()
    {
        Debug.Log("Есть нажатие");
    }

    private void Update()
    {
        if (placementFlag)
        {
            var pos = CalculateGridPosition(GetMousePositon());
            if (!inputManager.IsPointOverUI() && Input.GetMouseButtonDown(0) && CheckPosibleIsland(pos) && interfaceManager.balance - islandCost >= 0)
            {
                CreateIsland(pos);
                interfaceManager.SetBalance(-islandCost);
                islandCost += (int)(islandCost / 0.15f);
                RemovePosiblePosibleIslands();
                UpdateColor();
                DrawPosibleIslands();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopPlacement();
            }
        }
    }

    private void UpdateColor()
    {
        if (interfaceManager.balance - islandCost < 0)
        {
            cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void RemovePosiblePosibleIslands()
    {
        for (int i = 0; i < posibleIslandsList.transform.childCount; i++)
        {
            Destroy(posibleIslandsList.transform.GetChild(i).gameObject);
        }
    }

    private void DrawPosibleIslands()
    {
        #region old_code
        //var gridPosition = GetGridPosition();
        //Vector3 pos = islandsGrid.CellToWorld(gridPosition);
        //pos.x = (float)Math.Round(pos.x + 2.8799f);
        //pos.y = pos.y + 2.8799f > 0 ? (float)Math.Round(pos.y + 2.8799f) : (float)Math.Ceiling(pos.y + 2.8799f);
        //pos.y = pos.y - 0.5f;
        //cellIndicator.transform.position = pos;


        //if (!inputManager.IsPointOverUI() & Input.GetMouseButtonDown(0))
        //{
        //    Vector3 position = islandsGrid.CellToWorld(gridPosition);
        //    position.x = (float)Math.Round(position.x + 2.8799f);
        //    position.y = position.y + 2.8799f > 0 ? (float)Math.Round(position.y + 2.8799f) : (float)Math.Ceiling(position.y + 2.8799f);
        //    position.y = position.y - 0.5f;

        //    if (islandsGridData.CanPlaceIslandAt(position))
        //    {
        //        GameObject newObject = Instantiate(islandPrefab, islandsList.transform);
        //        newObject.transform.position = position;
        //        islandsGridData.placedIslands.Add(new(position.x, position.y));
        //    }
        //}
        #endregion
        posibleIslands = new HashSet<Vector2>();
        List<Vector2> direction = new List<Vector2>()
        {
            new(6,0),
            new(0,6),
            new(-6,0),
            new(0,-6),
        };

        foreach (Vector2 islandPosition in islandsGridData.placedIslands)
        {
            foreach (var dir in direction)
            {
                var pos = CalculateGridPosition(islandPosition + dir);

                if (!islandsGridData.placedIslands.Contains(pos) && !posibleIslands.Contains(pos))
                {
                    posibleIslands.Add(pos);
                }
            }
        }

        foreach (Vector2 islandPosition in posibleIslands)
        {
            GameObject newObject = Instantiate(cellIndicator, posibleIslandsList.transform);
            newObject.transform.position = islandPosition;
            newObject.transform.GetChild(1).GetComponent<TMP_Text>().text = islandCost.ToString();
        }
    }

    private bool CheckPosibleIsland(Vector2 position) => posibleIslands.Contains(position);

    private Vector2 CalculateGridPosition(Vector2 position)
    {
        Vector2 pos = islandsGrid.CellToWorld(GetGridPosition(position));
        pos.x = (float)Math.Round(pos.x + 2.8799f);
        pos.y = pos.y + 2.8799f > 0 ? (float)Math.Round(pos.y + 2.8799f) : (float)Math.Ceiling(pos.y + 2.8799f);
        pos.y = pos.y - 0.5f;
        return pos;
    }

    private void CreateIsland(Vector2 position)
    {
        GameObject newObject = Instantiate(islandPrefab, islandsList.transform);
        newObject.transform.position = position;
        islandsGridData.placedIslands.Add(position);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("IslandCost", islandCost);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("IslandCost"))
        {
            islandCost = PlayerPrefs.GetInt("IslandCost");
        }
    }
}

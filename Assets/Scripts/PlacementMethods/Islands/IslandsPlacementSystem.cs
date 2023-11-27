using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsPlacementSystem : MonoBehaviour
{
    [SerializeField]
    private Grid islandsGrid;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private PlacementSystem placementSystem;

    [SerializeField]
    private GameObject cellIndicator;

    [SerializeField]
    private GameObject islandsList;

    [SerializeField]
    private GameObject islandPrefab;

    private bool placementFlag = false;

    private Vector3Int GetGridPosition() => islandsGrid.WorldToCell(GetMousePositon());
    private Vector3 GetMousePositon() => inputManager.GetSelectedMapPosition();

    private void Start()
    {
        cellIndicator.SetActive(false);
    }

    public void StartPlacement()
    {
        placementSystem.StopPlacement();
        placementFlag = !placementFlag;
        if (placementFlag)
        {
            cellIndicator.SetActive(placementFlag);
        }
        else
        {
            cellIndicator.SetActive(placementFlag);
        }
    }

    public void StopPlacement()
    {
        if (placementFlag)
        {
            placementFlag = !placementFlag;
            cellIndicator.SetActive(placementFlag);
        }
    }

    private void Update()
    {
        if (placementFlag)
        {
            var gridPosition = GetGridPosition();
            cellIndicator.transform.position = islandsGrid.CellToWorld(gridPosition);

            if (!inputManager.IsPointOverUI() & Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Есть нажатие!");
                GameObject newObject = Instantiate(islandPrefab, islandsList.transform);
                Vector3 position = islandsGrid.CellToWorld(gridPosition);
                position.x = (float)Math.Round(position.x + 2.8799f);
                position.y = position.y + 2.8799f > 0 ? (float)Math.Round(position.y + 2.8799f): (float)Math.Ceiling(position.y + 2.8799f);
                position.y = position.y - 0.5f;
                newObject.transform.position = position;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopPlacement();
            }
        }
    }
}

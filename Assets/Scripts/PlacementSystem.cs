using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualisation;

    [SerializeField]
    private IslandBuilding islandBuilding;

    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private GameObject parentForObjects;

    private GridData placementData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObject = new();

    private GameObject clickedButton;

    private void Start()
    {
        StopPlacement();
        placementData = new();
        previewRenderer = cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void StartPlacement(int ID)
    {
        //—сылка на нажатую кнопку
        clickedButton = EventSystem.current.currentSelectedGameObject;
        
        StopPlacement();
        selectedObjectIndex = dataBase.objectsData.FindIndex(data => data.ID == ID);
        inputManager.currentCost = int.Parse(dataBase.objectsData[selectedObjectIndex].Name);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
        }
        gridVisualisation.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity) 
        {
            return;
        }

        if (islandBuilding.IsBuildOnIsland(mousePosition))
        {
            GameObject newObject = Instantiate(dataBase.objectsData[selectedObjectIndex].Prefab, parentForObjects.transform);
            newObject.transform.position = grid.CellToWorld(gridPosition);
            if(sound != null)
            {
                source.clip = sound;
                source.Play();
            }
            placedGameObject.Add( newObject );
            GridData selectedData = placementData;
            selectedData.AddObjectAt(gridPosition, 
                dataBase.objectsData[selectedObjectIndex].Size,
                dataBase.objectsData[selectedObjectIndex].ID,
                placedGameObject.Count - 1);
            inputManager.OnEsq += () => Destroy(clickedButton);
            inputManager.isPlaced = true;
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = placementData;
        return selectedData.CanPlaceObjectAt(gridPosition, dataBase.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }


    private void Update()
    {
        if(selectedObjectIndex < 0) 
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // —мена цвета индикатора клетки
        if (islandBuilding.IsBuildOnIsland(mousePosition) & CheckPlacementValidity(gridPosition, selectedObjectIndex))
            previewRenderer.material.color = Color.white; 
        else
            previewRenderer.material.color = Color.red; 
        
        
        //mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
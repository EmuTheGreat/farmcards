using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private GameObject cellIndicator;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO dataBase;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualisation;

    [SerializeField]
    private GameObject parentForObjects;

    [SerializeField]
    private IslandsColliders colliders;

    [SerializeField]
    private IslandsPlacementSystem islandsPlacementSystem;

    public GridData placementData = new();

    private Renderer previewRenderer;

    private List<GameObject> placedGameObject = new();

    private GameObject clickedButton;

    public string volumeParameter = "MasterVolume";

    public AudioMixer audioMixer;

    public AudioClip sound;

    public AudioSource audioSource;

    private void Start()
    {
        StopPlacement();
        //placementData = new();
        previewRenderer = cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>();
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "CardVol" ? 0f : -30f);
        audioMixer.SetFloat(volumeParameter, volumeValue);
    }

    public void StartPlacement(int ID)
    {
        islandsPlacementSystem.StopPlacement();
        clickedButton = EventSystem.current.currentSelectedGameObject;

        StopPlacement();

        selectedObjectIndex = dataBase.objectsData.Find(x => x.ID == ID).ID;
        inputManager.currentCost = dataBase.objectsData[selectedObjectIndex].Cost;
        inputManager.currentWater = dataBase.objectsData[selectedObjectIndex].WaterCost;
        inputManager.buildingsCost = dataBase.objectsData[selectedObjectIndex].BuildingsCost;


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
        Vector3Int gridPosition = GetGridPosition();
        if (inputManager.IsPointOverUI() & !CheckPlacementValidity(gridPosition, selectedObjectIndex))
        {
            return;
        }

        IslandBuilding island;
        if (CheckBuild(out island))
        {
            GameObject newObject = Instantiate(dataBase.objectsData[selectedObjectIndex].Prefab, parentForObjects.transform);
            newObject.transform.position = grid.CellToWorld(gridPosition);

            audioSource.PlayOneShot(sound);
            placedGameObject.Add(newObject);
            GridData selectedData = placementData;
            selectedData.AddObjectAt(gridPosition,
                dataBase.objectsData[selectedObjectIndex].Size,
                dataBase.objectsData[selectedObjectIndex].ID);
            inputManager.OnEsq += () => Destroy(clickedButton);
            inputManager.flag = true;
            island.placedObjects.Add((Vector2Int)gridPosition);
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = placementData;
        return selectedData.CanPlaceObjectAt(gridPosition, dataBase.objectsData[selectedObjectIndex].Size);
    }

    public void StopPlacement()
    {
        selectedObjectIndex = -1;

        gridVisualisation.SetActive(false);
        cellIndicator.SetActive(false);

        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        inputManager.currentCost = 0;
        inputManager.currentWater = 0;
        inputManager.buildingsCost = 0;
    }

    public bool CheckBuild(out IslandBuilding island)
    {
        Vector3 mousePosition = GetMousePositon();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        var pos = placementData
            .CalculatePositions(gridPosition, dataBase.objectsData[selectedObjectIndex].Size)
            .Select(x => new Vector2(x.x, x.y))
            .ToList();

        return colliders.CheckIslandBuild(new List<Vector2>(pos) { mousePosition }, out island) & CheckPlacementValidity(gridPosition, selectedObjectIndex);
    }


    private void FixedUpdate()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }

        Vector3Int gridPosition = GetGridPosition();

        IslandBuilding island;
        if (CheckBuild(out island))
        {
            previewRenderer.material.color = Color.white;
        }
        else
        {
            previewRenderer.material.color = Color.red;
        }

        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    private Vector3Int GetGridPosition() => grid.WorldToCell(GetMousePositon());
    private Vector3 GetMousePositon() => inputManager.GetSelectedMapPosition();
}
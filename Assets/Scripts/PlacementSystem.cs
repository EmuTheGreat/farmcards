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

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        //������ �� ������� ������
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        
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
            // �������� ������
        inputManager.OnEsq += () => { Destroy(clickedButton); };
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (islandBuilding.CanBuildHere(mousePosition))
        {
            GameObject newObject = Instantiate(dataBase.objectsData[selectedObjectIndex].Prefab, parentForObjects.transform);
            newObject.transform.position = grid.CellToWorld(gridPosition);
            if(sound != null)
            {
                source.clip = sound;
                source.Play();
            }
        }
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
        if (islandBuilding.CanBuildHere(mousePosition)) { cellIndicator.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white; }
        else { cellIndicator.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red; }
        //mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
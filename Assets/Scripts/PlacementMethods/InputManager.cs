using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private ObjectsManager drawObjects;

    [SerializeField]
    private Camera sceneCamera;

    [SerializeField]
    private LayerMask placementLayermask;

    [SerializeField]
    private IslandBuilding islandCollider;

    [SerializeField]
    private IslandsColliders colliders;

    [SerializeField]
    private InterfaceManager interfaceManager;
    public int currentCost;

    public event Action OnClicked, OnExit, OnEsq;
    public bool isPlaced;
    public bool IsPointOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) & colliders.CheckIslandBuild(new List<Vector2>() { GetSelectedMapPosition() }) & interfaceManager.balance - currentCost >= 0)
        {
            OnClicked?.Invoke();
            drawObjects.UpdateDrawObjects();
            OnEsq?.Invoke();
            OnEsq = null;

            if (isPlaced)
            {
                interfaceManager.balance -= currentCost;
                interfaceManager.water += currentCost;
                isPlaced = false;
                OnExit?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
            OnEsq = null;
        }
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = sceneCamera.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0f;
        return worldPosition;
    }

    public void Save()
    {
        SaveSystem.Save(interfaceManager);
    }

    public void Load()
    {
        InterfaceData data = SaveSystem.Load();
        interfaceManager.balance = data.Balance;
        interfaceManager.day = data.Day;
        interfaceManager.water = data.Water;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private IslandsPlacementSystem islandPlacementSystem;
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private ObjectsManager drawObjects;

    [SerializeField]
    private Camera sceneCamera;

    [SerializeField]
    private LayerMask placementLayermask;

    [SerializeField]
    private IslandsColliders colliders;

    [SerializeField]
    private InterfaceManager interfaceManager;

    public event Action OnClicked, OnExit, OnEsq;
    public bool IsPointOverUI() => EventSystem.current.IsPointerOverGameObject();

    public int currentCost;
    public int currentWater;
    public int waterSum;

    public bool flag;

    private void Update()
    {
        if (!islandPlacementSystem.placementFlag && Input.GetMouseButtonDown(0) && !IsPointOverUI() && interfaceManager.balance - currentCost >= 0)
        {
            OnClicked?.Invoke();
            drawObjects.UpdateDrawObjects();
            OnEsq?.Invoke();
            OnEsq = null;

            //Доп. проверка на утсановку объекте.
            if (flag)
            {
                //Счётчик воды, который прибавляет или отнимает воду каждый день.
                waterSum += currentWater;
                interfaceManager.SetWaterSum(waterSum);
                interfaceManager.SetBalance(-currentCost);
                flag = false;
            }

            OnExit?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlace();
        }
    }

    public void CancelPlace()
    {
        OnExit?.Invoke();
        OnEsq = null;
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = sceneCamera.ScreenToWorldPoint(mousePos);
        worldPosition.z = -1f;
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
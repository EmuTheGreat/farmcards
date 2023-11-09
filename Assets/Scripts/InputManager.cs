using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    [SerializeField]
    private LayerMask placementLayermask;

    [SerializeField]
    private IslandBuilding islandCollider;

    [SerializeField]
    private InterfaceManager interfaceManager;
    public int currentCost;

    public event Action OnClicked, OnExit, OnEsq;
    public bool IsPointOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) & islandCollider.CanBuildHere(GetSelectedMapPosition()) & interfaceManager.balance - currentCost >= 0)
        {
            interfaceManager.balance -= currentCost;           //Обновление баланса
            OnClicked?.Invoke();
            OnExit?.Invoke();
            OnEsq?.Invoke();                                   //Отмена удаления выбранной карты
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
}
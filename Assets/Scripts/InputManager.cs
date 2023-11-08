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
    private Collider2D islandCollider;

    public event Action OnClicked, OnExit;
    public bool IsPointOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
            OnExit?.Invoke();
            OnExit = null;
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //    OnExit?.Invoke();
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = sceneCamera.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
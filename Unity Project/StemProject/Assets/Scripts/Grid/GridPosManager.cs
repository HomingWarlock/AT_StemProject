using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridPosManager : MonoBehaviour
{
    public static GridPosManager Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask grid_layer;
    private Vector3 last_pos;

    public event Action OnClicked, OnExit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetGridPosition()
    {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mouse_pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, grid_layer))
        {
            last_pos = hit.point;
        }
        return last_pos;
    }
}
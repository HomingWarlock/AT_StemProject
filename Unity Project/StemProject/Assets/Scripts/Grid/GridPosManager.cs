using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosManager : MonoBehaviour
{
    public static GridPosManager Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask grid_layer;
    private Vector3 last_pos;

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
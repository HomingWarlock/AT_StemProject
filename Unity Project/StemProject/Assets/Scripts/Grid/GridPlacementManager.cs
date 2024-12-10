using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject mouse_point_object;
    [SerializeField] private GameObject cell_border_marker;
    [SerializeField] private Grid grid;

    private void Update()
    {
        Vector3 mouse_pos = GridPosManager.Instance.GetGridPosition();
        Vector3Int grid_pos = grid.WorldToCell(mouse_pos);
        mouse_point_object.transform.position = mouse_pos;
        cell_border_marker.transform.position = grid.CellToWorld(grid_pos);
    }
}
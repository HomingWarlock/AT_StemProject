using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject mouse_point_object;
    [SerializeField] private GameObject cell_border_marker;
    [SerializeField] private Grid grid;

    [SerializeField] private BuildingPiecesSO build_pieces_SO;
    private int current_object_index;

    [SerializeField] private GameObject grid_visual;

    private bool build_input_delay;

    private void Start()
    {
        StopPlacement();
        build_input_delay = false;
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        current_object_index = build_pieces_SO.build_data.FindIndex(data => data.ID == ID);
        if (current_object_index < 0)
        {
            Debug.LogError($"No ID Found {ID}");
            return;
        }
        grid_visual.SetActive(true);
        cell_border_marker.SetActive(true);
        GridPosManager.Instance.OnClicked += PlaceBuild;
        GridPosManager.Instance.OnExit += StopPlacement;
    }

    public void PlaceBuild()
    {
        if (GridPosManager.Instance.IsPointerOverUI())
        {
            return;
        }
        else if (!build_input_delay)
        {
            build_input_delay = true;
            StartCoroutine(BuildInputDelay());
            Vector3 mouse_pos = GridPosManager.Instance.GetGridPosition();
            Vector3Int grid_pos = grid.WorldToCell(mouse_pos);
            GameObject newObject = Instantiate(build_pieces_SO.build_data[current_object_index].Prefab);
            newObject.transform.position = grid.CellToWorld(grid_pos);
        }
    }

    public void StopPlacement()
    {
        current_object_index = -1;
        grid_visual.SetActive(false);
        cell_border_marker.SetActive(false);
        GridPosManager.Instance.OnClicked -= PlaceBuild;
        GridPosManager.Instance.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (current_object_index < 0)
            return;
        Vector3 mouse_pos = GridPosManager.Instance.GetGridPosition();
        Vector3Int grid_pos = grid.WorldToCell(mouse_pos);
        mouse_point_object.transform.position = mouse_pos;
        cell_border_marker.transform.position = grid.CellToWorld(grid_pos);
    }

    public IEnumerator BuildInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        build_input_delay = false;
    }
}
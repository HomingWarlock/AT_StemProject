using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementManager : MonoBehaviour
{
    public static GridPlacementManager Instance;

    [SerializeField] private GameObject build_piece_holder;

    [SerializeField] private GameObject mouse_point_object;
    [SerializeField] private GameObject cell_border_marker;
    [SerializeField] private Grid grid;

    private MeshRenderer cell_border_marker_rend;
    [SerializeField] private List<Material> cell_border_mats;

    [SerializeField] private BuildingPiecesSO build_pieces_SO;
    private int current_object_index;

    [SerializeField] private GameObject grid_visual;

    private bool build_input_delay;

    private bool is_player_placed;
    public GameObject current_player_object;
    [SerializeField] private GameObject player_delete_menu;

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

        is_player_placed = false;
        player_delete_menu.SetActive(false);
    }

    private void Start()
    {
        StopPlacement();
        build_input_delay = false;
        cell_border_marker_rend = cell_border_marker.transform.Find("CellBorderMarker").GetComponent<MeshRenderer>();
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

    public void DisplayGrid()
    {
        grid_visual.SetActive(true);
        cell_border_marker.SetActive(true);
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        DisplayGrid();
        current_object_index = build_pieces_SO.build_data.FindIndex(data => data.ID == ID);
        if (current_object_index < 0)
        {
            Debug.LogError($"No ID Found {ID}");
            return;
        }
        cell_border_marker_rend.material = cell_border_mats[current_object_index];
        GridPosManager.Instance.OnClicked += PrepareBuild;
        GridPosManager.Instance.OnExit += StopPlacement;
    }

    public void PrepareBuild()
    {
        if (GridPosManager.Instance.IsPointerOverUI())
        {
            return;
        }
        else if (!build_input_delay)
        {
            if (current_object_index == 5 || current_object_index == 6)
            {
                if (!is_player_placed)
                {
                    PlaceBuild();
                }
                else if (is_player_placed)
                {
                    player_delete_menu.SetActive(true);
                }
            }
            else
            {
                PlaceBuild();
            }
        }
    }

    public void PlaceBuild()
    {
        build_input_delay = true;
        StartCoroutine(BuildInputDelay());
        Vector3 mouse_pos = GridPosManager.Instance.GetGridPosition();
        Vector3Int grid_pos = grid.WorldToCell(mouse_pos);
        GameObject newObject = Instantiate(build_pieces_SO.build_data[current_object_index].Prefab);
        newObject.transform.name = build_pieces_SO.build_data[current_object_index].Name;
        newObject.transform.position = grid.CellToWorld(grid_pos);
        newObject.transform.parent = build_piece_holder.transform;

        if (current_object_index == 5 || current_object_index == 6)
        {
            if (!is_player_placed)
            {
                is_player_placed = true;
                current_player_object = newObject;
                PlayManager.Instance.player_cam_point = current_player_object.transform.Find("PieceObject/Cam_Point").gameObject;
            }
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

    public void DeletePlayer()
    {
        player_delete_menu.SetActive(false);
        Destroy(current_player_object);
        is_player_placed = false;
    }

    public IEnumerator BuildInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        build_input_delay = false;
    }
}
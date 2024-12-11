using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu_toggle_button;

    private bool is_hidden;
    [SerializeField] private GameObject build_menu;

    private void Awake()
    {
        menu_toggle_button.SetActive(true);

        is_hidden = true;
        build_menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (!is_hidden)
        {
            is_hidden = true;
            build_menu.SetActive(false);
            GridPlacementManager.Instance.StopPlacement();
        }
        else if (is_hidden) 
        {
            is_hidden = false;
            build_menu.SetActive(true);
            GridPlacementManager.Instance.DisplayGrid();
        }
    }
}

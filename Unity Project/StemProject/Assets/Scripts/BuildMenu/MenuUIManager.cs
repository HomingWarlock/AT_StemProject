using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu_toggle_button;

    private bool is_hidden;
    [SerializeField] private GameObject build_menu;

    [SerializeField] private GameObject play_button;

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

    public void HideMenusBeforeTest()
    {
        PlayManager.Instance.play_button.SetActive(false);
        menu_toggle_button.SetActive(false);
        play_button.SetActive(false);
        is_hidden = true;
        build_menu.SetActive(false);
        GridPlacementManager.Instance.StopPlacement();
        PlayManager.Instance.PlayTestSetup();
    }
}

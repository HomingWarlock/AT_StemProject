using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;

    public GameObject menu_toggle_button;

    private bool is_hidden;
    [SerializeField] private GameObject build_menu;

    [SerializeField] private GameObject play_button;
    [SerializeField] private GameObject no_player_found_menu;

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

        menu_toggle_button.SetActive(true);

        is_hidden = true;
        build_menu.SetActive(false);
        play_button.SetActive(true);
        no_player_found_menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (!is_hidden)
        {
            is_hidden = true;
            PlayManager.Instance.gamemode = "Idle";
            build_menu.SetActive(false);
            GridPlacementManager.Instance.StopPlacement();
        }
        else if (is_hidden) 
        {
            is_hidden = false;
            PlayManager.Instance.gamemode = "Building";
            build_menu.SetActive(true);
            GridPlacementManager.Instance.DisplayGrid();
        }
    }

    public void HideMenusBeforeTest()
    {
        if (!is_hidden)
        {
            ToggleMenu();
        }

        PlayManager.Instance.PlayTestSetup();
    }

    public void OpenNoPlayerDetectedMenu()
    {
        no_player_found_menu.SetActive(true);
    }
}

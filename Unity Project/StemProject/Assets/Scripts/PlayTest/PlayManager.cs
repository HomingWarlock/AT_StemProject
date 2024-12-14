using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public string gamemode;

    [SerializeField] private GameObject grid_holder;
    public GameObject play_button;

    [SerializeField] private GameObject build_cam;
    public GameObject play_cam;

    public GameObject player_cam_point;

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

        gamemode = "Idle";
    }

    public void PlayTestSetup()
    {
        if (GridPlacementManager.Instance.current_player_object != null)
        {
            MenuUIManager.Instance.menu_toggle_button.SetActive(false);
            grid_holder.SetActive(false);
            play_button.SetActive(false);

            play_cam.SetActive(true);
            build_cam.SetActive(false);

            CamFollow.Instance.SetNewPlayerFocus(player_cam_point);

            gamemode = "Play";
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (GridPlacementManager.Instance.current_player_object == null)
        {
            MenuUIManager.Instance.OpenNoPlayerDetectedMenu();
        }
    }

    public void EndPlayTest()
    {
        MenuUIManager.Instance.menu_toggle_button.SetActive(true);
        grid_holder.SetActive(true);
        play_button.SetActive(true);

        play_cam.SetActive(false);
        build_cam.SetActive(true);

        gamemode = "Building";
        Cursor.lockState = CursorLockMode.None;
    }
}

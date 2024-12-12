using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public string gamemode;

    public GameObject play_button;
    [SerializeField] private GameObject grid_holder;

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

        gamemode = "Building";
    }

    public void PlayTestSetup()
    {
        play_button.SetActive(false);
        grid_holder.SetActive(false);

        play_cam.SetActive(true);
        build_cam.SetActive(false);

        CamFollow.Instance.SetNewPlayerFocus(player_cam_point);

        gamemode = "Play";
        Cursor.lockState = CursorLockMode.Locked;
    }
}

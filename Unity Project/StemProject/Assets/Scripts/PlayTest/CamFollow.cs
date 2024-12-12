using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public static CamFollow Instance;

    private float pos_lerp;
    private float rot_lerp;

    private GameObject cam_point;

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

        pos_lerp = 1;
        rot_lerp = 1;
    }

    private void Update()
    {
        if (PlayManager.Instance.gamemode == "Play")
        {
            transform.position = Vector3.Lerp(transform.position, cam_point.transform.position, pos_lerp);
            transform.rotation = Quaternion.Lerp(transform.rotation, cam_point.transform.rotation, rot_lerp);
        }
    }

    public void SetNewPlayerFocus(GameObject campoint)
    {
        cam_point = campoint;
    }
}

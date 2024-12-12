using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMove player_script;

    void Start()
    {
        player_script = transform.GetComponentInParent<PlayerMove>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ground")
        {
            player_script.grounded = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground")
        {
            player_script.grounded = false;
        }
    }
}

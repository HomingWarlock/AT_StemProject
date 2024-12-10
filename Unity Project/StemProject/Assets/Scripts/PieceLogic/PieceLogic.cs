using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLogic : MonoBehaviour
{
    public GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(parent);
        }
    }
}

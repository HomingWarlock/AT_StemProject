using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider box_col;
    private GameObject player_model;

    private float move_speed;
    private float jump_speed;
    public bool grounded;
    private bool jump_input_delay;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        box_col = transform.Find("PieceObject").GetComponent<BoxCollider>();
        box_col.enabled = false;
        player_model = transform.Find("PieceObject").gameObject;

        move_speed = 5;
        jump_speed = 5;
        grounded = false;
        jump_input_delay = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;

                if (!jump_input_delay)
                {
                    jump_input_delay = true;
                    StartCoroutine(JumpInputDelay());

                    rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rb.useGravity = false;
            box_col.enabled = false;
            PlayManager.Instance.EndPlayTest();
        }

        if (PlayManager.Instance.gamemode == "Play" && !rb.useGravity)
        {
            rb.useGravity = true;
            box_col.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (PlayManager.Instance.gamemode == "Play")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = player_model.transform.forward * move_speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = -player_model.transform.forward * move_speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = player_model.transform.right * move_speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = -player_model.transform.right * move_speed;
            }

            player_model.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 1000 * Time.deltaTime, 0);
        }
    }

    private IEnumerator JumpInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        jump_input_delay = false;
    }
}
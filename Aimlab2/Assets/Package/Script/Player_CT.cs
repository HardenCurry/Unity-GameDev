using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CT : MonoBehaviour
{
    CharacterController Cc;
    public Transform groundCheck;
    public LayerMask groundMask;
    float groundDistance = 0.15f;
    bool isGround;
    float jumpheight = 1f;
    [Header("Gravity")]
    float gravity = -9.81f;
    Vector3 Velo;
    [Header("Movement")]
    float hor;
    float vert;
    public float speed;
    public float check;
    void Start()
    {
        Cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        //vert = Input.GetAxisRaw("Vertical");

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && Velo.y < 0)
        {
            Velo.y = 0;
        }
        Vector3 dir = transform.right * hor + transform.forward * vert + Velo;

        Cc.Move(dir * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGround)
        {
            Velo.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        Velo.y += gravity * Time.deltaTime;

        Cc.Move(Velo * Time.deltaTime);
    }
}

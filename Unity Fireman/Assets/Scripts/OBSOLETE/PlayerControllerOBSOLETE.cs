using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    private CharacterController controller;
    [SerializeField]
    public float jumpForce;

    private Vector3 moveDirection;

    [SerializeField]
    public float gravityScale;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);

        if (controller.isGrounded)
        {
            moveDirection.y = -1;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }


        controller.Move(moveDirection * Time.deltaTime);
    }
}
*/
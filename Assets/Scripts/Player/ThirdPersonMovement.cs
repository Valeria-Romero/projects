using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    //References to Controller and Main Camera
    public CharacterController controller;
    public Transform cam;
    //General speed of the movement
    public float speed = 6f;
    //Gravity
    public float gravity = 9.81f;
    //Initial jump speed
    public float jumpSpeed = 3.5f;
    //Turn speed
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    //This is use as a temp variable, this should not be necesary, but it is needed for the script to work
    private float _directionY;
    //This checks if gravity was already applied
    bool appliedGravityAlready;

    //New InputSystem
    InputController controls;
    private Vector2 axis;
    private bool jump;
    [HideInInspector]public bool shoot;

    private void Awake()
    {
        controls = new InputController();
        controls.Gameplay.Move.performed += ctx => axis = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => axis = Vector2.zero;
        controls.Gameplay.Shoot.performed += ctx => shoot = true;
        controls.Gameplay.Shoot.canceled += ctx => shoot = false;
        controls.Gameplay.Jump.performed += ctx => jump = true;
        controls.Gameplay.Jump.canceled += ctx => jump = false;
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Axis: " + axis);
        Debug.Log("Jump: " + jump);
        Debug.Log("Shoot: " + shoot);
        Vector3 direction = new Vector3(axis.x, 0f, axis.y);









        


        //Gets input values and resets values
        appliedGravityAlready = false;

        //Changes the player's rotation to match the camera
        controller.transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        //Checks if the player is providing an input
        if (direction.magnitude >= 0.1f || controls.Gameplay.Jump.triggered)
        {
            //Changes the player's input to match the camera direction
            //float targetAngle = Mathf.Atan2(0, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float targetAngle2 = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            Vector3 moveDir = new Vector3(0, 0, 0);

            if (direction.magnitude >= 0.1f)
            {
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //Used to make the player rotation smoth, not needed with the current set up
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }

            //Makes the player jump
            if (controller.isGrounded)
            {
                if (controls.Gameplay.Jump.triggered)
                {
                    _directionY = jumpSpeed;
                }
            }

            //Applies gravity
            _directionY -= gravity * Time.deltaTime;

            appliedGravityAlready = true;

            moveDir.y = _directionY;

            //Moves the player according to all previous input
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        //In case the player is not moving, applies gravity
        if (!controller.isGrounded || direction.magnitude < 0.1f)
        {
            if (!appliedGravityAlready)
            {
                Vector3 moveDir = Vector3.zero;

                _directionY -= gravity * Time.deltaTime;

                moveDir.y = _directionY;

                controller.Move(moveDir * speed * Time.deltaTime);
            }
        }
    }
}

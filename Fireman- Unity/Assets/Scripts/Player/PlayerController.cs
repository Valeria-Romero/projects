using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public ShootingWater shootingWater;
    public HealthBar healthBar;

    [Header("Player Stats")]
    public float health = 100f;
    public float maxHealth = 100f;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.5f;

    public float damageCooldown = 0.5f;

    [HideInInspector] public Vector3 moveDir;
    private Vector3 velocity;

    private bool damageRecieved = false;

    [Space]
    [Header("Colisiones")]
    private bool isGrounded;
    public Vector3 groundChecker;
    public float groundCheckerRadius;
    public LayerMask groundMask;
    public HeatGauge heatGauge;
    public float maxHeatGaugeDetection = 10f;
    public LayerMask fireMask;

    //New InputSystem
    InputController controls;
    private Vector2 axis;
    private bool jump;
    private bool shoot;
    //private bool reload;
    private bool reloadTap;

    private void Awake()
    {
        controls = new InputController();
        controls.Gameplay.Move.performed += ctx => axis = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => axis = Vector2.zero;
        controls.Gameplay.Shoot.performed += ctx => shoot = true;
        controls.Gameplay.Shoot.canceled += ctx => shoot = false;
        controls.Gameplay.Jump.performed += ctx => jump = true;
        controls.Gameplay.Jump.canceled += ctx => jump = false;
        //controls.Gameplay.Reload.performed += ctx => reload = true;
        //controls.Gameplay.Reload.canceled += ctx => reload = false;
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

    void Update()
    {
        //Debug.Log("Axis: " + axis);
        //Debug.Log("Jump: " + jump);
        //Debug.Log("Shoot: " + shoot);
        //Debug.Log("Reload: " + reload);

        //Inputs
        reloadTap = controls.Gameplay.Reload.triggered;

        controller.transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f); //Rota al personaje con la camara

        Movement(damageRecieved);

        //Aplica inputs al script de disparar
        shootingWater.MyInput(shoot, reloadTap);

        NearOfFire();
    }

    private void Movement(bool canMove)
    {
        if (!canMove)
        {
            float targetAngle = Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg + cam.eulerAngles.y; //Ajusta los inputs del jugador a la dirección de la camara

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            isGrounded = Physics.CheckSphere(transform.position + groundChecker, groundCheckerRadius, groundMask);



            if (isGrounded)
            {
                velocity.y = -2f;
            }

            if (jump && isGrounded)
            {
                Jump();
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (axis == Vector2.zero)
            {
                moveDir = new Vector3(0, moveDir.y, 0);
            }
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void NearOfFire()
    {
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        

        Collider[] results = Physics.OverlapSphere(currentPosition, maxHeatGaugeDetection, fireMask);

        foreach (var result in results)
        {
            float distance = Vector3.Distance(result.GetComponent<Transform>().position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                float forHeat = (1 * minDistance / 10f);
                heatGauge.currentHeatAmount = forHeat;

            }
        }
    }

    public void TakeDamage(float damage)
    {
        //health -= Mathf.Min(damage, health / 4f );
        health -= damage;
        //damageRecieved = true;
        //controls.Gameplay.Disable();
        //Invoke("ResetDamage", damageCooldown);
        healthBar.UpdateHealthBar();
    }

    /*private void ResetDamage()
    {
        //damageRecieved = false;
        controls.Gameplay.Enable();
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + groundChecker, groundCheckerRadius);
        Gizmos.DrawWireSphere(transform.position, maxHeatGaugeDetection);
    }

}

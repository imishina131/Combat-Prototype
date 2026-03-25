// Combat Prototype
// Isaiah Ragland
// 2026-03-24
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class movement : MonoBehaviour
{

    //Done By Isaiah Ragland


    HealthBarScript healthBarScript;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    public bool inAir = false;

    //new for knockback
    public float knockbackDecay = 5f; // How quickly knockback fades
    private Vector3 knockbackVelocity; // Knockback movement
    //new for knockback


    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Animator animator;

    // camera variables
    public Transform cameraTransform;
    ThirdPersonCamera cam;

    public static movement instance;
    public bool isDodging = false;


    [Header("Input Actions")]
    public InputActionReference moveAction; // expects Vector2
    public InputActionReference jumpAction; // expects Button

    // ADDED BY CAMERON
    // PLAYER ATTACKS HERE
    public Transform attackPoint;
    public float attackRadius = 2f;
    public int damage = 5;
    public LayerMask enemyLayer;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<ThirdPersonCamera>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y <= 1.05f)
        {
            playerVelocity.y = 0.1f;
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Gets camera forward & right
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // Create movement relative to camera
        Vector3 move = camForward * input.y + camRight * input.x;
        move = Vector3.ClampMagnitude(move, 1f);

        // Rotate Player towaard movement direction
        if (move !=  Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        if (move == Vector3.zero)
        {

        }

        // Jump
        if (jumpAction.action.triggered && inAir == false)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            animator.SetBool("Jump", true);
            //invoke to false after 1.26 seconds
            Invoke("Jumpy", 2);
            inAir = true;
            Invoke("backtoground", 1.75f);

        }
        //Jump kick (combo 3)
        if (groundedPlayer != true && Input.GetKeyDown(KeyCode.Z))
        {
            DealDamage();
            Debug.Log("player has jump Kick");
            animator.SetBool("Jump Kick", true);
            //invoke to false after 1.26 seconds
            Invoke("JumpyKick", 1.26f);
        }

        //dodge

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("TDodge");
            playerSpeed = 7.5f;
            Invoke("DodgeTime", 1.5f);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;


        //new for knockback
        // Decay knockback over time
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        //new for knockback

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
    //new for knockback
    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            knockbackVelocity = direction.normalized * force;
        }
    }
    //new for knockback

    public void DodgeTime()
    {
        playerSpeed = 5.0f;
        StartCoroutine(Dodge());
        
    }
    public void Jumpy()
    {
        animator.SetBool("Jump", false);
    }

    public void JumpyKick()
    {
        animator.SetBool("Jump Kick", false);
    }
    public void backtoground()
    {
        inAir = false;
    }

    IEnumerator Dodge()
    {
        isDodging = true;
        yield return new WaitForSeconds(2f);
        isDodging = false;
    }

    //new for knockback
    //testing knockback

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Knockback backwards and slightly upward
            ApplyKnockback(-transform.forward + Vector3.up * 0.5f, 5f);
        }
    }
    //new for knockback

    void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("enemy damaged by player");
            }

            if (cam != null)
            {
                cam.Shake(0.2f, 0.15f);
            }
        }
    }

}

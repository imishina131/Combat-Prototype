using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class movement : MonoBehaviour
{
    HealthBarScript healthBarScript;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Animator animator;

    // camera variables
    public Transform cameraTransform;

    public static movement instance;
    public bool isDodging = false;


    [Header("Input Actions")]
    public InputActionReference moveAction; // expects Vector2
    public InputActionReference jumpAction; // expects Button

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

        /* commented out by cameron
        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
        */

        if (move == Vector3.zero)
        {

        }

        // Jump
        if (jumpAction.action.triggered && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            animator.SetBool("Jump", true);
            //invoke to false after 1.26 seconds
            Invoke("Jumpy", 2);

        }
        //Jump kick (combo 3)
        if (groundedPlayer != true && Input.GetKeyDown(KeyCode.Z))
        {
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

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
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

    IEnumerator Dodge()
    {
        isDodging = true;
        yield return new WaitForSeconds(2f);
        isDodging = false;
    }



   
}

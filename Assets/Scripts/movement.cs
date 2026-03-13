using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    /*
    //combo1
    public float coolDownTime = 1f;
    private float nextFireTime = 0f;
    public static int combo1 = 0;
     float lastClickTime = 0;
     float maxComboDelay = 2;

    //Combo 2 or C2
    public static int combo2 = 0;
     float lastClickTimeC2 = 0;
     float maxComboDelayC2 = 2;
    private float nextFireTime2 = 0f;
    */

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
        /*
        //stops animation for combo 1
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hook punch"))
        {
            animator.SetBool("hook punch", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            animator.SetBool("punch", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("eldow punch"))
        {
            animator.SetBool("eldow punch", false);
            combo1 = 0;
        }
        if (Time.time - lastClickTime > maxComboDelay)
        {
            combo1 = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
        //stops animation for combo 1
        //stops animation for combo 2
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hook punchC2"))
        {
            animator.SetBool("hook punchC2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("HeadButt"))
        {
            animator.SetBool("HeadButt", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("HighKick"))
        {
            animator.SetBool("HighKick", false);
            combo2 = 0;
        }
        if (Time.time - lastClickTimeC2 > maxComboDelayC2)
        {
            combo2 = 0;
        }
        if (Time.time > nextFireTime2)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                OnClick2();
            }
        }
        //stops animation for combo 2
        */
        //
        /*
        if (healthBarScript._currentHP <= 0)
        {
            animator.SetBool("Death" , true);
        }
        */
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y <= 1.05f)
        {
            playerVelocity.y = 0.1f;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        if (move == Vector3.zero)
        {
            //still animations
            //animator.SetBool("Jump", false);
        }

        // Jump
        if (jumpAction.action.triggered && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            animator.SetBool("Jump", true);
            //invoke to false after 1.26 seconds
            
        }
        //Jump kick (combo 3)
        if (groundedPlayer != true && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("player has jump Kick");
            animator.SetBool("Jump Kick", true);
            //invoke to false after 1.26 seconds
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }

    /*
    //for combo1
     void OnClick()
    {
        lastClickTime = Time.time;
        combo1++;
        if (combo1 == 1)
        {
            animator.SetBool("hook punch", true);
            Debug.Log("C1 hook punch");
        }
        combo1 = Mathf.Clamp(combo1, 0, 3);
        if (combo1 >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hook punch"))
        {
            animator.SetBool("hook punch", false);
            animator.SetBool("punch", true);
            Debug.Log("C1 punch");
        }
        if (combo1 >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            animator.SetBool("punch", false);
            animator.SetBool("eldow punch", true);
            Debug.Log("C1 eldow punch");
        }
    }
    //for combo2
     void OnClick2()
    {
        lastClickTimeC2 = Time.time;
        combo2++;
        if (combo2 == 1)
        {
            animator.SetBool("hook punchC2", true);
            Debug.Log("C2 hook punch");
        }
        combo2 = Mathf.Clamp(combo2, 0, 3);
        if (combo2 >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hook punchC2"))
        {
            animator.SetBool("hook punchC2", false);
            animator.SetBool("HeadButt", true);
            Debug.Log("C2 HeadButt");
        }
        if (combo1 >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("HighKick"))
        {
            animator.SetBool("HeadButt", false);
            animator.SetBool("HighKick", true);
            Debug.Log("C2 HighKick");
        }
    }
    */
    
}

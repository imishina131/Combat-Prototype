using UnityEngine;

public class combos : MonoBehaviour
{
    private Animator animator;
    //combo1
    public float coolDownTime = 2f;
    private float nextFireTime = 0f;
    public static int combo1 = 0;
    float lastClickTime = 0;
    float maxComboDelay = 2;

    //Combo 2 or C2
    public static int combo2 = 0;
    float lastClickTimeC2 = 0;
    float maxComboDelayC2 = 2;
    private float nextFireTime2 = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

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
}

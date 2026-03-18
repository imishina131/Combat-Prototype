using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combos : MonoBehaviour
{
    private Animator animator;
    //combo1
    public float coolDownTime = 1.0f;
    private float nextFireTime;
    private int combo1 = 0;

    //Combo 2 or C2
    public float coolDownTime2 = 1.0f;
    private float nextFireTime2;
    private int combo2 = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(combo1);
        //Combo1
        if (Time.time - nextFireTime > coolDownTime)
        {
            combo1 = 0;
            ResetCombo();
        }
        //checks where we are during animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (/*stateInfo.normalizedTime > 0.5f && stateInfo.IsName("hook punch") && */ combo1 == 2)
        {
            animator.SetBool("hook punch", false);
            animator.SetBool("punch", true);
            Debug.Log("punch");
            //animator.SetTrigger("triggerPunch");
            
        }
        if (/*stateInfo.normalizedTime > 0.5f && stateInfo.IsName("punch") &&*/ combo1 == 3)
        {
            animator.SetBool("punch", false);
            animator.SetBool("eldow punch", true);
            Debug.Log("eldow punch");
        }
        if (stateInfo.normalizedTime > 0.5f && stateInfo.IsName("eldow punch"))
        {
            animator.SetBool("eldow punch", false);
            combo1 = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LightCombo();
        }

        //combo 2
        if (Time.time - nextFireTime2 > coolDownTime2)
        {
            combo2 = 0;
            ResetCombo2();
        }
        //checks where we are during animation
        AnimatorStateInfo stateInfo2 = animator.GetCurrentAnimatorStateInfo(0);

        if (combo2 == 2)
        {
            animator.SetBool("hook punchC2", false);
            animator.SetBool("HeadButt", true);
            Debug.Log("HeadButt");
        }
        if (combo2 == 3)
        {
            animator.SetBool("HeadButt", false);
            animator.SetBool("HighKick", true);
            Debug.Log("HighKick");
        }
        if (stateInfo.normalizedTime > 0.5f && stateInfo.IsName("HighKick"))
        {
            animator.SetBool("HighKick", false);
            combo2 = 0;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            HeavyCombo();
        }

    }
    /*
    if (Input.GetKeyDown(KeyCode.??))
        {
          //for combo
        }
    */

//for combo1
void LightCombo()
    {
        nextFireTime = Time.time; // checks attack time
        combo1++;

        combo1 = Mathf.Clamp(combo1, 0, 3); //

        if (combo1 == 1)
        {
            animator.SetBool("hook punch", true);
            Debug.Log("hook punch");
        }
    }

    void ResetCombo()
    {
        animator.SetBool("hook punch", false);
        animator.SetBool("punch", false);
        animator.SetBool("eldow punch", false);
    }






    //for combo2
    void HeavyCombo()
    {
        nextFireTime2 = Time.time; // checks attack time
        combo2++;

        combo2 = Mathf.Clamp(combo2, 0, 3); //

        if (combo2 == 1)
        {
            animator.SetBool("hook punchC2", true);
            Debug.Log("hook punchC2");
        }
    }

    void ResetCombo2()
    {
        animator.SetBool("hook punchC2", false);
        animator.SetBool("HeadButt", false);
        animator.SetBool("HighKick", false);
    }

}

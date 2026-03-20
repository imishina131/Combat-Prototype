using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combos : MonoBehaviour
{
    // ADDED BY CAMERON
    // PLAYER ATTACKS HERE
    public Transform attackPoint;
    public float attackRadius = 2f;
    public int damage = 5;
    public LayerMask enemyLayer;

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
        //Combo1
        if (Time.time - nextFireTime > coolDownTime)
        {
            combo1 = 0;
            ResetCombo();
        }
        //checks where we are during animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //combo 1 attack 2
        if (combo1 == 2)
        {
            animator.SetBool("hook punch", false);
            animator.SetBool("punch", true);
            Debug.Log("punch");
            
            DealDamage();
        }
        //combo 1 attack 3
        if (combo1 == 3)
        {
            animator.SetBool("punch", false);
            animator.SetBool("eldow punch", true);
            Debug.Log("eldow punch");

            DealDamage();
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

        //combo 2 attack 2

        if (combo2 == 2)
        {
            animator.SetBool("hook punchC2", false);
            animator.SetBool("HeadButt", true);
            Debug.Log("HeadButt");

            DealDamage();
        }

        ////combo 2 attack 3
        if (combo2 == 3)
        {
            animator.SetBool("HeadButt", false);
            animator.SetBool("HighKick", true);
            Debug.Log("HighKick");

            DealDamage();
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

//for combo1
void LightCombo()
    {
        nextFireTime = Time.time; // checks attack time
        combo1++;

        combo1 = Mathf.Clamp(combo1, 0, 3);

        //for combo1 attack 1
        if (combo1 == 1)
        {
            animator.SetBool("hook punch", true);
            Debug.Log("hook punch");

            DealDamage();
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

        //for combo2 attack 1
        if (combo2 == 1)
        {
            animator.SetBool("hook punchC2", true);
            Debug.Log("hook punchC2");

            DealDamage();
        }
    }

    void ResetCombo2()
    {
        animator.SetBool("hook punchC2", false);
        animator.SetBool("HeadButt", false);
        animator.SetBool("HighKick", false);
    }

    // ADDED BY CAMERON
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

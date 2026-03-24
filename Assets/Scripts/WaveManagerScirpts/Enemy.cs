using UnityEngine;
using System;
using UnityEngine.AI;

// this represents an enemy in the world
public class Enemy : MonoBehaviour
{
    // events to help organize 
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    public int maxHealth = 10;
    public int currentHealth;

    public GameObject healthPickup;

    private Animator animator;
    NavMeshAgent agent;
    public bool isDead = false;
    float knockbackStrength = 20;
    private GameObject player;

    Rigidbody rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (agent != null)
        {
            agent.enabled = false;
        }

        if (rb != null)
        {
            //rb.isKinematic = false;
            //rb.useGravity = true;
            Vector3 knockbackDirection = (gameObject.transform.position - player.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse); 
            Invoke("ResetEnemyState", 1.5f); 
        }

        if (isDead) return;

        currentHealth -= damage;

        // if any other scipt is listening to this event, invoke this method
        OnEnemyHit?.Invoke(this);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    void ResetEnemyState()
    {
        if (isDead) return;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; 
            //rb.isKinematic = true;
            //rb.useGravity = false;
        }

        if (agent != null)
        {
            agent.enabled = true;
            agent.Warp(transform.position);
        }
    }

    public void Kill()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("enemyDeath");
        }
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // 5 percent chance to drop health on death
        if (UnityEngine.Random.value < 0.5f)
        {
            if (healthPickup != null)
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
            }
        }

        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject, 2f);
    }
}

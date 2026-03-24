using UnityEngine;
using System;
using UnityEngine.AI;

// this represents an enemy in the world
public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    public int maxHealth = 10;
    public int currentHealth;

    public GameObject healthPickup;

    private Animator animator;
    NavMeshAgent agent;
    public bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        OnEnemyHit?.Invoke(this);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        isDead = true;

        animator.SetTrigger("enemyDeath");
        agent.ResetPath();

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

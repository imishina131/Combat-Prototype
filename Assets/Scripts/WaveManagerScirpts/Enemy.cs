using UnityEngine;
using System;

// this represents an enemy in the world
public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    public int maxHealth = 10;
    public int currentHealth;

    public GameObject healthPickup;

    private Animator animator;
    public bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

        // 5 percent chance to drop health on death
        if (UnityEngine.Random.value < 0.5f)
        {
            if (healthPickup != null)
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
            }
        }

        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject, 5f);
    }
}

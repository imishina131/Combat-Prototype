using UnityEngine;
using System;

// this represents an enemy in the world
public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;

    public int maxHealth = 10;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}

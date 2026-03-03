using UnityEngine;
using System;

// this represents an enemy in the world
public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;

    public void Kill()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}

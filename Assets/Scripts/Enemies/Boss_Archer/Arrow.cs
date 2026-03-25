// Combat Prototype
// Cameron Lee Czysz-Mille
// 2026-03-24
using UnityEngine;

// Created by Cameron
public class Arrow : MonoBehaviour
{
    public int damage = 2;

    private void OnCollisionEnter(Collision collision)// destroys when collides and damages the player when hit
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthBarScript playerHealth = collision.gameObject.GetComponent<HealthBarScript>();

            playerHealth.TakeDamage(damage, gameObject);
        }

        Destroy(gameObject);
    }
}

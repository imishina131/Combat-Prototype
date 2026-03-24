using UnityEngine;

// Created by Cameron
public class Arrow : MonoBehaviour
{
    public int damage = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthBarScript playerHealth = collision.gameObject.GetComponent<HealthBarScript>();

            playerHealth.TakeDamage(damage, gameObject);
        }

        Destroy(gameObject);
    }
}

// Combat Prototype
// Irina Mishina
// 2026-03-24
using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    Transform playerLocation;
    Transform initialPos;
    Vector3 direction;

    float speed = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerLocation = player.transform;
        initialPos = gameObject.transform;

        direction = (playerLocation.position - initialPos.position).normalized;

        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()//moves the projectile continuously
    {
        transform.position += new Vector3(direction.x, direction.y + 0.3f, direction.z) * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)//destroy if the projectile hits the ground or the player
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()// destroys prefab after 4 seconds
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}

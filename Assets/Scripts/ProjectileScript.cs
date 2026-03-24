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
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y + 0.3f, direction.z) * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}

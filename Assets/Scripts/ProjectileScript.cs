using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit With Arrow");
        }

        Destroy(gameObject);
    }
}

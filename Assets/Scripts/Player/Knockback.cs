using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    //Done By Isaiah Ragland
    //works for enemy
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float strength = 17, delay = 0.2f;
    public UnityEvent OnBegin, OnDone;

    public void PlayFeedBack(GameObject sender)
    {
        StopAllCoroutines();
        OnDone?.Invoke();
        Vector3 direction = (transform.position -sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}

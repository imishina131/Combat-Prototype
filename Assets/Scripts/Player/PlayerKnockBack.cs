using System.Collections;
using UnityEngine;

public class PlayerKnockBack : MonoBehaviour
{
    //Done By Isaiah Ragland

    public float KnockBackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    //not riggid body on player so..
    private Rigidbody rb;
    private CharacterController controller;

    public bool isKnockBack {  get; private set; }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator KnockBackAction(Vector3 hitDirection, Vector3 constForceDirection, float InputDirection)
    {
        isKnockBack = true;

        Vector3 _hitForce;
        Vector3 _constForce;
        Vector3 _knockbackForce;
        Vector3 _combindForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constForce = constForce * constForceDirection;

        float _elapsedTime = 0f;
        while(_elapsedTime < KnockBackTime)
        {
            //timer
            _elapsedTime += Time.deltaTime;
            //combing hitfoece and consrforce
            _knockbackForce = _hitForce + _constForce;

            //combind knockback with input
            if (InputDirection != 0)
            {
                _combindForce = _knockbackForce + new Vector3(InputDirection, 0f);

            }
            else
            {
                _combindForce = _knockbackForce;
            }
            //applying the knockback
            //controller.velocity = _combindForce;
            rb.linearVelocity = _combindForce;

            //
            yield return new WaitForFixedUpdate();
        }

        isKnockBack = false;
    }
}

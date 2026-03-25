// Combat Prototype
// Cameron Lee Czysz-Mille & Isaiah Ragland
// 2026-03-24
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    private bool cursorLocked = true;

    public float mouseSensitivity = 200f;
    public float distance = 6f;
    public float height = 2f;

    public float minY = -30f;
    public float maxY = 60f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    // camera shake
    float shakeDuration = 0f;
    float shakeMagnitude = 0.2f;
    float shakeTimer = 0f;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        // press esc to toggle cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // rotate camera around player
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        Vector3 offset = new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + rotation * offset;

        // if shake timer is set, shakes camera randomly
        if (shakeTimer > 0f)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.position = desiredPosition + randomOffset;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = desiredPosition;
        }
            transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTimer = duration;
    }
}

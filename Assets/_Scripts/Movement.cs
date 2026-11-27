using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    GameObject camera;
    Rigidbody rb;

    [Header("Input System")]
    Vector2 moveInput;
    Vector2 lookInput;

    [Header("Movement Values")]
    public float speed;

    [Header("Camera Values")]
    float xRotation;
    public float sensitivity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        //movement
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.linearVelocity = new Vector3(moveDir.x, 0, moveDir.z) * speed;
    }

    private void Update()
    {
        //Looking
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void GetMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}

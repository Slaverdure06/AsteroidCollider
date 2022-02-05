using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    private PlayerInput playerInput;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
    }

    
    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVelocity);
        transform.position += (Time.deltaTime * movementDirection);
    }

    private void ProcessInput() 
    {
        if (playerInput.actions["Move"].IsPressed())
        {
            Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
            movementDirection = new Vector3(input.x,input.y);
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen() 
    {
        Vector3 newPos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.x >1) { newPos.x = -newPos.x +0.1f; }
        else if (viewportPos.x < 0) { newPos.x = -newPos.x - 0.1f; }
        if (viewportPos.y > 1) { newPos.y = -newPos.y + 0.1f; }
        else if (viewportPos.y < 0) { newPos.y = -newPos.y - 0.1f; }

        transform.position = newPos;
    }

    private void RotateToFaceVelocity() 
    {
        if (rb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,rotationSpeed * Time.deltaTime);
    }
}

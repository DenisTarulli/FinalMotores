using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpHeight = 7f;

    public bool isRunning = false;
    public bool isInAir = false;

    private float gravity = -15f;
    private Vector3 velocity;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.right * xInput + transform.forward * zInput;

        characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        if (characterController.isGrounded)
            isInAir = false;
        else 
            isInAir = true;

        if (moveDir != Vector3.zero)
            isRunning = true;
        else
            isRunning = false;
    }
}

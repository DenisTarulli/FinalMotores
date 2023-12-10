using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float attackDelay = 0.43f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask attackLayer;

    private bool isAttacking = false;
    private bool readyToAttack = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpHeight = 7f;

    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isInAir = false;

    [SerializeField] private Camera cam;

    private float gravity = -15f;
    private Vector3 velocity;
    private CharacterController characterController;
    private Animator animator;
    private string currentAnimationState;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0))
            Attack();

        SetAnimations();
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

        if (moveDir != Vector3.zero)
            isRunning = true;
        else
            isRunning = false;
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;

        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.1f);
    }

    private void SetAnimations()
    {
        if (!isAttacking)
        {
            if (!characterController.isGrounded)
                ChangeAnimationState("Jump");
            else if (!isRunning)
                ChangeAnimationState("Idle");
            else
                ChangeAnimationState("Run");
        }
    }

    private void Attack()
    {
        if (isAttacking || !readyToAttack) return;
        
        isAttacking = true;
        readyToAttack = false;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        ChangeAnimationState("Attack");
    }

    private void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            Debug.Log(hit.collider.name);
        }
    }


    private void ResetAttack()
    {
        isAttacking = false;
        readyToAttack = true;
    }
}

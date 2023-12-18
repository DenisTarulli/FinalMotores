using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float attackDelay = 0.43f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float maxHealth = 100f;
    
    [HideInInspector] private float currentHealth = 0;
    [HideInInspector] private bool isAttacking = false;
    [HideInInspector] private bool readyToAttack = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpHeight = 7f;    

    [Header("References")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerHealthBar healthBar;
    

    [HideInInspector] private CharacterController characterController;
    [HideInInspector] private Animator animator;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isInAir = false;
    [HideInInspector] private float gravity = -15f;
    [HideInInspector] private string currentAnimationState;
    [HideInInspector] private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
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

        Invoke(nameof(ResetAttack), attackCooldown);
        Invoke(nameof(AttackRaycast), attackDelay);

        ChangeAnimationState("Attack");
    }

    private void AttackRaycast()
    {
        FindObjectOfType<AudioManager>().Play("AxeSwing");

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            hit.collider.TryGetComponent(out Enemy enemy);

            if (enemy != null)
            {
                enemy.TakeDamage();
                GameObject hitFx = Instantiate(hitEffect, hit.point, Quaternion.identity);
                Destroy(hitFx, 1f);
            }
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        readyToAttack = true;
    }

    public void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().Play("Hurt");
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}

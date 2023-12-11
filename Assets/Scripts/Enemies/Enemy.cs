using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float damage = 18f;
    [SerializeField] private float attackDelay = 0.1f;

    private GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    private PlayerActions playerActions;

    private bool isAttacking = false;
    private float distance;

    private void Start()
    {        
        playerActions = FindObjectOfType<PlayerActions>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (!isAttacking)
            agent.SetDestination(player.transform.position);

        if (distance <= agent.stoppingDistance && !isAttacking)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        agent.isStopped = true;
        isAttacking = true;

        anim.SetLayerWeight(1, 1);
        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(0.3f);

        if (distance <= attackRange)
            playerActions.TakeDamage(damage);

        yield return new WaitForSeconds(0.45f);

        agent.isStopped = false;
        isAttacking = false;

        anim.SetLayerWeight(1, 0);
        anim.SetBool("Attack", false);

        yield return new WaitForSeconds(attackDelay);
    }
}

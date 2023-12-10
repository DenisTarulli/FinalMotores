using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    private void Start()
    {        
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();        
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}

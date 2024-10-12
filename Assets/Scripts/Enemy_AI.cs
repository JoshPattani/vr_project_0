using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy_AI : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Attacking }
    
    [Header("Enemy Settings")] 
    public float chaseDistance = 10f; // Distance at which the enemy will start chasing the player 

    public float attackDistance = 2f; // Distance at which the enemy will attack the player 

    public float patrolSpeed = 2f;  // Speed during patrol 

    public float chaseSpeed = 5f; // Speed when chasing the player

    // Patrol Points
    public Transform[] patrolPoints;

    private int currentPatrolIndex; // Current patrol point index 

    [Header("Combat Settings")]
    public float attackDamage = 10f; // Damage dealt to the player on attack
    public float attackCooldown = 2f; // Cooldown time
    private float nextAttackTime = 5f; // Time until the next attack
         
    [Header("References")] 
    public GameObject player;  // Reference to the player 
    private NavMeshAgent navAgent; // Reference to the NavMeshAgent component 
    private EnemyState currentState; // Current state of the enemy 
     
    // private float move_speed = 10f;
    // private GameObject playerTarget;

    void Start()
    {
        // Get the NavMeshAgent component
        navAgent = GetComponent<NavMeshAgent>();

        // Set the NavMeshAgent to use the NavMesh
        navAgent.updatePosition = true;
       
        // Set the initial state to patrolling
        currentState = EnemyState.Patrolling;

        // Set the initial patrol point index
        currentPatrolIndex = 0;

        // Set the initial speed to patrol speed
        navAgent.speed = patrolSpeed;
    } 

    void Update()
    {
        // Check the current state of the enemy 
        switch (currentState)
        {
            case EnemyState.Patrolling:
                // Patrol between points
                Patrol();
                break;
            case EnemyState.Chasing:
                // Chase the player
                Chase();
                break;
            case EnemyState.Attacking:
                // Attack the player
                Attack();
                break;
        }
    }

    private void Patrol() {
        if (patrolPoints.Length == 0) return;

        // Ensure the agent is not stopped
        if (navAgent.isStopped)
            navAgent.isStopped = false;

        // Set patrol speed if not already set
        if (navAgent.speed != patrolSpeed)
            navAgent.speed = patrolSpeed;

        // Check if the agent has a path or is currently moving towards a destination
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // Set the destination to the current patrol point
            navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
            Debug.Log("Patrolling to point " + currentPatrolIndex);

            // Move to the next patrol point for the next cycle
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        } else if (!navAgent.hasPath)
        {
            // If agent has no path, set the destination
            navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
            Debug.Log("Setting initial patrol destination to point " + currentPatrolIndex);
        }

        // Check for player
        CheckForPlayer();
    }

    private void Chase() {
        // Resume movement
        if (navAgent.isStopped) navAgent.isStopped = false;
        // Set chase speed
        navAgent.speed = chaseSpeed;
        // Set the destination to the player's position
        navAgent.SetDestination(player.transform.position);

        // Check for player
        CheckForPlayer();
    }

    private void Attack()
    {
        // Stop the enemy from moving
        navAgent.isStopped = true;

        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Assuming the player has a PlayerHealth script
                if (playerHealth != null)
                {
                    // Deal damage to the player
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("Attacking player! Dealt " + attackDamage + " damage.");

                    // Set the cooldown for the next attack
                    nextAttackTime = Time.time + attackCooldown;
                }
                else
                {
                    Debug.Log("Player does not have a PlayerHealth component.");
                }

                // Optionally, play an attack animation
                // animator?.SetTrigger("Attack"); // Assuming you have an "Attack" trigger in your Animator
            }
        }
        else
        {
            Debug.Log("No player found to attack!");
        }

        // Check for player again for possible state changes (like distance)
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = EnemyState.Attacking;
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = EnemyState.Chasing;
        }
        else
        {
            currentState = EnemyState.Patrolling;
        }
    }

    // private void OnTriggerEnter(Collider other) {
    //     // Find the player
    //     if (playerTarget != null) {
    //         // move towards player when player enters area
    //         transform.LookAt(playerTarget.transform.position);
    //         transform.position += transform.forward * move_speed * Time.deltaTime;
    //     }
    //     if (other.gameObject.CompareTag("Player")) {
    //         // other is set in unity via tags
    //         playerTarget = other.gameObject;
    // }
}

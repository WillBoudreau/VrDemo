using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMove : MonoBehaviour
{
    [Header("Chicken Settings")]
    [SerializeField] private float moveSpeed = 5; // The speed at which the chicken moves
    [SerializeField] private float rotationSpeed = 1.0f; // Rotation speed multiplier
    [SerializeField] private NavMeshAgent agent; // The NavMeshAgent component
    [SerializeField] private float determineTargetRadius = 10; // The radius to determine the target
    [SerializeField] private bool isGrabbed = false; // Is the chicken grabbed

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if(!isGrabbed)
        {
            Move();
        }
    } 

    /// <summary>
    /// Move the chicken
    /// </summary>
    void Move()
    {
        if(agent.remainingDistance < 0.5f)
        {
            MoveToRandomPosition();
        }
    }

    /// <summary>
    /// Move the chicken to a random position within the radius
    /// </summary>
    void MoveToRandomPosition()
    {
        agent.SetDestination(GetRandomPosition());
    }

    /// <summary>
    /// Get a random position within the radius
    /// </summary>
    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * determineTargetRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, determineTargetRadius, 1);
        return hit.position;
    }

    /// <summary>
    /// Called when the chicken is grabbed
    /// </summary>
    public void OnGrab()
    {
        isGrabbed = true;
        agent.isStopped = true;
    }

    /// <summary>
    /// Called when the chicken is released
    /// </summary>
    public void OnRelease()
    {
        isGrabbed = false;
        agent.isStopped = false;
    }
}

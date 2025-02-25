using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private GameObject player; // The player object
    [Header("Target Rotation Settings")]
    [SerializeField] private float rotationSpeed = 1.0f; // Rotation speed multiplier
    [SerializeField] private float zRotation = 90; // Z rotation of the target
    [SerializeField] private float yRotation = 0; // Y rotation of the target
    [SerializeField] private float xRotation = 0; // X rotation of the target

    void Start()
    {
        FindPlayer();
    }
    void Update()
    {
        RotateTowardsPlayer();
    }

    /// <summary>
    /// Find the player object
    /// </summary>
    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    /// <summary>
    /// Rotate the target towards the player
    /// </summary>
    void RotateTowardsPlayer()
    {
        // Get the direction to the player
        Vector3 direction = player.transform.position - transform.position;

        // Get the rotation to look at the player
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Adjust the rotation to be a perfect 
        rotation *= Quaternion.Euler(xRotation, yRotation, zRotation);

        // Rotate the target towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball Hit Target");
            Destroy(this.gameObject);
        }
    }
}

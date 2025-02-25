using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    [Header("Cow Settings")]
    [SerializeField] private GameObject player; // The player object
    [SerializeField] private int maxTargets = 5; // Maximum number of targets that can be spawned
    [SerializeField] private GameObject[] targetPrefabs; // List of target prefabs to spawn
    [SerializeField] private Transform[] targetSpawnPoints; // List of spawn points for the targets

    [Header("Cow Rotation Settings")]
    [SerializeField] private float rotationSpeed = 1.0f; // Rotation speed multiplier
    [SerializeField] private float zRotation = 90; // Z rotation of the cow 
    [SerializeField] private float yRotation = 0; // Y rotation of the cow
    [SerializeField] private float xRotation = 0; // X rotation of the cow

    private List<Transform> usedTargetSpawnPoints = new List<Transform>();
    private List<Transform> availableSpawnPoints = new List<Transform>();


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Add all target spawn points to the available spawn points list
        foreach(Transform spawnPoint in targetSpawnPoints)
        {
            availableSpawnPoints.Add(spawnPoint);
        }
    }
    void Update()
    {
        CheckTargetSpawnPoints();
    }
    /// <summary>
    /// Spawn a target at the given position
    /// </summary>
    void SpawnTarget()
    {
        if(GameObject.FindGameObjectsWithTag("Target").Length >= maxTargets)
        {
            return;
        }

        // Get a random target prefab
        GameObject targetPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];

        // Get a random spawn point that is not used
        Transform spawnPoint = GetUnusedSpawnPoint();

        if (spawnPoint != null)
        {

            // Spawn the target at the spawn point
            Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
            usedTargetSpawnPoints.Add(spawnPoint);
            availableSpawnPoints.Remove(spawnPoint);

        }
        else
        {
            Debug.Log("No available spawn points");
        }
    }

    /// <summary>
    /// Check if target spawn points are empty
    /// </summary>
    void CheckTargetSpawnPoints()
    {
        if (GameObject.FindGameObjectsWithTag("Target").Length <= maxTargets)
        {
            foreach(Transform spawnPoint in targetSpawnPoints)
            {
                if(!usedTargetSpawnPoints.Contains(spawnPoint))
                {
                    SpawnTarget();
                }
            }
        }
    }

    /// <summary>
    /// Get an unused spawn point
    /// </summary>
    Transform GetUnusedSpawnPoint()
    {
        Debug.Log("Getting Unused Spawn Point");
        if (availableSpawnPoints.Count > 0)
        {
            Debug.Log("Available Spawn Points");
            return availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        }
        return null;
    }
}

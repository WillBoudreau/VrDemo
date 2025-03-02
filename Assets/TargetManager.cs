using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{   
    [Header("Target Manager Settings")]
    [SerializeField] private int maxTargets = 5; // Maximum number of targets that can be spawned
    [SerializeField] private GameObject[] targetPrefabs; // List of target prefabs to spawn
    [SerializeField] private Transform[] targetSpawnPoints; // List of spawn points for the targets
    [SerializeField] private Transform[] targetSpawnPoints2; // List of spawn points for the targets
    [SerializeField] private Transform[] targetSpawnPoints3; // List of spawn points for the targets
    [SerializeField] private float targetSpawnRate = 1.0f; // The rate at which the cow spawns targets
    [SerializeField] private bool isAbleToSpawnSet2; // Can the boss spawn set 2
    [SerializeField] private bool isAbleToSpawnSet3; // Can the boss spawn set 3
    [Header("Class Calls")]
    [SerializeField] private PlayerController playerController; // The player controller

    private List<Transform> usedTargetSpawnPoints = new List<Transform>();
    private List<Transform> availableSpawnPoints = new List<Transform>();

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // Add all target spawn points to the available spawn points list
        foreach(Transform spawnPoint in targetSpawnPoints)
        {
            availableSpawnPoints.Add(spawnPoint);
        }
    }
    void Update()
    {
        // Check if the target spawn points are empty
        CheckTargetSpawnPoints();
        if(playerController.killCount >= 5)
        {
            isAbleToSpawnSet2 = true;
        }
        if(playerController.killCount >= 10)
        {
            isAbleToSpawnSet3 = true;
        }
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
        if (GameObject.FindGameObjectsWithTag("Target").Length == 0)
        {
            RespawnTargets();
        }
    }
    /// <summary>
    /// Respawn the targets when they are all destroyed
    /// </summary>
    public void RespawnTargets()
    {
        usedTargetSpawnPoints.Clear();
        availableSpawnPoints.Clear();

        foreach(Transform spawnPoint in targetSpawnPoints)
        {
            availableSpawnPoints.Add(spawnPoint);
            if(isAbleToSpawnSet2)
            {
                foreach(Transform spawnPoint2 in targetSpawnPoints2)
                {
                    availableSpawnPoints.Add(spawnPoint2);
                }
            }
            if(isAbleToSpawnSet3)
            {
                foreach(Transform spawnPoint3 in targetSpawnPoints3)
                {
                    availableSpawnPoints.Add(spawnPoint3);
                }
            }
        }

        for (int i = 0; i < maxTargets; i++)
        {
            SpawnTarget();
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
    /// <summary>
    /// Track the amount of targets spawned
    /// </summary>
    void TrackTargets()
    {
        if(GameObject.FindGameObjectsWithTag("Target").Length >= maxTargets)
        {
            return;
        }
        else
        {
            SpawnTarget();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Transform spawnPoint in targetSpawnPoints)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, 1);
        }
        Gizmos.color = Color.blue;
        foreach(Transform spawnPoint2 in targetSpawnPoints2)
        {
            Gizmos.DrawWireSphere(spawnPoint2.position, 1);
        }
        Gizmos.color = Color.green;
        foreach(Transform spawnPoint3 in targetSpawnPoints3)
        {
            Gizmos.DrawWireSphere(spawnPoint3.position, 1);
        }
    }
}

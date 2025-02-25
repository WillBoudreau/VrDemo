using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Settings")]
    [Header("Game Objects")]
    [SerializeField] private GameObject[] ballPrefabs; // List of ball prefabs to spawn
    [SerializeField] private Transform[] ballSpawnPoints; // List of spawn points for the balls
    [SerializeField] private GameObject[] targetPrefabs; // List of target prefabs to spawn
    [SerializeField] private Transform[] targetSpawnPoints; // List of spawn points for the targets
    [Header("Game Variables")]
    [SerializeField] private float spawnTime = 5; // Time between ball spawns
    [SerializeField] private int maxBalls = 5; // Maximum number of balls that can be spawned
    [SerializeField] private int maxTargets = 5; // Maximum number of targets that can be spawned

    private List<Transform> usedTargetSpawnPoints = new List<Transform>();
    private List<Transform> availableSpawnPoints = new List<Transform>();
    void Start()
    {
        // Add all target spawn points to the available spawn points list
        foreach(Transform spawnPoint in targetSpawnPoints)
        {
            availableSpawnPoints.Add(spawnPoint);
        }
    }
    void Update()
    {
        //SpawnBall();
        CheckTargetSpawnPoints();
    }

    /// <summary>
    /// Spawn a ball at the given position
    /// </summary>
    void SpawnBall()
    {
        if(GameObject.FindGameObjectsWithTag("Ball").Length >= maxBalls)
        {
            return;
        }
        // Get a random ball prefab
        GameObject ballPrefab = ballPrefabs[Random.Range(0, ballPrefabs.Length)];

        // Get a random spawn point
        Transform spawnPoint = ballSpawnPoints[Random.Range(0, ballSpawnPoints.Length)];

        // Spawn the ball at the spawn point
        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }

    /// <summary>
    /// Spawn a target at the given position
    /// </summary>
    void SpawnTarget()
    {
        Debug.Log("Spawning Target");
        if(GameObject.FindGameObjectsWithTag("Target").Length >= maxTargets)
        {
            return;
        }
        Debug.Log("Spawning Target 2");
        // Get a random target prefab
        GameObject targetPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];
        Debug.Log("Spawning Target 3");
        // Get a random spawn point that is not used
        Transform spawnPoint = GetUnusedSpawnPoint();
        Debug.Log("Spawning Target 4");
        if (spawnPoint != null)
        {
            Debug.Log("Spawning Target 5");
            // Spawn the target at the spawn point
            Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
            usedTargetSpawnPoints.Add(spawnPoint);
            availableSpawnPoints.Remove(spawnPoint);
            Debug.Log("Spawning Target 6");
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

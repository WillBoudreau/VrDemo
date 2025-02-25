using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Settings")]
    [SerializeField] private GameObject[] ballPrefabs; // List of ball prefabs to spawn
    [SerializeField] private Transform[] ballSpawnPoints; // List of spawn points for the balls
    [SerializeField] private GameObject[] targetPrefabs; // List of target prefabs to spawn
    [SerializeField] private Transform[] targetSpawnPoints; // List of spawn points for the targets
    [SerializeField] private float spawnTime = 5; // Time between ball spawns
    [SerializeField] private int maxBalls = 5; // Maximum number of balls that can be spawned
    [SerializeField] private int maxTargets = 5; // Maximum number of targets that can be spawned


    void Update()
    {
        SpawnBall();
        SpawnTarget();
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
        if(GameObject.FindGameObjectsWithTag("Target").Length >= maxTargets)
        {
            return;
        }
        // Get a random target prefab
        GameObject targetPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];

        // Get a random spawn point
        Transform spawnPoint = targetSpawnPoints[Random.Range(0, targetSpawnPoints.Length)];

        // Spawn the target at the spawn point
        Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
    }
}

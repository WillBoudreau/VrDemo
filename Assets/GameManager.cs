using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Settings")]
    [SerializeField] private GameObject[] BallPrefabs; // List of ball prefabs to spawn
    [SerializeField] private Transform[] BallSpawnPoints; // List of spawn points for the balls
    [SerializeField] private float spawnTime = 5; // Time between ball spawns
    [SerializeField] private int maxBalls = 5; // Maximum number of balls that can be spawned

    void Update()
    {
        SpawnBall();
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
        GameObject ballPrefab = BallPrefabs[Random.Range(0, BallPrefabs.Length)];

        // Get a random spawn point
        Transform spawnPoint = BallSpawnPoints[Random.Range(0, BallSpawnPoints.Length)];

        // Spawn the ball at the spawn point
        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }
}

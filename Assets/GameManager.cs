using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Settings")]
    [Header("Game Objects")]
    [SerializeField] private GameObject[] ballPrefabs; // List of ball prefabs to spawn
    [SerializeField] private Transform[] ballSpawnPoints; // List of spawn points for the balls
    [Header("Game Variables")]
    [SerializeField] private float spawnTime = 5; // Time between ball spawns
    [SerializeField] private int maxBalls = 5; // Maximum number of balls that can be spawned


    void Update()
    {
        //SpawnBall();
        //CheckTargetSpawnPoints();
    }
}

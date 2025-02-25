using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBehaviour : MonoBehaviour
{
    [Header("Blaster Settings")]
    [SerializeField] private GameObject ballPrefab; // The ball prefab
    [SerializeField] private Transform spawnPoint; // The spawn point for the balls
    [SerializeField] private float fireRate = 1; // The rate at which the blaster fires
    [SerializeField] private float nextFire = 0; // The time of the next fire
    [SerializeField] private float ballSpeed = 1000; // The speed of the ball


    /// <summary>
    /// Fire the blaster
    /// </summary>
    /// <returns></returns>
    public void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnPoint.position, spawnPoint.forward * 10);
    }
}

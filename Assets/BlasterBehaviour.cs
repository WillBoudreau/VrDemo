using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBehaviour : MonoBehaviour
{
    [Header("Blaster Settings")]
    [SerializeField] private GameObject ballPrefab; // The ball prefab
    [SerializeField] private Transform spawnPoint; // The spawn point for the balls
    [SerializeField] private float fireRate = 1; // The rate at which the blaster fires
    [SerializeField] private float ballSpeed = 1000; // The speed of the ball
    [SerializeField] private int maxEggs = 5; // Maximum number of eggs that can be fired before cooldown
    [SerializeField] private int currentEggs = 0; // Current number of eggs fired
    [SerializeField] private Material[] eggMaterials; // List of egg materials
    [SerializeField] private Material blasterMaterial; // The material of the blaster
    [SerializeField] private bool canFire = true;
    [SerializeField] private GameObject startPoint;

    void Start()
    {
        blasterMaterial = GetComponent<Renderer>().material;
    }
    void Update()
    {
        if(currentEggs >= maxEggs)
        {
            canFire = false;
            StartCoroutine(Cooldown());
        }
    }
    /// <summary>
    /// Fire the blaster
    /// </summary>
    /// <returns></returns>
    public void Fire()
    {
        if(canFire)
        {
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
            ball.GetComponent<Renderer>().material = eggMaterials[Random.Range(0, eggMaterials.Length)];
            currentEggs++;
        }
    }
    IEnumerator Cooldown()
    {
        blasterMaterial.color = Color.red;
        yield return new WaitForSeconds(fireRate);
        currentEggs = 0;
        blasterMaterial.color = Color.white;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnPoint.position, spawnPoint.forward * 10);
    }
}

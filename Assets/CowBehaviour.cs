using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    [Header("Evil Chicken Settings Settings")]
    [SerializeField] private GameObject player; // The player object
    public float health = 100; // The health of the cow
    public float damage = 10; // The damage of the cow
    [SerializeField] private bool canTakeDamage; // Can the cow take damage
    [Header("Class calls")]
    [SerializeField] private TargetManager targetManager; // The target manager
    [Header("Egg Settings")]
    [SerializeField] private Transform[] eggSpawnPoints; // List of spawn points for the eggs
    [SerializeField] private float eggSpeed = 1000; // The speed of the eggs
    [SerializeField] private GameObject eggPrefab; // The egg prefab
    [SerializeField] private float eggFireRate; // The rate at which the cow fires eggs
    [SerializeField] private Vector3 eggScale = new Vector3(2,2,2); // The scale of the eggs
    private float starteggFireRate;

    [Header("Evil Chicken Rotation Settings")]
    [SerializeField] private float rotationSpeed = 1.0f; // Rotation speed multiplier
    [SerializeField] private float zRotation = 90; // Z rotation of the cow 
    [SerializeField] private float yRotation = 0; // Y rotation of the cow
    [SerializeField] private float xRotation = 0; // X rotation of the cow
    void Start()
    {
        starteggFireRate = eggFireRate;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        EggCooldown();
    }
   
    /// <summary>
    /// Take Damage
    /// </summary>
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlickerRed());
        if(health <= 0)
        {
            Die();
        }
    }
    ///<summary>
    /// Flicker red when hit
    ///</summary>
    IEnumerator FlickerRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
    /// <summary>
    /// Boss dies
    /// </summary>
    void Die()
    {
        Destroy(this.gameObject);
    }
    
    /// <summary>
    /// Fire Eggs Back at Player
    /// </summary>
    public void FireEgg()
    {
        // Get a random spawn point
        Transform spawnPoint = eggSpawnPoints[Random.Range(0, eggSpawnPoints.Length)];
        GameObject egg = Instantiate(eggPrefab, spawnPoint.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - spawnPoint.position).normalized;
        egg.GetComponent<Rigidbody>().AddForce(direction * eggSpeed);
        egg.transform.localScale = eggScale;
    }
    /// <summary>
    /// Fire Egg after cooldown
    /// </summary>
    void EggCooldown()
    {
        eggFireRate -= Time.deltaTime;
        if(eggFireRate <= 0)
        {
            eggFireRate = starteggFireRate;
            FireEgg();
        }
    }
    /// <summary>
    /// Rotate the chicken towards the player
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(Transform spawnPoint in eggSpawnPoints)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, 1);
        }
    }
}

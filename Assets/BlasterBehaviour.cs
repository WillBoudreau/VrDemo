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
    [SerializeField] private MeshRenderer blasterMesh; // The MeshRenderer of the blaster
    [SerializeField] private bool canFire = true;
    [SerializeField] private GameObject[] hatPrefabs; // List of hat prefabs
    [SerializeField] private int maxEggsBonus = 5; // Maximum number of eggs that can be fired before cooldown
    public int level = 1;
    private bool isCoolingDown = false;

    void Start()
    {
        blasterMesh = GetComponent<MeshRenderer>();
        foreach(GameObject hat in hatPrefabs)
        {
            hat.SetActive(false);
        }
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
    /// </summary>
    /// <returns></returns>
    public void Fire()
    {
        if(canFire)
        {
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            if(level == 2)
            {
                GameObject ball2 = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
                ball2.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
            }
            else if(level == 3)
            {
                GameObject ball2 = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
                GameObject ball3 = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
                ball2.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
                ball3.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
            }
            ball.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * ballSpeed);
            ball.GetComponent<Renderer>().material = eggMaterials[Random.Range(0, eggMaterials.Length)];
            currentEggs++;
        }
    }
    /// <summary>
    /// Level up the blaster
    /// </summary>
    void LevelUp()
    {
        level++;
        if(level == 2)
        {
            maxEggs += maxEggsBonus;
            hatPrefabs[0].SetActive(true);
        }
        else if(level == 3)
        {
            maxEggs += maxEggsBonus;
            hatPrefabs[0].SetActive(false);
            hatPrefabs[1].SetActive(true);
        }
    }
    IEnumerator Cooldown()
    {
        Debug.Log("Cooldown");
        blasterMesh.material.color = Color.red;
        yield return new WaitForSeconds(fireRate);
        currentEggs = 0;
        canFire = true;
        blasterMesh.material.color = Color.white;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chicken" && other.GetComponent<BlasterBehaviour>().level == level)
        {
            LevelUp();
            Destroy(other.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnPoint.position, spawnPoint.forward * 10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private GameObject player; // The player object
    [SerializeField] private CowBehaviour cowBehaviour; // The cow behaviour
    [Header("Target Rotation Settings")]
    [SerializeField] private float rotationSpeed = 1.0f; // Rotation speed multiplier
    [SerializeField] private float zRotation = 0; // Z rotation of the target
    [SerializeField] private float yRotation = 0; // Y rotation of the target
    [SerializeField] private float xRotation = 0; // X rotation of the target
    [Header("Target Mesh Settings")]
    [SerializeField] private Mesh[] targetMeshes; // The list of target meshes
    [SerializeField] private Material[] targetMaterial; // The material of the target
    [SerializeField] private Color targetColor; // The color of the target
    [SerializeField] private float targetScale = 1; // The scale of the target
    [Header("Target Collider Settings")]
    [SerializeField] private float colliderScale = 1; // The scale of the collider
    [SerializeField] private Vector3 colliderOffset; // The offset of the collider
    [SerializeField] private Vector3 colliderSize; // The size of the collider
    [Header("Target Damage Settings")]
    [SerializeField] private float minDamage = 1; // The damage of the target
    [SerializeField] private float maxDamage = 10; // The damage of the target

    void Start()
    {
        FindPlayer();
        SetTargetModel();
        cowBehaviour = GameObject.FindGameObjectWithTag("Boss").GetComponent<CowBehaviour>();
    }
    void Update()
    {
        RotateTowardsPlayer();
    }

    /// <summary>
    /// Find the player object
    /// </summary>
    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    /// <summary>
    /// Rotate the target towards the player
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
    ///<summary>
    /// Set the target model
    ///</summary>
    void SetTargetModel()
    {
        SetMesh();
        SetMaterial();
        SetScale();
        ResizeCollider(GetComponent<MeshFilter>().mesh);
    }
    
    /// <summary>
    /// Set the mesh of the target
    /// </summary>
    void SetMesh()
    {
        // Get a random target mesh
        Mesh targetMesh = targetMeshes[Random.Range(0, targetMeshes.Length)];

        // Set the target mesh
        GetComponent<MeshFilter>().mesh = targetMesh;
    }
    /// <summary>
    /// Set the material of the target
    /// </summary>
    void SetMaterial()
    {
        // Set the material of the target
        GetComponent<MeshRenderer>().material = targetMaterial[Random.Range(0, targetMaterial.Length)];
    }
    /// <summary>
    /// Set the scale of the target
    /// </summary>
    void SetScale()
    {
        // Set the scale of the target
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
    /// <summary>
    /// Resize the collider to match the model
    /// </summary>
    void ResizeCollider(Mesh mesh)
    {
        if(mesh == null)
        {
            return;
        }

        // Calculate the bounds of the mesh
        Bounds bounds = mesh.bounds;

        // Set the collider size to match the mesh bounds
        colliderSize = bounds.size * colliderScale;

        // Set the collider offset to match the mesh center
        colliderOffset = bounds.center;

        // Set the collider size and center
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = colliderSize;
        boxCollider.center = colliderOffset;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball Hit Target");
            cowBehaviour.TakeDamage(player.GetComponent<PlayerController>().playerDamage);
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Get the damage of the target
    /// </summary>
    public float GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}

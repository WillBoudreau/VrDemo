using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float playerHealth = 100; // The health of the player
    public float playerDamage = 10; // The damage of the player
    [Header("Class calls")]
    [SerializeField] private UIManager uiManager; // The UI manager

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    void Update()
    {
        
    }

    /// <summary>
    /// Take damage
    /// </summary>
    public void TakeDamage(float damage)
    {
        // Subtract the damage from the player health
        playerHealth -= damage;
        uiManager.SetUI("DamageUI");

        // Check if the player is dead
        if (playerHealth <= 0)
        {
            // Kill the player
            KillPlayer();
        }
    }
    /// <summary>
    /// Kill the player
    /// </summary>
    void KillPlayer()
    {
        uiManager.SetUI("GameOverUI");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss Egg")
        {
            // Get the target behaviour
            TargetBehaviour target = other.GetComponent<TargetBehaviour>();

            // Check if the target is not null
            if (target != null)
            {
                // Take damage
                TakeDamage(target.GetDamage());
            }
        }
    }
}

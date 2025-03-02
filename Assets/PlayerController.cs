using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float playerHealth = 50; // The health of the player
    public float playerDamage = 10; // The damage of the player
    public int killCount = 0; // The kill count of the player
    [Header("Class calls")]
    [SerializeField] private UIManager uiManager; // The UI manager

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        SetHealthBar();
        UpdateKillCount(0);
    }
    void SetHealthBar()
    {
        uiManager.healthBar.maxValue = playerHealth;
        uiManager.healthBar.value = playerHealth;
    }
    /// <summary>
    /// Take damage
    /// </summary>
    public void TakeDamage(float damage)
    {
        // Subtract the damage from the player health
        playerHealth -= damage;
        uiManager.SetUI("DamageUI");
        Debug.Log("Player health: " + playerHealth);
        UpdateHealthBar();

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
    /// <summary>
    /// Update the health bar based on the player health
    /// </summary>
    void UpdateHealthBar()
    {
        uiManager.healthBar.value = playerHealth;
    }
    /// <summary>
    /// Update the kill count
    /// </summary>
    public void UpdateKillCount(int killcount)
    {
        killCount += killcount;
        uiManager.killCountText.text = "Kill Count: " + killCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossEgg")
        {
            Debug.Log("Hit by boss egg");
            //Take damage from the boss
            CowBehaviour boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<CowBehaviour>();
            // Check if the boss is not null
            if (boss != null)
            {
                // Take damage from the boss
                TakeDamage(boss.damage);
            }
            else
            {
                Debug.LogError("boss is null");
            }
        }
        else if (other.tag == "Target")
        {
            Debug.Log("Hit by target");
            // Take damage from the target
            TargetBehaviour target = other.GetComponent<TargetBehaviour>();
            // Check if the target is not null
            if (target != null)
            {
                // Take damage from the target
                TakeDamage(target.GetDamage());
                Destroy(other.gameObject);
            }
            else
            {
                Debug.LogError("target is null");
            }
        }
    }
}

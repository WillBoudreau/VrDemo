using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private PlayerController player; // The player object
    [SerializeField] private CowBehaviour boss; // The boss object
    [SerializeField] private static GameManager instance; // The instance of the game manager
    [SerializeField] private UIManager uiManager; // The UI manager

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<CowBehaviour>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    void Update()
    {
        CheckPlayerHealth();
        CheckBossHealth();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Check health of the player
    /// </summary>
    public void CheckPlayerHealth()
    {
        if (player.playerHealth <= 0)
        {
            uiManager.SetUI("GameOverUI");
        }
    }
    /// <summary>
    /// Check health of the boss
    /// </summary>
    public void CheckBossHealth()
    {
        if (boss.health <= 0)
        {
            uiManager.SetUI("GameWinUI");
        }
    }
}
